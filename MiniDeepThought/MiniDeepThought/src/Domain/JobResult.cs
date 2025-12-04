using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Domain
{
    internal class JobResult
    {
        public string Answer { get; set; } = "";
        public string Summary { get; set; } = "";
        public long DurationMs { get; set; }
    }
}
