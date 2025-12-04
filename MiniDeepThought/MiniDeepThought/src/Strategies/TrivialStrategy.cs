using MiniDeepThought.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Strategies
{
    internal class TrivialStrategy: IAnswerStrategy
    {
        public string Key => "Trivial";

        public Task<JobResult> ComputeAsync(
            IProgress<int> progress,
            CancellationToken token)
        {
            progress.Report(100);

            return Task.FromResult(
                new JobResult
                {
                    Answer = "42",
                    Summary = "The ultimate answer revealed instantly.",
                    DurationMs = 0
                });
        }
    }
}
