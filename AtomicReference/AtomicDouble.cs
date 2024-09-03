using System;


namespace IotDevices.AtomicReference
{
    public class AtomicInteger
    {
        private readonly object _accessLock = new object();
        private int _value;


        public AtomicInteger(int value)
        {
            _value = value;
        }

        public AtomicInteger()
        {
            _value = 0;
        }

        public void Set(int i)
        {
            lock (_accessLock)
            {
                _value = i;
            }
        }

        public int Get()
        {
            int value = 0;
            lock (_accessLock)
            {
                value = _value;
            }

            return value;
        }
    }
}
