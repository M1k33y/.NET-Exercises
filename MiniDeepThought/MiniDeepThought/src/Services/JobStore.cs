using MiniDeepThought.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Services
{
    internal class JobStore
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly object _fileLock = new object();
        public List<Job> Jobs { get; private set; } = new();

        public JobStore(string filePath = "deepthought-jobs.json")
        {
            _filePath = filePath;

            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Load();
        }

        public void Add(Job job)
        {
            Jobs.Add(job);
            Save();
        }

        public void Update(Job job)
        {
            lock (_fileLock)
            {
                Save();
            }
        }

        public Job? GetById(Guid id)
        {
            return Jobs.Find(j => j.JobId == id);
        }

        public IEnumerable<Job> ListAll()
        {
            return Jobs;
        }

        private void Load()
        {
            if (!File.Exists(_filePath))
            {
                Jobs = new List<Job>();
                return;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                Jobs = JsonSerializer.Deserialize<List<Job>>(json, _jsonOptions)
                       ?? new List<Job>();
            }
            catch
            {
                // If corrupted, recover gracefully
                Jobs = new List<Job>();
            }
        }

        public void Save()
        {
            lock (_fileLock)
            {
                string json = JsonSerializer.Serialize(Jobs, _jsonOptions);
                File.WriteAllText(_filePath, json);
            }
        }

    }
}
