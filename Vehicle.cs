namespace ParkingSystem
{
    public class Vehicle
    {
        public string LicensePlate { get; }
        public string Color { get; }
        public string Type { get; }

        public Vehicle(string licensePlate, string color, string type)
        {
            LicensePlate = licensePlate;
            Color = color;
            Type = type;
        }
    }
}