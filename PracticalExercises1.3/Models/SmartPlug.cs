using PracticalExercises1._3.Model;
using PracticalExercises1._3.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._3.Models
{
    public class SmartPlug : SmartDevice, IMeasurableLoad
    {
        private readonly Random rnd = new();

        public double CurrentWatts => rnd.NextDouble() * 1500; // simulate

        public double TotalWh { get; private set; }

        public SmartPlug(string name) : base(name) { }

        public override bool SelfTest() => true;

        public void ResetEnergy() => TotalWh = 0;

        public void AccumulateEnergy()
        {
            if (IsOn)
                TotalWh += CurrentWatts / 3600.0; // per cycle (approx)
        }

        public override string GetStatus()
        {
            return base.GetStatus() + $", Energy: {TotalWh:F2} Wh";
        }
    }
}
