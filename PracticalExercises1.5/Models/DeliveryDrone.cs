using PracticalExercises1._5.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PracticalExercises1._5.Models
{
    internal class DeliveryDrone:Drone,INavigable,ICargoCarrier
    {
        public double CapacityKg { get; } = 5.0;
        public double CurrentLoadKg { get; private set; }
        public (double lat, double lon)? CurrentWaypoint { get; private set; }

        public DeliveryDrone(string name) : base(name) { }

        public bool Load(double kg)
        {
            if (kg <= 0)
            {
                Console.WriteLine("Weight must be positive.");
                return false;
            }

            if (CurrentLoadKg + kg > CapacityKg)
            {
                Console.WriteLine($"{Name} cannot load {kg} kg. Capacity {CapacityKg} kg.");
                return false;
            }

            CurrentLoadKg += kg;
            Console.WriteLine($"{Name} loaded {kg:F2} kg.");
            return true;
        }

        public void UnloadAll()
        {
            Console.WriteLine($"{Name} unloaded {CurrentLoadKg:F2} kg.");
            CurrentLoadKg = 0;
        }

        public void SetWaypoint(double lat, double lon)
        {
            CurrentWaypoint = (lat, lon);
            Console.WriteLine($"{Name} waypoint set to ({lat}, {lon}).");
        }
    }
}
