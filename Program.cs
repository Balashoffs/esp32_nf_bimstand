using IotDevices.Boards.Climate;
using IotDevices.Boards.Curtains;
using System.Diagnostics;
using System.Threading;

namespace IotDevices
{
    public class Program
    {
        public static void Main()
        {
            //RunClimateBoard();
            RunCurtains();
        }

        private static void RunClimateBoard()
        {
            ClimateBoard board = new ClimateBoard(new System.Device.Gpio.GpioController(), 10000);
            board.SetupBoard();
            board.SetupWifi();
            //board.SetupMqtt(new[] { "bimstand/office_room_3_2_1/climate_data" });
            board.SetupMqtt(new[] { "bimstand/office_room_3_2_2/climate_data" });
            while (true)
            {
                try
                {
                    board.Start();
                    while (true)
                    {
                        Thread.Sleep(100);
                    }
                }
                catch (System.Exception e)
                {
                    Thread.Sleep(1000);
                    Debug.WriteLine(e.Message);
                }
            }
        }

        private static void RunCurtains()
        {
            CurtainsBoard board = new CurtainsBoard(new System.Device.Gpio.GpioController());
            board.SetupBoard();
            board.SetupWifi();
            board.SetupMqtt(new[] { "bimstand/office_room_3_2_1/curtains_switch" });
            //board.SetupMqtt(new[] { "bimstand/office_room_3_2_2/curtains_switch" });
            while (true)
            {
                try
                {
                    board.Start();
                    while (true)
                    {
                        Thread.Sleep(100);
                    }
                }
                catch (System.Exception e)
                {
                    Thread.Sleep(1000);
                    Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
