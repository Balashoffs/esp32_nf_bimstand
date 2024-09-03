namespace IotDevices.Device.Model
{
    public class CurtainsDTO
    {
        public int direction;

        public CurtainsDTO() {
            direction = 0;
        }
        public CurtainsDTO(int dir)
        {
            direction = dir;
        }
    }
}
