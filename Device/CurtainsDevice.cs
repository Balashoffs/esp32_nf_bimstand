using IotDevices.AtomicReference;
using IotDevices.Device.Model;
using IotDevices.Sensor.JGA25370B;

namespace IotDevices.Device
{
    public class CurtainsDevice
    {
        private readonly JGA25370B _curtainDriver;
        private readonly AtomicInteger _status;

        private readonly double _qnt = 500.0;
        public CurtainsDevice(JGA25370B curtainDriver)
        {
            _curtainDriver = curtainDriver;
            _status = new AtomicInteger(0);
        }

        public void Init()
        {
            _curtainDriver.Init();
        }

        public void Execute(CurtainsDTO curtains)
        {
            int nextDirection = curtains.direction;
            int currentDirection = _status.Get();
            if (currentDirection == 0)
            {
                _status.Set(nextDirection);
            }
            else
            {
                if (nextDirection != currentDirection)
                {
                    _status.Set(nextDirection);
                    _curtainDriver.TryToStop();
                    if (nextDirection == 1)
                    {
                        _curtainDriver.Forward(_qnt);
                    }
                    else if (nextDirection == -1)
                    {
                        _curtainDriver.Backward(_qnt);
                    }

                }
            }
        }
    }
}
