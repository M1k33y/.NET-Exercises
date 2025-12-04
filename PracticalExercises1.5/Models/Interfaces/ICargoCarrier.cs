using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._5.Models.Interfaces
{
    internal interface ICargoCarrier
    {
        double CapacityKg { get; }
        double CurrentLoadKg {  get; }

        bool Load(double kg);
        void UnloadAll();
    }
}
