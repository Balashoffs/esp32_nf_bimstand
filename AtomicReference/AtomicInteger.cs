using System;


namespace IotDevices.AtomicReference
{
    public class AtomicDouble
    {
        private readonly object _accessLock = new object();
        private double _value;


        public AtomicDouble(double value)
        {
            _value = value;
        }

        public AtomicDouble()
        {
            _value = 0.0;
        }

        public void Set(double i)
        {
            lock (_accessLock)
            {
                _value = i;
            }
        }

        public double Get()
        {
            double value = 0;
            lock (_accessLock)
            {
                value = _value;
            }

            return value;
        }
    }
}
