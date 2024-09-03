using System.Device.Gpio;
using System.Threading;
using nanoFramework.Hardware.Esp32;

namespace IotDevices.Sensor.JGA25370B.Examples
{
    public class Examples
    {
        public static void Run()
        {
            GpioController gpioController = new GpioController();
            Configuration.SetPinFunction(27, DeviceFunction.PWM1);
            JGA25370B s = new JGA25370B("", 12, 14, 27, 25, 26, gpioController);
            s.Init();

            while (true)
            {
                for (int i = 10; i >= 1; i--)
                {
                    s.Backward(10);
                    Thread.Sleep(1000);
                }
                for (int i = 1; i <= 10; i++)
                {
                    s.Forward(10);
                    Thread.Sleep(1000);
                }


            }
        }
    }
}
