using System;
using System.Device.Pwm;

namespace IotDevices.Device
{
    public class PwmDevice
    {
        private readonly PwmChannel _pwm1;
        public string Id { get; private set; }

        public PwmDevice(string id, int enbPin, int freq = 5000, double duty = 0.1)
        {
            Id = id;
            _pwm1 = PwmChannel.CreateFromPin(enbPin, freq, duty);
            _pwm1.Start();
           
        }

        public void TurnOn(double pwm)
        {
            _pwm1.DutyCycle = pwm;
        }

        public void TurnOff()
        {
            _pwm1.Stop();
        }
    }
}
