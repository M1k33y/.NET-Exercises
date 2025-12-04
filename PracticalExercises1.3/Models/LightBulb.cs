using PracticalExercises1._3.Model;
using PracticalExercises1._3.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._3.Models
{
    internal class LightBulb:SmartDevice,IDimmable
    {
        public int Brightness { get; private set; } = 50;

        public LightBulb(string name) :base(name) { }

        public void SetBrightness(int value)
        {
            Brightness = Math.Clamp(value, 0, 100);
        }

        public override bool SelfTest()
        {
            return true;
        }

        public override string GetStatus()
        {
            return base.GetStatus()+$"Brightness: {Brightness}";
        }
    }
}
