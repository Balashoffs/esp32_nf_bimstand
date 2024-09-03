using nanoFramework.Json;
using System.Device.Gpio;
using System.Threading;
using IotDevices.Device;
using IotDevices.Device.Model;
using IotDevices.Network;
using IotDevices.Protocol.Mqtt;
using nanoFramework.Hardware.Esp32;
using System.Diagnostics;

namespace IotDevices.Boards.Climate
{
    class ClimateBoard : BaseBoard
    {
        private readonly int delayValueInMs;

        public ClimateBoard(GpioController controller, int delay = 1000) : base(controller)
        {
            delayValueInMs = delay;
        }
        
        override
        public void SetupWifi()
        {
            Credentials[] allWifiCredentials = new Credentials[] {
                new Credentials("bimstand", "odessamama")
            };
            networkManager.SetupWifiCredentials(allWifiCredentials);
            base.SetupWifi();
        }

        override
        public void SetupMqtt(string[] topics)
        {
            MqttSetupData mqd = new MqttSetupData("192.168.1.100", topics);
            networkManager.SetupMqtt(mqd);
        }

        override
        public void SetupBoard()
        {
            PinConfig[] configs = new PinConfig[] {
                new PinConfig(21, DeviceFunction.I2C1_DATA),
                new PinConfig(22, DeviceFunction.I2C1_CLOCK),
                new PinConfig(16, DeviceFunction.I2C2_DATA),
                new PinConfig(17, DeviceFunction.I2C2_CLOCK),
            };
            SetupESP32Pins(configs);
            base.SetupBoard();

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
                    ClimateDevice climateDevice = new ClimateDevice();
                    string topic = networkManager.topics[0];
                    while (true)
                    {
                        ClimateDTO dto = climateDevice.Measure();
                        string json = JsonConvert.SerializeObject(dto);
                        Debug.WriteLine(json);
                        mqttClient.PublishMessage(topic, json);
                        Thread.Sleep(delayValueInMs);
                    }
                }
            }
        }


    }
}
