using System.Device.Gpio;

namespace IotDevices.Sensor
{
    public class OutputPin
    {
        private readonly GpioPin _in1;
        public OutputPin(GpioController controller, int inPin1)
        {
            _in1 = controller.OpenPin(inPin1, PinMode.Output);
            _in1.Write(PinValue.Low);
        }

        public void TurnOn()
        {
            _in1.Write(PinValue.High);
        }

        public void TurnOff()
        {
            _in1.Write(PinValue.Low);
        }

        public void Toggle()
        {
            _in1.Toggle();
        }
    }
}