
using System.Device.I2c;

namespace IotDevices.Sensor
{
    public abstract class I2CCustomDevice
    {

        public I2cDevice I2CDevice { get; private set; }

        public I2CCustomDevice(byte address, int busId = 1)
        {
            I2cConnectionSettings i2cSettings = new(busId, address);
            I2CDevice = I2cDevice.Create(i2cSettings);
        }

       
        public virtual void Dispose()
        {
            I2CDevice.Dispose();
        }

    }
}
