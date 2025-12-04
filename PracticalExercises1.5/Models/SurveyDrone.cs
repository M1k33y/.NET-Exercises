using PracticalExercises1._5.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PracticalExercises1._5.Models
{
    internal class SurveyDrone: Drone, INavigable, IPhotoCapture
    {
        public (double lat, double lon)? CurrentWaypoint { get; private set; }
        public int PhotoCount { get; private set; }

        public SurveyDrone(string name) : base(name) { }

        public void SetWaypoint(double lat, double lon)
        {
            CurrentWaypoint = (lat, lon);
            Console.WriteLine($"{Name} waypoint set to ({lat}, {lon}).");
        }

        public void TakePhoto()
        {
            if (!IsAirborne)
            {
                Console.WriteLine("Cannot take photo. Drone must be airborne.");
                return;
            }
            PhotoCount++;
            Console.WriteLine($"{Name} took a photo. Total: {PhotoCount}");
        }
    }
}
