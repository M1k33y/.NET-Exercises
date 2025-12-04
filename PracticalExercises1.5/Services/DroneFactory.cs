using PracticalExercises1._5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._5.Services
{
    internal class DroneFactory
    {
        public static Drone? Create(string type, string name)
        {
            return type.ToLower() switch
            {
                "survey" => new SurveyDrone(name),
                "delivery" => new DeliveryDrone(name),
                "racing" => new RacingDrone(name),
                _ => null
            };
        }
    }
}
