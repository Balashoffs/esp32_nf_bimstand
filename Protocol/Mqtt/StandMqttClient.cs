using System;
using System.Text;
using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;

namespace IotDevices.Protocol.Mqtt
{
    public delegate void IncomeMqttMessageWithTopic(string topic, string message);
    public class StandMqttClient
    {
        private readonly MqttClient _mqttClient;

        public IncomeMqttMessageWithTopic IncomeMsg;

        public StandMqttClient(string name)
        {
            _mqttClient = new MqttClient(name);
            Console.WriteLine("Create mqtt client");
        }

        public bool Connect()
        {
            try
            {
                var clientId = Guid.NewGuid().ToString();
                Console.WriteLine("clientID: " + clientId);
                MqttReasonCode code = _mqttClient.Connect(clientId);
                Console.WriteLine("code: " + code);
                return code == MqttReasonCode.Success;
            }
            catch (Exception)
            {

                Console.WriteLine("not connect to mqtt server");
            }
            return false;
        }

        public void Subscribe(string[] topics)
        {
            _mqttClient.Subscribe(topics, new[] { MqttQoSLevel.AtLeastOnce });
            _mqttClient.MqttMsgPublishReceived += _mqttClient_MqttMsgPublishReceived;
        }


        private void _mqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string json = Encoding.UTF8.GetString(e.Message, 0, e.Message.Length);
            if (IncomeMsg != null)
            {
                IncomeMsg.Invoke(e.Topic, json);
            }

        }

        public void PublishMessage(string topic, string message)
        {
            _mqttClient.Publish(topic, Encoding.UTF8.GetBytes(message), null, null, MqttQoSLevel.AtLeastOnce,
                true);
        }
    }
}