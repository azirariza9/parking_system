using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    public class ParkingLot
    {
        private ParkingSlot[] slots;
        private int TotalSlots => slots.Length;

        public ParkingLot(int totalSlots)
        {
            slots = new ParkingSlot[totalSlots];
            for (int i = 0; i < totalSlots; i++)
            {
                slots[i] = new ParkingSlot(i + 1);
            }
        }

        public int ParkVehicle(string licensePlate, string color, string type)
        {
            var availableSlot = slots.FirstOrDefault(s => s.IsAvailable);
            if (availableSlot == null)
            {
                return -1; 
            }

            var vehicle = new Vehicle(licensePlate, color, type);
            availableSlot.ParkVehicle(vehicle);
            return availableSlot.SlotNumber;
        }

        public bool Leave(int slotNumber)
        {
            var slot = slots.FirstOrDefault(s => s.SlotNumber == slotNumber);
            if (slot == null || slot.IsAvailable)
            {
                return false;
            }

            slot.RemoveVehicle();
            return true;
        }

        public int GetOccupiedSlotsCount() => slots.Count(s => !s.IsAvailable);
        public int GetAvailableSlotsCount() => slots.Count(s => s.IsAvailable);

        public IEnumerable<Vehicle> GetVehiclesByType(string type) =>
            slots.Where(s => !s.IsAvailable && s.ParkedVehicle.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                 .Select(s => s.ParkedVehicle);

        public IEnumerable<Vehicle> GetVehiclesByColor(string color) =>
            slots.Where(s => !s.IsAvailable && s.ParkedVehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                 .Select(s => s.ParkedVehicle);

        public IEnumerable<Vehicle> GetVehiclesByPlateType(bool isOddPlate)
        {
            foreach (var slot in slots)
            {
                if (slot.IsAvailable) continue;
                
                var plateNumber = slot.ParkedVehicle.LicensePlate.Split('-')[1];
                if (int.TryParse(plateNumber, out int numericPart))
                {
                    bool isOdd = numericPart % 2 != 0;
                    if (isOdd == isOddPlate)
                    {
                        yield return slot.ParkedVehicle;
                    }
                }
            }
        }

        public int? FindSlotByLicensePlate(string licensePlate)
        {
            var slot = slots.FirstOrDefault(s => 
                !s.IsAvailable && 
                s.ParkedVehicle.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
            
            return slot?.SlotNumber;
        }

        public IEnumerable<ParkingSlot> GetOccupiedSlots() => 
            slots.Where(s => !s.IsAvailable);
    }
}