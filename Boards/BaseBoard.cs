using System.Device.Gpio;
using IotDevices.Device;
using nanoFramework.Hardware.Esp32;

namespace IotDevices.Boards
{
    public abstract class BaseBoard
    {
        public PinConfig[] configs;
        public readonly NetworkManager networkManager;
        public readonly GpioController controller;
        private readonly BlynkDevice blinkyDevice;
        public bool IsSetup { get; private set; }

        public BaseBoard(GpioController controller)
        {
            this.controller = controller;
            networkManager = new NetworkManager();
            blinkyDevice = new BlynkDevice("network", 25, controller);
        }

        public virtual void SetupWifi()
        {
            networkManager.ConnectionStatusEvent += (value) =>
            {
                blinkyDevice.Blynk(value);
            };
            blinkyDevice.StartBlynk();
        }

        public virtual void SetupMqtt(string[] topics)
        {

        }
        public virtual void SetupBoard()
        {
            //configs = new[] {
            //    new PinConfig(25, DeviceFunction.PWM1),
            //    new PinConfig(26, DeviceFunction.PWM2),
            //    new PinConfig(27, DeviceFunction.PWM3)
            //};
            //SetupESP32Pins(configs);
            IsSetup = true;
        }
        public abstract void Start();

        public static void SetupESP32Pins(PinConfig[] configs)
        {
            for (int i = 0; i < configs.Length; i++)
            {
                Configuration.SetPinFunction(configs[i].PinValue, configs[i].PinType);
            }
        }

        public void Working()
        {
            blinkyDevice.Blynk(1);
        }
    }
}
