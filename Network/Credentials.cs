using System;


namespace IotDevices.Network
{
    public class Credentials
    {
        private readonly string _ssid;
        private readonly string _password;

        public string Ssid => _ssid;
        public string Password => _password;

        public Credentials(string ssid, string password)
        {
            _ssid = ssid;
            _password = password;
        }


    }
}
