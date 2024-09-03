
namespace IotDevices.Device.Model
{
    public class ClimateDTO
    {
        public string _temperature;
        public string temperature => _temperature;
        public string _humidity;
        public string humidity => _humidity;
        public string _pressure;
        public string pressure => _pressure;

        public ClimateDTO(string temperature, string humidity, string pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
        }
    }
}
