using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    class Program
    {
        private static ParkingLot parkingLot;

        static void Main(string[] args)
        {
            Console.WriteLine("Parking System - Type 'exit' to quit");
            
            while (true)
            {
                Console.Write("$ ");
                var input = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrEmpty(input)) continue;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                ProcessCommand(input);
            }
        }

        private static void ProcessCommand(string input)
        {
            var parts = input.Split(' ');
            var command = parts[0].ToLower();

            switch (command)
            {
                case "create_parking_lot":
                    CreateParkingLot(parts);
                    break;
                case "park":
                    ParkVehicle(parts);
                    break;
                case "leave":
                    LeaveSlot(parts);
                    break;
                case "status":
                    ShowStatus();
                    break;
                case "type_of_vehicles":
                    ShowVehicleTypeCount(parts);
                    break;
                case "registration_numbers_for_vehicles_with_odd_plate":
                    ShowOddPlateVehicles();
                    break;
                case "registration_numbers_for_vehicles_with_even_plate":
                    ShowEvenPlateVehicles();
                    break;
                case "registration_numbers_for_vehicles_with_colour":
                    ShowVehiclesByColor(parts);
                    break;
                case "slot_numbers_for_vehicles_with_colour":
                    ShowSlotsByColor(parts);
                    break;
                case "slot_number_for_registration_number":
                    FindSlotByLicensePlate(parts);
                    break;
                default:
                    Console.WriteLine($"Invalid command: {command}");
                    break;
            }
        }

        private static void CreateParkingLot(string[] parts)
        {
            if (parts.Length < 2 || !int.TryParse(parts[1], out int totalSlots) || totalSlots <= 0)
            {
                Console.WriteLine("Invalid command format. Usage: create_parking_lot [total_slots]");
                return;
            }

            parkingLot = new ParkingLot(totalSlots);
            Console.WriteLine($"Created a parking lot with {totalSlots} slots");
        }

        private static void ParkVehicle(string[] parts)
        {
            if (parkingLot == null)
            {
                Console.WriteLine("Please create parking lot first");
                return;
            }

            if (parts.Length < 4)
            {
                Console.WriteLine("Invalid command format. Usage: park [license_plate] [color] [type]");
                return;
            }

            var licensePlate = parts[1];
            var color = parts[2];
            var type = parts[3];

            var slotNumber = parkingLot.ParkVehicle(licensePlate, color, type);
            if (slotNumber == -1)
            {
                Console.WriteLine("Sorry, parking lot is full");
            }
            else
            {
                Console.WriteLine($"Allocated slot number: {slotNumber}");
            }
        }

        private static void LeaveSlot(string[] parts)
        {
            if (parkingLot == null)
            {
                Console.WriteLine("Please create parking lot first");
                return;
            }

            if (parts.Length < 2 || !int.TryParse(parts[1], out int slotNumber))
            {
                Console.WriteLine("Invalid command format. Usage: leave [slot_number]");
                return;
            }

            if (parkingLot.Leave(slotNumber))
            {
                Console.WriteLine($"Slot number {slotNumber} is free");
            }
            else
            {
                Console.WriteLine($"Slot number {slotNumber} is already empty or invalid");
            }
        }

        private static void ShowStatus()
        {
            if (parkingLot == null)
            {
                Console.WriteLine("Please create parking lot first");
                return;
            }

            Console.WriteLine("Slot No.\tRegistration No.\tType\tColour");
            foreach (var slot in parkingLot.GetOccupiedSlots())
            {
                var vehicle = slot.ParkedVehicle;
                Console.WriteLine($"{slot.SlotNumber}\t{vehicle.LicensePlate}\t{vehicle.Type}\t{vehicle.Color}");
            }
        }

        private static void ShowVehicleTypeCount(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command format. Usage: type_of_vehicles [vehicle_type]");
                return;
            }

            var type = parts[1];
            var count = parkingLot.GetVehiclesByType(type).Count();
            Console.WriteLine(count);
        }

        private static void ShowOddPlateVehicles()
        {
            var vehicles = parkingLot.GetVehiclesByPlateType(true);
            Console.WriteLine(string.Join(", ", vehicles.Select(v => v.LicensePlate)));
        }

        private static void ShowEvenPlateVehicles()
        {
            var vehicles = parkingLot.GetVehiclesByPlateType(false);
            Console.WriteLine(string.Join(", ", vehicles.Select(v => v.LicensePlate)));
        }

        private static void ShowVehiclesByColor(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command format. Usage: registration_numbers_for_vehicles_with_colour [color]");
                return;
            }

            var color = parts[1];
            var vehicles = parkingLot.GetVehiclesByColor(color);
            Console.WriteLine(string.Join(", ", vehicles.Select(v => v.LicensePlate)));
        }

        private static void ShowSlotsByColor(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command format. Usage: slot_numbers_for_vehicles_with_colour [color]");
                return;
            }

            var color = parts[1];
            var slots = parkingLot.GetOccupiedSlots()
                .Where(s => s.ParkedVehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.SlotNumber);

            Console.WriteLine(string.Join(", ", slots));
        }

        private static void FindSlotByLicensePlate(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command format. Usage: slot_number_for_registration_number [license_plate]");
                return;
            }

            var licensePlate = parts[1];
            var slotNumber = parkingLot.FindSlotByLicensePlate(licensePlate);

            if (slotNumber.HasValue)
            {
                Console.WriteLine(slotNumber.Value);
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }
    }
}