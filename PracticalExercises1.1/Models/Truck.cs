using PracticalExercises1._1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._1.Models
{
    internal class Truck:Vehicle,IDrivable
    {
        public double CargoCapacity { get; set; }
        public override void StartEngine()
        {
            Console.WriteLine("The truck engine rumbles to life");
        }
        public void Drive()
        {
            Console.WriteLine("The truck is hauling cargo on the road");
        }
    }
}
