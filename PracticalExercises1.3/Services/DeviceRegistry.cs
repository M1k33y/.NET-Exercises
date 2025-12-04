using PracticalExercises1._3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._3.Services
{
    public class DeviceRegistry
    {
        private readonly List<SmartDevice> _devices = new();
        private int _nextId = 1;

        public IReadOnlyList<SmartDevice> Devices => _devices;

        public SmartDevice? GetById(int id) => _devices.FirstOrDefault(d => d.Id == id);

        public void Add(SmartDevice device)
        {
            device.Id = _nextId++;
            _devices.Add(device);
        }
    }
}
