using System;
using System.Diagnostics;
using Iot.Device.Ahtxx;
using UnitsNet;

namespace IotDevices.Sensor
{
    public class Aht20Sensor : I2CCustomDevice
    {
        public static string MeasureTemperatureType
        {
            get { return "\u00b0C"; }
        }

        public static string MeasureHumidityType
        {
            get { return "%"; }
        }

        private Aht20 aht20;

        public Aht20Sensor() : base(Aht20.DefaultI2cAddress)
        {
            aht20 = new(I2CDevice);
        }

        private string GetTemperaure()
        {
            Temperature currentValue = aht20.GetTemperature();
            double value = currentValue.DegreesCelsius;
            string str = string.Format($"{{0:{"F2"}}}", new object[] { value });
            return str;
        }

        public string Temperaure => GetTemperaure();

        private string GetHumidity()
        {
            RelativeHumidity currentValue = aht20.GetHumidity();
            double value = currentValue.Value;
            string str = string.Format($"{{0:{"F2"}}}", new object[] { value });
            return str;
        }

        public string Humidity => GetHumidity();


        override
        public void Dispose()
        {
            aht20.Dispose();
            base.Dispose();
        }
    }
}
