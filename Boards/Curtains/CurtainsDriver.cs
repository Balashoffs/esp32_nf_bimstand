using System.Device.Gpio;
using System.Threading;
using IotDevices.Device;
using IotDevices.Device.Model;
using IotDevices.Protocol.Mqtt;
using IotDevices.Sensor.JGA25370B;
using nanoFramework.Json;


namespace IotDevices.Boards.Curtains
{
    public class CurtainsDriver
    {
        private CurtainsDevice[] _curtainsDevices;
        private string[] _curtainsTopics;
        private StandMqttClient _mqttClient;

        public CurtainsDriver(CurtainsDevice[] curtainsDevices, string[] curtainsTopics, StandMqttClient mqttClient)
        {
            _curtainsDevices = curtainsDevices;
            _curtainsTopics = curtainsTopics;
            _mqttClient = mqttClient;
        }

        public void Start()
        {
            _mqttClient.IncomeMsg += (incomeTopic, json) =>
            {
                for (int i = 0; i < _curtainsTopics.Length; i++)
                {
                    string topic = _curtainsTopics[i];
                    if (topic.Equals(incomeTopic))
                    {
                        CurtainsDTO curtainsDto = (CurtainsDTO)JsonConvert.DeserializeObject(json, typeof(CurtainsDTO));
                        for (int j = 0; j < _curtainsDevices.Length; j++)
                        {
                            CurtainsDevice cd = _curtainsDevices[j];
                            Thread th = new Thread(() => {
                                cd.Execute(curtainsDto);
                            });
                            th.Start();
                        }
                    }
                }
            };
        }

        public static CurtainsDevice[] BuildCurtainsDevice(int[][] pins, GpioController controller)
        {
            CurtainsDevice[] cds = new CurtainsDevice[pins.Length];
            for (int i = 0;  i< pins.Length; i++)
            {
                int[] pinss = pins[i];
                //22, 21, 23, 17, 16,
                string name = "ch_" + (i + 1);
                JGA25370B drv = new JGA25370B(name, pinss[0], pinss[1], pinss[2], pinss[3], pinss[4], controller, i % 2 != 0);

                drv.Init();
                CurtainsDevice cd = new CurtainsDevice(drv);
                cd.Init();
                cds[i] = cd;
            }
            return cds;
        }
    }
}