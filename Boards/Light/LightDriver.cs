using IotDevices.Device;
using IotDevices.Device.Model;
using IotDevices.Protocol.Mqtt;
using nanoFramework.Json;

namespace IotDevices.Boards.Light
{
    public class LightDriver
    {
        private string[] _lightTopic;
        private LightDevice[] _lightDevices;
        private StandMqttClient _mqttClient;

        public LightDriver(LightDevice[] lightDevices, string[] lightTopics, StandMqttClient mqttClient)
        {
            _lightDevices = lightDevices;
            _lightTopic = lightTopics;
            _mqttClient = mqttClient;
        }

        public void Init()
        {
            _mqttClient.IncomeMsg += (topic, msg) =>
            {
                for (int i = 0; i < _lightTopic.Length; i++)
                {

                    string topicS = _lightTopic[i];
                    if (topic.Equals(topicS))
                    {
                        LightDTO lightDto = (LightDTO)JsonConvert.DeserializeObject(msg, typeof(LightDTO));
                        LightDevice cd = _lightDevices[i];
                        cd.Execute(lightDto);
                    }
                }
            };
        }
    }
}