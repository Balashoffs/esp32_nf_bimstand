using nanoFramework.Hardware.Esp32;

namespace IotDevices.Boards
{
    public class PinConfig
    {
        public int PinValue { get; private set; }
        public DeviceFunction PinType { get; private set; }

        public PinConfig(int pin, DeviceFunction df)
        {
            PinValue = pin;
            PinType = df;
        }
    }
}
