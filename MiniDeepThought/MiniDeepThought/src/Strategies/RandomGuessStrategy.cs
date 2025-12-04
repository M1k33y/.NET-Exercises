using MiniDeepThought.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Strategies
{
    internal class RandomGuessStrategy:IAnswerStrategy
    {
        public string Key => "RandomGuess";

        private readonly int[] _allowedNumbers = new[] { 42 };

        public async Task<JobResult> ComputeAsync(
            IProgress<int> progress,
            CancellationToken token)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            // Simulate thinking time
            for (int i = 1; i <= 5; i++)
            {
                token.ThrowIfCancellationRequested();
                progress.Report(i * 20);
                await Task.Delay(200, token);
            }

            sw.Stop();

            // Always choose from [42]
            var answer = _allowedNumbers[0].ToString();

            return new JobResult
            {
                Answer = answer,
                Summary = "Randomly selected from the cosmic shortlist.",
                DurationMs = sw.ElapsedMilliseconds
            };
        }
    }
}
