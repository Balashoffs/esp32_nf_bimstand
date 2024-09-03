using System.Device.Gpio;
using IotDevices.Device;
using IotDevices.Network;
using IotDevices.Protocol.Mqtt;
using nanoFramework.Hardware.Esp32;

namespace IotDevices.Boards.Curtains
{
    public class CurtainsBoard : BaseBoard
    {
        public CurtainsBoard(GpioController controller) : base(controller)
        {
        }


        override
        public void SetupBoard()
        {
            System.Diagnostics.Debug.WriteLine($"'SetupBoard' was started");
            base.SetupBoard();
            PinConfig[] configs = new PinConfig[] {
                new PinConfig(23, DeviceFunction.PWM1),
                new PinConfig(18, DeviceFunction.PWM2),
            };
            SetupESP32Pins(configs);
            System.Diagnostics.Debug.WriteLine($"'SetupBoard' was finished");
            

        }
        override
        public void SetupWifi()
        {
            Credentials[] allWifiCredentials = new Credentials[] {
                new Credentials("MikroTik-C4F74B", "19odessamama87")
            };
            networkManager.SetupWifiCredentials(allWifiCredentials);
            base.SetupWifi();
        }

        public override void SetupMqtt(string[] topics)
        {
            //MqttSetupData mqd = new MqttSetupData("192.168.1.100", topics);
            MqttSetupData mqd = new MqttSetupData("192.168.88.175", topics);
            networkManager.SetupMqtt(mqd);
        }



        override
        public void Start()
        {   
            bool isConnect = networkManager.ConnectToWifi();
            if (isConnect)
            {
                StandMqttClient mqttClient;
                isConnect = networkManager.ConnectToMqttServer(out mqttClient);
                if (isConnect)
                {
                    mqttClient.Subscribe(networkManager.topics);
                    int[] pinsOne = new[] { 22, 21, 23, 17, 16 };
                    int[] pinsTwo = new[] { 4, 19, 18, 14, 13 };
                    //int[][] allPind = new[] { pinsOne, pinsTwo };
                    int[][] allPind = new[] { pinsOne, pinsTwo };
                    CurtainsDevice[] cds = CurtainsDriver.BuildCurtainsDevice(allPind, controller);
                    CurtainsDriver cdrv = new CurtainsDriver(cds, networkManager.topics, mqttClient);
                    cdrv.Start();
                    Working();
                   
                }
            }
        }
    }
}
