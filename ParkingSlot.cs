namespace ParkingSystem
{
    public class ParkingSlot
    {
        public int SlotNumber { get; }
        public Vehicle? ParkedVehicle { get; set; }
        public bool IsAvailable => ParkedVehicle == null;

        public ParkingSlot(int slotNumber)
        {
            SlotNumber = slotNumber;
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            ParkedVehicle = vehicle;
        }

        public void RemoveVehicle()
        {
            ParkedVehicle = null;
        }
    }
}