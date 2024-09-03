using System;
using IotDevices.Device.Model;

namespace IotDevices.Device.Events
{
    public abstract class DeviceEventArgs : EventArgs
    {
        public string Topic { get; private set; }

        protected DeviceEventArgs(string topic)
        {
            Topic = topic;
        }
    }

    public class CurtainsEventArgs : DeviceEventArgs
    {
        public CurtainsEventArgs(string topic, CurtainsDTO dto) : base(topic)
        {
            Curtains = dto;
        }

        public CurtainsDTO Curtains { get; private set; }
    }


    public class LightsEventArgs : DeviceEventArgs
    {
        public LightsEventArgs(string topic, LightDTO dto) : base(topic)
        {
            Light = dto;
        }

        public LightDTO Light { get; private set; }
    }
}