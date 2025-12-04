using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._4.Models.Interfaces
{
    internal interface IOverdraftPolicy
    {
        decimal OverdraftLimit {  get; }
    }
}
