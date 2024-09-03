
using IotDevices.Device.Model;
using IotDevices.Sensor;


namespace IotDevices.Device
{

    public class ClimateDevice
    {
        private readonly BMP280Sensor _bmp280Sensor;
        private readonly Aht20Sensor _aht20Sensor;

        public ClimateDevice()
        {
            _bmp280Sensor = new BMP280Sensor();
            _aht20Sensor = new Aht20Sensor();
        }

        public ClimateDTO Measure()
        {
            string temp = _aht20Sensor.Temperaure;
            string humi = _aht20Sensor.Humidity;
            string press = _bmp280Sensor.Pressure;

            return new ClimateDTO(temp, humi, press);
        }


    }

    
}
