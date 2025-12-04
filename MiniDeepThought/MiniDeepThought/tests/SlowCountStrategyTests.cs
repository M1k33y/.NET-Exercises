using MiniDeepThought.src.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniDeepThought.tests
{
    public class SlowCountStrategyTests
    {
        [Fact]
        public async Task SlowCountStrategy_AdvancesProgress_To100()
        {
            // Arrange
            var strategy = new SlowCountStrategy(steps: 5, delayMs: 1);
            int lastProgress = 0;

            var progress = new Progress<int>(p => lastProgress = p);

            // Act
            var result = await strategy.ComputeAsync(progress, CancellationToken.None);

            // Assert
            Assert.Equal("42", result.Answer);
            Assert.Equal(100, lastProgress);
        }

        [Fact]
        public async Task SlowCountStrategy_HonorsCancellation_BeforeCompletion()
        {
            // Arrange
            var strategy = new SlowCountStrategy(steps: 1000, delayMs: 1);
            int lastProgress = 0;

            var cts = new CancellationTokenSource();
            var progress = new Progress<int>(p => lastProgress = p);

            // Cancel quickly
            cts.CancelAfter(5);

            // Act + Assert
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => strategy.ComputeAsync(progress, cts.Token));

            Assert.True(lastProgress < 100);
        }
    }
}
