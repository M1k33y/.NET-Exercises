using PracticalExercises1._3.Model;
using PracticalExercises1._3.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._3.Models
{
    internal class Thermostat:SmartDevice,ITemperatureControl
    {
        public double TargetCelsius { get; private set; } = 22;
        public Thermostat(string name) :base(name) { }
        public void SetTarget(double celsius)
        {
            TargetCelsius = Math.Clamp(celsius, 10, 30);
        }
        public override bool SelfTest()
        {
            return true;
        }
        public override string GetStatus()
        {
            return base.GetStatus()+$", Target: {TargetCelsius}C";
        }
    }
}
