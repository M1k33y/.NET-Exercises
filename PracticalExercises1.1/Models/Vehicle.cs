using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._1.Models
{
    public class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public virtual void StartEngine()
        {
            Console.WriteLine("The vehicle engine starts");
        }
    }
}
