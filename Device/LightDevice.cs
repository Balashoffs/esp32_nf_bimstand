using IotDevices.Device.Model;
using IotDevices.Sensor;

namespace IotDevices.Device
{
    public class LightDevice
    {
        private readonly OutputPin outputPin;

        public LightDevice(OutputPin outputPin)
        {
            this.outputPin = outputPin;
        }

        public void Execute(LightDTO lightDto)
        {
            if (lightDto.InOn)
            {
                outputPin.TurnOn();
            }
            else
            {
                outputPin.TurnOff();
            }
        }
    }
}