using PracticalExercises1._3.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._3.Model
{
    public abstract class SmartDevice:IPowerSwitch,ISelfTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOn {  get; set; }

        protected SmartDevice(string name) 
        {
            Name = name;
        }

        public void PowerOn()
        {
            IsOn = true;
        }
        public void PowerOff() 
        {

            IsOn = false; 
        }

        public abstract bool SelfTest();
        public virtual string GetStatus()
        {
            return $"{Name} (Id={Id}) - Power: {(IsOn ? "On" : "Off")}";
        }
    }
}
