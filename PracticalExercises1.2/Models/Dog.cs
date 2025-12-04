using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._2.Models
{
    internal class Dog:Animal
    {
        public bool IsTrained { get; set; }
        public override void Speak()
        {
            Console.WriteLine("Woof!");
        }
        public override decimal DailyCareCost()
        {
            return base.DailyCareCost() + 3m;
        }
        public override void Feed()
        {
            Console.WriteLine($"Dog { Name }  has been fed.");
        }
    }
}
