using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Domain
{
    internal class Job
    {
        public Guid JobId { get; set; }
        public string QuestionText { get; set; } = "";
        public string AlgorithmKey { get; set; } = "";
        public JobStatus Status { get; set; }
        public int Progress { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? CompletedUtc { get; set; }
        public JobResult? Result { get; set; }

        public Job()
        {
            // Required for JSON serialization
        }

        public Job(string question, string algorithm)
        {
            JobId = Guid.NewGuid();
            QuestionText = question;
            AlgorithmKey = algorithm;
            Status = JobStatus.Pending;
            Progress = 0;
            CreatedUtc = DateTime.UtcNow;
        }
    }
}
