namespace IotDevices.Device.Model
{
    public class LightDTO
    {
        public bool InOn { get; private set; }
        public LightDTO(string topic, bool isOn)
        {
            InOn = isOn;
        }
    }
}
