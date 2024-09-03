using IotDevices.AtomicReference;
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;

namespace IotDevices.Sensor.JGA25370B
{
    public class JGA25370B
    {
        //https://iarduino.ru/shop/Mehanika/motor-reduktor-25mm-620-ob-min-s-enkoderom.html

        private readonly GpioPulseCounter _counter1;
        private readonly L289Driver _l289Driver;
        private bool _isReady = false;
        private bool _isWork = false;
        
        private string id;
        private readonly AtomicDouble _currentSpins;
        public AtomicDouble LastSpins => _currentSpins;
        public JGA25370B(
            string id,
            int inPin1,
            int inPin2,
            int enblPin,
            int countPin1,
            int countPin2,
            GpioController controller,
            bool isReverse = false
        )
        {
            this.id = id;
            
            _counter1 = new GpioPulseCounter(countPin1, countPin2);
            _counter1.Polarity = GpioPulsePolarity.Both;
            _currentSpins = new AtomicDouble(0.0);
            _l289Driver = new L289Driver(controller, inPin1, inPin2, enblPin, isReverse);
        }

        public void Init()
        {
            if (!_isReady)
            {
                _l289Driver.Init();
                _isReady = true;
            }
        }

        public void Forward(double maxRotation)
        {
            if (_isReady)
            {
                double max = PreStart(maxRotation);
                _l289Driver.Forward();

                while (_isWork)
                {
                    GpioPulseCount pulseQnt = _counter1.Read();
                    double spin = pulseQnt.Count;
                    Debug.WriteLine($"{id} -> forward::spin: {spin}");
                    max = max - spin / 7 / 14.5 * 0.67;
                    _currentSpins.Set(max);
                    Debug.WriteLine($"{id} -> forward::max: {max}");
                    if (max.CompareTo(0.0) <= 0)
                    {
                        Clear();
                        Debug.WriteLine($"{id} Forward -> Stop rotate: total spin is {spin}");
                    }
                }
            }
        }

        public void TryToStop()
        {
            object _lock = new object();
            if (_currentSpins.Get().CompareTo(0.0) != 0.0)
            {
                Debug.WriteLine("try to stop spin");
                lock (_lock)
                {
                    _counter1.Stop();
                    _isWork = false;
                    _l289Driver.Stop();
                    Thread.Sleep(1000);
                }
            }
        }

        public void Backward(double maxRotation)
        {
            if (_isReady)
            {
                double max = PreStart(maxRotation);
                _l289Driver.Backward();
                while (_isWork)
                {
                    GpioPulseCount pulseQnt = _counter1.Read();
                    double spin = pulseQnt.Count;
                    Debug.WriteLine($"{id} -> forward::spin: {max}");
                    max = max - spin / 7 / 14.5 * -0.67;
                    _currentSpins.Set(max);
                    Debug.WriteLine($"{id} -> forward::max: {max}");
                    if (max.CompareTo(0.0) <= 0)
                    {
                        Clear();
                        Debug.WriteLine($"{id} Backward -> Stop rotate: total spin is {spin}");
                    }
                }
            }
        }

        private double PreStart(double max)
        {
            _isWork = true;
            _counter1.Start();
            double lastSpin = _currentSpins.Get();
            if (lastSpin.CompareTo(0.0) != 0)
            {
                max = lastSpin;
                Debug.WriteLine($"{id} PreStart -> last lastSpin is {lastSpin}");
                Debug.WriteLine($"{id} PreStart -> last current max is {max}");
            }

            return max;
        }

        private void Clear()
        {
            _l289Driver.Stop();
            _counter1.Reset();
            _currentSpins.Set(0.0);
            _counter1.Stop();
            _isWork = false;
        }
    }
}
