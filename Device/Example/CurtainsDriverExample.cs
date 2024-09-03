
using System.Device.Gpio;
using System.Threading;
using IotDevices.Device.Model;
using IotDevices.Sensor.JGA25370B;

namespace IotDevices.Device.Example
{
    public class CurtainsDriverExample
    {
        public static void DriverExample()
        {
            GpioController controller = new GpioController();
            JGA25370B drv1 = new JGA25370B("", 22, 21, 23, 17, 16, controller);
            drv1.Init();

            JGA25370B drv2 = new JGA25370B("", 4, 19, 18, 13, 14, controller);
            drv2.Init();



            CurtainsDevice cd1 = new CurtainsDevice(drv1);
            cd1.Init();
            CurtainsDevice cd2 = new CurtainsDevice(drv2);
            cd2.Init();

            new Thread(() =>
            {
                while (true)
                {
                    cd1.Execute(new CurtainsDTO(1));
                    cd1.Execute(new CurtainsDTO(-1));
                }
            }).Start();
            new Thread(() =>
            {
                while (true)
                {
                    cd2.Execute(new CurtainsDTO(1));
                    cd2.Execute(new CurtainsDTO(-1));
                }
            }).Start();
        }
    }
}
