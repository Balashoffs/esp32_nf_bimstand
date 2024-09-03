using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;
using IotDevices.AtomicReference;
using IotDevices.Sensor;

namespace IotDevices.Device
{
    public class BlynkDevice
    {
        private readonly OutputPin outputPin;
        private AtomicBoolean atomicBoolean;
        private AtomicInteger atomicInt;
        private string id;
        public string Id => id;

        public BlynkDevice(string id, int pin, GpioController controller)
        {
            outputPin = new OutputPin(controller, pin);
            atomicBoolean = new AtomicBoolean(true);
            atomicInt = new AtomicInteger(100);
            this.id = id;
        }

        public void TurnOn()
        {
            outputPin.TurnOn();
        }

        public void TurnOff()
        {
            outputPin.TurnOff();
        }

        public void StartBlynk()
        {
            Thread thred = new Thread(() =>
            {
                Debug.WriteLine("Blynking");
                Blunking();
            });
            thred.Start();
        }

        public void Blynk(int delay = 0)
        {
            if(delay > 0)
            {
                atomicInt.Set(delay);
            }else if(delay == 1)
            {
                atomicBoolean.Set(false);
                Thread.Sleep(1000);
                outputPin.TurnOn();
            }
            else
            {
                outputPin.TurnOff();
            }

        }

        private void Blunking()
        {
            while (atomicBoolean.Get())
            {
                int delay = atomicInt.Get();
                outputPin.Toggle();
                Thread.Sleep(delay);
                outputPin.Toggle();
                Thread.Sleep(delay);
                outputPin.Toggle();
                Thread.Sleep(delay);
                outputPin.Toggle();
                Thread.Sleep(delay);
                outputPin.Toggle();
                Thread.Sleep(delay);
            }
            Debug.WriteLine("Stop Blynking");
        }

    }
}
