using Iot.Device.Bmp180;
using UnitsNet;

namespace IotDevices.Sensor
{
    public class Bmp180Sensor : I2CCustomDevice
    {
        public static string MeasureType
        {
            get { return "mm HG"; }
        }

        private Bmp180 bmp180;

        public Bmp180Sensor() : base(Bmp180.DefaultI2cAddress)
        {
            bmp180 = new Bmp180(I2CDevice);
            bmp180.SetSampling(Sampling.Standard);
        }

        private string GetPressure()
        {
            Pressure currentValue = bmp180.ReadPressure();
            double value = currentValue.MillimetersOfMercury;
            return string.Format($"{{0:{"F2"}}}", new object[] { value });
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
