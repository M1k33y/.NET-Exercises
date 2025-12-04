using PracticalExercises1._2.Models;
using PracticalExercises1._2.Interfaces;

namespace PracticalExercises1._2
{
    internal class Program
    {
        static List<Animal> animals = new();
        static int nextId = 1;

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Pet Shelter Menu ---");
                Console.WriteLine("1) Add Dog");
                Console.WriteLine("2) Add Cat");
                Console.WriteLine("3) Add Bird");
                Console.WriteLine("4) List Animals");
                Console.WriteLine("5) Feed All");
                Console.WriteLine("6) Speak All");
                Console.WriteLine("7) Adopt (by Id)");
                Console.WriteLine("8) Exit");
                Console.Write("Choose option: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddDog(); break;
                    case "2": AddCat(); break;
                    case "3": AddBird(); break;
                    case "4": ListAnimals(); break;
                    case "5": FeedAll(); break;
                    case "6": SpeakAll(); break;
                    case "7": Adopt(); break;
                    case "8": running = false; break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        // ------------------- Add Dog -------------------
        static void AddDog()
        {
            Console.Write("Name: ");
            string name = ReadNonEmptyString();

            int age = ReadInt("Age (>= 0): ", min: 0);

            bool trained = ReadBool("Is trained (y/n): ");

            Dog dog = new()
            {
                Id = nextId++,
                Name = name,
                Age = age,
                IsTrained = trained
            };

            animals.Add(dog);
            Console.WriteLine("Dog added.");
        }

        // ------------------- Add Cat -------------------
        static void AddCat()
        {
            Console.Write("Name: ");
            string name = ReadNonEmptyString();

            int age = ReadInt("Age (>= 0): ", min: 0);

            bool indoor = ReadBool("Is indoor (y/n): ");

            Cat cat = new()
            {
                Id = nextId++,
                Name = name,
                Age = age,
                IsIndoor = indoor
            };

            animals.Add(cat);
            Console.WriteLine("Cat added.");
        }

        // ------------------- Add Bird -------------------
        static void AddBird()
        {
            Console.Write("Name: ");
            string name = ReadNonEmptyString();

            int age = ReadInt("Age (>= 0): ", min: 0);

            double wing = ReadDouble("Wing span (cm > 0): ", min: 0.1);

            Bird bird = new()
            {
                Id = nextId++,
                Name = name,
                Age = age,
                WingSpanCm = wing
            };

            animals.Add(bird);
            Console.WriteLine("Bird added.");
        }

        // ------------------- List -------------------
        static void ListAnimals()
        {
            if (!animals.Any())
            {
                Console.WriteLine("No animals in shelter.");
                return;
            }

            Console.WriteLine("Id | Type | Name | Age | Extra | DailyCost");

            foreach (var a in animals)
            {
                string type = a.GetType().Name;
                string extra = type switch
                {
                    "Dog" => $"Trained: {((Dog)a).IsTrained}",
                    "Cat" => $"Indoor: {((Cat)a).IsIndoor}",
                    "Bird" => $"WingSpan: {((Bird)a).WingSpanCm} cm",
                    _ => ""
                };

                Console.WriteLine($"{a.Id} | {type} | {a.Name} | {a.Age} | {extra} | {a.DailyCareCost():0.00}");
            }
        }

        // ------------------- Feed All -------------------
        static void FeedAll()
        {
            int count = 0;
            foreach (var a in animals)
            {
                a.Feed();
                count++;
            }

            Console.WriteLine($"\nFed {count} animals.");
        }

        // ------------------- Speak All -------------------
        static void SpeakAll()
        {
            foreach (var a in animals)
            {
                Console.Write($"{a.Name} says: ");
                a.Speak();

                if (a is IFlyable f)
                {
                    f.Fly();
                }
            }
        }

        // ------------------- Adopt -------------------
        static void Adopt()
        {
            int id = ReadInt("Enter Id to adopt: ", min: 1);

            var animal = animals.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                Console.WriteLine("Animal not found.");
                return;
            }

            animals.Remove(animal);
            Console.WriteLine($"{animal.Name} has been adopted!");
        }

        // ------------------- Input helpers -------------------

        static string ReadNonEmptyString()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.Write("Input cannot be empty. Try again: ");
            }
        }

        static int ReadInt(string prompt, int min)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min)
                    return value;

                Console.WriteLine("Invalid number. Try again.");
            }
        }

        static double ReadDouble(string prompt, double min)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value > min)
                    return value;

                Console.WriteLine("Invalid number. Try again.");
            }
        }

        static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y") return true;
                if (input == "n") return false;

                Console.WriteLine("Invalid input. Enter y/n.");
            }
        }
    }
}