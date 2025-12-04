using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalExercises1._1.Interfaces;
namespace PracticalExercises1._1.Models
{
    internal class Motorcycle:Vehicle, IDrivable
    {
        public bool HasSidecar { get; set; }
        public override void StartEngine()
        {
            Console.WriteLine("The motorcycle engine starts with a button");
        }
        public void Drive()
        {
            Console.WriteLine("The motorcycle is driving on the road");
        }
    }
}
