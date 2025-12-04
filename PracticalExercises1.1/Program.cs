using PracticalExercises1._1.Models;
using PracticalExercises1._1.Interfaces;

namespace PracticalExercises1._1
{
    internal class Program
    {
        static List<Vehicle> vehicles = new();

        static void Main(string[] args)
        {
            bool running = true;

            while(running)
            {
                Console.WriteLine();
                Console.WriteLine("Vehicle Management System");
                Console.WriteLine("1. Add Car");
                Console.WriteLine("2. Add Motorcycle");
                Console.WriteLine("3. Add Truck");
                Console.WriteLine("4. List Vehicles");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddCar();
                        break;
                    case "2":
                        AddMotorcycle();
                        break;
                    case "3":
                        AddTruck();
                        break;
                    case "4":
                        ListVehicles();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            }
        }

        static void AddCar()
        {
            Console.Write("Brand: "); string brand = Console.ReadLine()!;
            Console.Write("Model: "); string model = Console.ReadLine()!;
            Console.Write("Year: "); int year = int.Parse(Console.ReadLine()!);
            Console.Write("Number of doors: "); int doors = int.Parse(Console.ReadLine()!);

            vehicles.Add(new Car
            {
                Brand = brand,
                Model = model,
                Year = year,
                NumberOfDoors = doors
            });

            Console.WriteLine("Car added!");
        }

        static void AddMotorcycle()
        {
            Console.Write("Brand: "); string brand = Console.ReadLine()!;
            Console.Write("Model: "); string model = Console.ReadLine()!;
            Console.Write("Year: "); int year = int.Parse(Console.ReadLine()!);
            Console.Write("Has sidecar (true/false): "); bool sidecar = bool.Parse(Console.ReadLine()!);

            vehicles.Add(new Motorcycle
            {
                Brand = brand,
                Model = model,
                Year = year,
                HasSidecar = sidecar
            });

            Console.WriteLine("Motorcycle added!");
        }

        static void AddTruck()
        {
            Console.Write("Brand: "); string brand = Console.ReadLine()!;
            Console.Write("Model: "); string model = Console.ReadLine()!;
            Console.Write("Year: "); int year = int.Parse(Console.ReadLine()!);
            Console.Write("Cargo capacity (tons): "); double cap = double.Parse(Console.ReadLine()!);

            vehicles.Add(new Truck
            {
                Brand = brand,
                Model = model,
                Year = year,
                CargoCapacity = cap
            });

            Console.WriteLine("Truck added!");
        }

        static void ListVehicles()
        {
            if (!vehicles.Any())
            {
                Console.WriteLine("No vehicles available.");
                return;
            }

            foreach (var v in vehicles)
            {
                Console.WriteLine($"\n--- {v.GetType().Name} ---");
                Console.WriteLine($"Brand: {v.Brand}");
                Console.WriteLine($"Model: {v.Model}");
                Console.WriteLine($"Year: {v.Year}");

                switch (v)
                {
                    case Car c:
                        Console.WriteLine($"Doors: {c.NumberOfDoors}");
                        break;
                    case Motorcycle m:
                        Console.WriteLine($"Has sidecar: {m.HasSidecar}");
                        break;
                    case Truck t:
                        Console.WriteLine($"Cargo capacity: {t.CargoCapacity} tons");
                        break;
                }

                
                v.StartEngine();

                if (v is IDrivable d)
                    d.Drive();
            }
        }


    }
}