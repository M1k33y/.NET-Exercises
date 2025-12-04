using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._5.Models.Interfaces
{
    internal interface INavigable
    {
        (double lat,double lon)? CurrentWaypoint {  get; }
        void SetWaypoint(double lat,double lon);
    }
}
