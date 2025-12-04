using PracticalExercises1._2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._2.Models
{
    internal class Bird:Animal,IFlyable
    {
        public double WingSpanCm { get; set; }

        public override void Speak()
        {
            Console.WriteLine("Chirp!");
        }
        public override decimal DailyCareCost()
        {
            return base.DailyCareCost() + 1m;
        }
        public override void Feed()
        {
            Console.WriteLine($"Bird { Name } has been fed.");
        }
        public void Fly()
        {
            Console.WriteLine($"Bird { Name } is flying with a wingspan of { WingSpanCm } cm.");
        }
    }
}
