using MiniDeepThought.src.Domain;
using MiniDeepThought.src.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Services
{
    internal class JobRunner
    {
        private readonly JobStore _store;
        private readonly Dictionary<string, IAnswerStrategy> _strategies;
        private CancellationTokenSource? _cts;
        private bool _isRunning;

        public JobRunner(JobStore store, IEnumerable<IAnswerStrategy> strategies)
        {
            _store = store;
            _strategies = new Dictionary<string, IAnswerStrategy>();

            foreach (var s in strategies)
                _strategies[s.Key] = s;
        }

        public bool IsRunning => _isRunning;

        public Job? CurrentJob { get; private set; }

        public async Task RunJobAsync(Job job)
        {
            if (!_strategies.ContainsKey(job.AlgorithmKey))
                throw new InvalidOperationException($"Unknown strategy '{job.AlgorithmKey}'.");

            var strategy = _strategies[job.AlgorithmKey];

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _isRunning = true;
            CurrentJob = job;

            job.Status = JobStatus.Running;
            job.Progress = 0;
            _store.Update(job);

            var progress = new Progress<int>(pct =>
            {
                job.Progress = pct;
                _store.Update(job);
            });

            var sw = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var result = await strategy.ComputeAsync(progress, token);

                sw.Stop();

                job.Status = JobStatus.Completed;
                job.CompletedUtc = DateTime.UtcNow;
                job.Result = result;
                _store.Update(job);
            }
            catch (OperationCanceledException)
            {
                sw.Stop();

                job.Status = JobStatus.Canceled;
                job.CompletedUtc = DateTime.UtcNow;
                _store.Update(job);
            }
            finally
            {
                _isRunning = false;
                CurrentJob = null;
            }
        }

        public void Cancel()
        {
            if (!_isRunning || _cts == null)
                return;

            _cts.Cancel();
        }
    }
}
