using System;

namespace IotDevices.Protocol.Mqtt
{
    public class MqttSetupData
    {
        private readonly string _host;
        public string host => _host;

        private readonly int _port;
        public int port => _port;

        public string[] topics => _topics;

        private readonly string[] _topics;

        public MqttSetupData(string host, string[] topics, int port = 1883)
        {
            _host = host;
            _port = port;
            _topics = topics;
        }
    }
}
