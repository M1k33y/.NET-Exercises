using PracticalExercises1._5.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._5.Models
{
    internal abstract class Drone:ISelfTest,IFlightControl
    {
        private static int _idCounter = 1;
        public int Id { get; }
        public string Name { get; set; }
        public double BatteryPercent { get; private set; } = 100;
        public bool IsAirborne { get; private set; }

        protected Drone(string name)
        {
            Id = _idCounter++;
            Name= name; 
        }

        public virtual bool RunSelfTest()
        {
            bool ok = BatteryPercent >= 20 && !IsAirborne;
            return ok;
        }
        public virtual void TakeOff()
        {
            if (IsAirborne)
            {
                Console.WriteLine($"{Name} is already airborne.");
                return;
            }

            if (BatteryPercent < 20)
            {
                Console.WriteLine($"{Name} has too little battery to take off (≥20% required).");
                return;
            }

            IsAirborne = true;
            BatteryPercent -= 5;
            Console.WriteLine($"{Name} took off. Battery {BatteryPercent:F0}%");
        }
        public virtual void Land()
        {
            if (!IsAirborne)
            {
                Console.WriteLine($"{Name} is already on the ground.");
                return;
            }
            IsAirborne = false;
            Console.WriteLine($"{Name} has landed.");
        }

        public void Charge(double percent)
        {
            BatteryPercent = Math.Clamp(BatteryPercent + percent, 0, 100);
            Console.WriteLine($"{Name} charged. Battery now {BatteryPercent:F0}%.");
        }

        public override string ToString()
            => $"#{Id} {GetType().Name} \"{Name}\" — Battery {BatteryPercent:F0}%, Airborne: {IsAirborne}";

    }
}
