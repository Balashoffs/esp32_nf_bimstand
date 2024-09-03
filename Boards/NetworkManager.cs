using System;
using IotDevices.Network;
using IotDevices.Protocol.Mqtt;

namespace IotDevices.Boards
{

    public class NetworkManager
    {
        private Credentials[] allWifiCredentials;
        private MqttSetupData mqttSetup;

        public string[] topics => mqttSetup.topics;

        public delegate void ConnectionStatus(int value);
        public event ConnectionStatus ConnectionStatusEvent;

        public void SetupWifiCredentials(Credentials[] Credentials)
        {
            allWifiCredentials = Credentials;
        }

        public void SetupMqtt(MqttSetupData mqttSetup)
        {
            this.mqttSetup = mqttSetup;
        }

        public bool ConnectToWifi()
        {
            ConnectionStatusEvent?.Invoke(100);
            bool isConnect = false;
            for (int i = 0; i < allWifiCredentials.Length; i++)
            {
                Credentials cur = allWifiCredentials[i];
                isConnect = WirelessManager.SetupAndConnectNetwork(cur.Ssid, cur.Password);
                if (isConnect)
                {
                    ConnectionStatusEvent?.Invoke(200);
                }
            }
            if (!isConnect)
            {
                ConnectionStatusEvent?.Invoke(600);
            }
            return isConnect;
        }

        public bool ConnectToMqttServer(out StandMqttClient client)
        {
            ConnectionStatusEvent?.Invoke(200);
            client = new StandMqttClient(mqttSetup.host);
            bool isMqtt = false;
            do
            {
                try
                {
                    ConnectionStatusEvent?.Invoke(300);
                    isMqtt = client.Connect();
                }
                catch (Exception)
                {
                    ConnectionStatusEvent?.Invoke(600);
                }
            } while (!isMqtt);
            return isMqtt;
        }
    }
}
