using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._2.Models
{
    internal class Cat:Animal
    {
        public bool IsIndoor { get; set; }
        public override void Speak()
        {
            Console.WriteLine("Meow!");
        }
        public override decimal DailyCareCost()
        {
            return base.DailyCareCost() + 2m;
        }
        public override void Feed()
        {
            Console.WriteLine($"Cat { Name } has been fed.");
        }

    }
}
