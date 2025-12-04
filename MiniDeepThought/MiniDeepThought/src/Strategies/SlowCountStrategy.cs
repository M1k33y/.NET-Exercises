using MiniDeepThought.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Strategies
{
    internal class SlowCountStrategy:IAnswerStrategy
    {
        public string Key => "SlowCount";

        private readonly int _steps;
        private readonly int _delayMs;

        // Default: 100 steps, -50ms per step
        public SlowCountStrategy(int steps = 100, int delayMs = 50)
        {
            _steps = steps;
            _delayMs = delayMs;
        }

        public async Task<JobResult> ComputeAsync(
            IProgress<int> progress,
            CancellationToken token)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 1; i <= _steps; i++)
            {
                token.ThrowIfCancellationRequested();

                int pct = (int)((i / (double)_steps) * 100);
                progress.Report(pct);

                await Task.Delay(_delayMs, token);
            }

            sw.Stop();

            return new JobResult
            {
                Answer = "42",
                Summary = "Counted thoughtfully to discover the answer.",
                DurationMs = sw.ElapsedMilliseconds
            };
        }
    }
}
