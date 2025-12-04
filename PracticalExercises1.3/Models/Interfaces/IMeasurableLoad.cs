using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._3.Models.Interfaces
{
    internal interface IMeasurableLoad
    {
        double CurrentWatts { get; }
        double TotalWh { get; }
        void ResetEnergy();
    }
}
