using Iot.Device.Bmp180;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.ReadResult;
using System;

namespace IotDevices.Sensor
{
    public class BMP280Sensor : I2CCustomDevice
    {
        public static string MeasureType
        {
            get { return "mm HG"; }
        }

        private Bmp280 bmp180;

        public BMP280Sensor() : base(Bmp180.DefaultI2cAddress)
        {
            bmp180 = new Bmp280(I2CDevice);
            bmp180.PressureSampling = Iot.Device.Bmxx80.Sampling.Standard;
        }

        private string GetPressure()
        {
            Bmp280ReadResult currentValue = bmp180.Read();
            double value = currentValue.Pressure.MillimetersOfMercury;
            string str = string.Format($"{{0:{"F2"}}}", new object[] { value });
            return str;
        }

        public string Pressure => GetPressure();

        override
        public void Dispose()
        {
            bmp180.Dispose();
            base.Dispose();
        }
    }
}
