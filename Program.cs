﻿﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace parking_system
{
    class Program
    {
        static Dictionary<int, Vehicle> parkingLot = new Dictionary<int, Vehicle>();
        static int totalLots = 0;

        static void Main(string[] args)
        {

            Console.Clear();
            Console.WriteLine("Welcome to Parking Menu");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Create Parking Lot");
            Console.WriteLine("2) Park Vehicle");
            Console.WriteLine("3) Leave Parking Lot");
            Console.WriteLine("4) Get Parking Status");
            Console.WriteLine("5) Count Vehicles By Type");
            Console.WriteLine("6) Get Vehicles By Odd Plate");
            Console.WriteLine("7) Get Vehicles By Even Plate");
            Console.WriteLine("8) Get Vehicles By Color");
            Console.WriteLine("9) Get Slot By Color");
            Console.WriteLine("10) Get Report");
            Console.WriteLine("0) Exit");
            
            
        while (true)
            {
                Console.Write("\r\nSelect an option: ");
                string input = Console.ReadLine();
                string[] command = input.Split(' ');

                switch (command[0].ToLower())
                {
                    case "1":
                        CreateParkingLot(command);
                        break;
                    case "2":
                        ParkVehicle(command);
                        break;
                    case "3":
                        LeaveParkingLot(command);
                        break;
                    case "4":
                        GetParkingStatus();
                        break;
                    case "5":
                        CountVehiclesByType(command);
                        break;
                    case "6":
                        GetVehiclesByOddPlate();
                        break;
                    case "7":
                        GetVehiclesByEvenPlate(command);
                        break;
                    case "8":
                        GetVehiclesByColor(command);
                        break;
                    case "9":
                        GetSlotsByColor(command);
                        break;
                    case "10":
                        GetParkingReport();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }

        static void CreateParkingLot(string[] command)
        {
            if (int.TryParse(command[1], out totalLots))
            {
                Console.WriteLine($"Created a parking lot with {totalLots} slots");
            }
            else
            {
                PrintErrorMessage("Invalid number of slots");
            }
        }
        class Vehicle
        {
            public string Plate { get; }
            public string Color { get; }
            public string Type { get; }

            public Vehicle(string plate, string color, string type)
            {
                Plate = plate;
                Color = color;
                Type = type;
            }
        }

        static void ParkVehicle(string[] command)
        {
            if (totalLots == 0)
            {
                PrintErrorMessage("Parking lot is not created");
                return;
            }

            if (parkingLot.Count < totalLots)
            {
                if (parkingLot.Any(v => v.Value.Plate.Equals(command[1], StringComparison.OrdinalIgnoreCase)))
                {
                    PrintErrorMessage("Vehicle is already parked");
                    return;
                }

                int slotNumber = 0;
                for (int i = 1; i <= totalLots; i++)
                {
                    if (!parkingLot.ContainsKey(i))
                    {
                        slotNumber = i;
                        break;
                    }
                }

                string plate = command[1];
                string color = command[2];
                string type = command[3];
                
                if (!IsValidVehicleType(type))
                {
                    PrintErrorMessage("Invalid type of vehicle");
                    return;
                }

                parkingLot.Add(slotNumber, new Vehicle(plate, color, type));
                Console.WriteLine($"Allocated slot number: {slotNumber}");
            }
            else
            {
                PrintErrorMessage("Sorry, parking lot is full");
            }
        }

        static void LeaveParkingLot(string[] command)
        {
            int slotNumber = int.Parse(command[1]);
            if (parkingLot.ContainsKey(slotNumber))
            {
                parkingLot.Remove(slotNumber);
                Console.WriteLine($"Slot number {slotNumber} is free");
            }
            else
            {
                PrintErrorMessage($"Slot number {slotNumber} not found");
            }
        }

        static void GetParkingStatus()
        {
            Console.WriteLine("Slot No.  Type  Plate No.  Color");
            foreach (var kvp in parkingLot)
            {
                Console.WriteLine($"{kvp.Key,-9} {kvp.Value.Type,-7} {kvp.Value.Plate,-17} {kvp.Value.Color}");
            }
        }

        static void CountVehiclesByType(string[] command)
        {
            string type = command[1];
            int count = parkingLot.Count(v => v.Value.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(count);
        }

        static void GetVehiclesByOddPlate()
        {
            var oddPlates = parkingLot.Where(v => v.Value.Plate.Split('-')[1].Last() % 2 != 0)
                .Select(v => v.Value.Plate);
            Console.WriteLine(string.Join(", ", oddPlates));
        }

        static void GetVehiclesByEvenPlate(string[] command)
        {
            var evenPlates = parkingLot.Where(v => v.Value.Plate.Split('-')[1].Last() % 2 == 0)
                .Select(v => v.Value.Plate);
            Console.WriteLine(string.Join(", ", evenPlates));
        }

        static void GetVehiclesByColor(string[] command)
        {
            string color = command[1];
            var vehicles = parkingLot.Where(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(v => v.Value.Plate);
            if (vehicles.Count() == 0)
            {
                PrintErrorMessage("Not found");
                return;
            }

            Console.WriteLine(string.Join(", ", vehicles));
        }

        static void GetSlotsByColor(string[] command)
        {
            string color = command[1];
            var slots = parkingLot.Where(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(v => v.Key.ToString());
            if (slots.Count() == 0)
            {
                PrintErrorMessage("Not found");
                return;
            }

            Console.WriteLine(string.Join(", ", slots));
        }

        static void GetParkingReport()
        {
            int occupiedSlots = parkingLot.Count;
            Console.WriteLine($"Number of occupied slots: {occupiedSlots}");

            int availableSlots = totalLots - occupiedSlots;
            Console.WriteLine($"Number of available slots: {availableSlots}");

            int oddPlates = parkingLot.Count(v => v.Value.Plate.Split('-')[1].Last() % 2 != 0);
            Console.WriteLine($"Number of vehicles with odd registration number: {oddPlates}");

            int evenPlates = parkingLot.Count(v => v.Value.Plate.Split('-')[1].Last() % 2 == 0);
            Console.WriteLine($"Number of vehicles with even registration number: {evenPlates}");

            int motor = parkingLot.Count(v => v.Value.Type.Equals("Motor", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Number of Motor vehicles: {motor}");
            int mobil = parkingLot.Count(v => v.Value.Type.Equals("Mobil", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Number of Mobil vehicles: {mobil}");

            var colors = parkingLot.GroupBy(v => v.Value.Color)
                .Select(v => new { Color = v.Key, Count = v.Count() });
            foreach (var color in colors)
            {
                Console.WriteLine($"Number of {color.Color} vehicles: {color.Count}");
            }
        }
        
        static void PrintErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
        
        static bool IsValidVehicleType(string type)
        {
            return type.Equals("Mobil", StringComparison.OrdinalIgnoreCase) ||
                   type.Equals("Motor", StringComparison.OrdinalIgnoreCase);
        }
    }
}