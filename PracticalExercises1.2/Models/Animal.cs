using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._2.Models
{
    internal abstract class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime IntakeDate { get; set; }

        public abstract void Speak();
        public virtual decimal DailyCareCost()
        {
            return 5m; 
        }
        public abstract void Feed();

    }
}
