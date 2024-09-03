using System.Device.Gpio;
using System.Device.Pwm;

namespace IotDevices.Sensor.JGA25370B
{
    class L289Driver
    {
        private bool _isReady = false;
        private readonly GpioPin _in1, _in2;
        private readonly PwmChannel _pwm1;
        private bool _isReverse;
        public double Duty
        {
            get => _pwm1.DutyCycle;
            set => _pwm1.DutyCycle = value;
        }
        public L289Driver(GpioController controller, int inPin1, int intPin2, int enbPin, bool isReverse = false)
        {
            _isReverse = isReverse;
            _in1 = controller.OpenPin(inPin1, PinMode.Output);
            _in2 = controller.OpenPin(intPin2, PinMode.Output);
            _pwm1 = PwmChannel.CreateFromPin(enbPin);
        }

        public void Init()
        {
            _in1.Write(PinValue.Low);
            _in2.Write(PinValue.Low);
            _isReady = true;
            
        }

        /*
        Direction	Input 1	Input 2	Enable 1
        Forward	      0	      1	      1
        Backwards	  1	      0	      1
        Stop	      0	      0	      0
        */

        public void Forward()
        {
            if (_isReady)
            {
                if (_isReverse)
                {
                    _pwm1.Start();
                    _in1.Write(PinValue.Low);
                    _in2.Write(PinValue.High);
                }
                else
                {
                    _pwm1.Start();
                    _in1.Write(PinValue.High);
                    _in2.Write(PinValue.Low);
                }

            }
        }

        public void Backward()
        {
            if (_isReverse)
            {
                _pwm1.Start();
                _in1.Write(PinValue.High);
                _in2.Write(PinValue.Low);
            }
            else
            {
                _pwm1.Start();
                _in1.Write(PinValue.Low);
                _in2.Write(PinValue.High);
            }
        }

        public void Stop()
        {
            if (_isReady)
            {
                _pwm1.Stop();
                _in1.Write(PinValue.Low);
                _in2.Write(PinValue.Low);
            }
        }

        public void Dispose()
        {
            _pwm1.Dispose();
            _in1.Dispose();
            _in2.Dispose();
        }
    }
}
