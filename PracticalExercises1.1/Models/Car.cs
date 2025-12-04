using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalExercises1._1.Interfaces;
namespace PracticalExercises1._1.Models
{
    internal class Car :Vehicle, IDrivable
    {
        public int NumberOfDoors { get; set; }
        public override void StartEngine()
        {
            Console.WriteLine("The car engine starts with a key");
        }
        public void Drive()
        {
            Console.WriteLine("The car is driving on the road");
        }
    }
}
