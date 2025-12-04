using MiniDeepThought.src.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniDeepThought.tests
{
    public class TrivialStrategyTests
    {
        [Fact]
        public async Task TrivialStrategy_Returns42_AndReportsProgress()
        {
            // Arrange
            var strategy = new TrivialStrategy();
            int lastProgress = 0;

            var progress = new Progress<int>(p => lastProgress = p);

            // Act
            var result = await strategy.ComputeAsync(progress, CancellationToken.None);

            // Assert
            Assert.Equal("42", result.Answer);
            Assert.Equal(100, lastProgress);
            Assert.True(result.DurationMs >= 0);
        }
    }

}
