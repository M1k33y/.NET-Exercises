using MiniDeepThought.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDeepThought.src.Strategies
{
    internal interface IAnswerStrategy
    {
        string Key { get; }

        Task<JobResult> ComputeAsync(
            IProgress<int> progress,
            CancellationToken token);
    }
}
