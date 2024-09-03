namespace IotDevices.AtomicReference
{
    public class AtomicBoolean
    {
        private bool _value;
        private readonly object _accessLock = new object();

        public AtomicBoolean(bool value)
        {
            _value = value;
        }

        public AtomicBoolean()
        {
            _value = false;
        }

        public void Set(bool value)
        {
            lock (_accessLock)
            {
                _value = value;
            }
        }

        public bool Get()
        {
            bool value = false;
            lock (_accessLock)
            {
                value = _value;
            }

            return value;
        }
    }
}
