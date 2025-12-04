using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Util
{
    internal class ConsoleHelpers
    {
        public static string PromptString(string message, int minLength, int maxLength)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Value cannot be empty.");
                    continue;
                }

                if (input.Length < minLength || input.Length > maxLength)
                {
                    Console.WriteLine($"Value must be between {minLength} and {maxLength} characters.");
                    continue;
                }

                return input;
            }
        }

        public static string PromptAlgorithm()
        {
            while (true)
            {
                Console.Write("Algorithm [Trivial | SlowCount | RandomGuess]: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a valid algorithm.");
                    continue;
                }

                string normalized = input.Trim();

                if (normalized.Equals("Trivial", StringComparison.OrdinalIgnoreCase)
                    || normalized.Equals("SlowCount", StringComparison.OrdinalIgnoreCase)
                    || normalized.Equals("RandomGuess", StringComparison.OrdinalIgnoreCase))
                {
                    return normalized;
                }

                Console.WriteLine("Unknown algorithm. Try again.");
            }
        }

        public static Guid PromptGuid(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine()?.Trim();

                if (Guid.TryParse(input, out Guid id))
                    return id;

                Console.WriteLine("Invalid GUID format. Try again.");
            }
        }

        public static bool KeyPressed(ConsoleKey key)
        {
            if (!Console.KeyAvailable)
                return false;

            var pressed = Console.ReadKey(intercept: true).Key;
            return pressed == key;
        }
    }
}
