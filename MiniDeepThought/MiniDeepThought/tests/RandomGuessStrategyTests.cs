using MiniDeepThought.src.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniDeepThought.tests
{
    public class RandomGuessStrategyTests
    {
        [Fact]
        public async Task RandomGuessStrategy_ProducesNumberString()
        {
            // Arrange
            var strategy = new RandomGuessStrategy();
            int lastProgress = 0;

            var progress = new Progress<int>(p => lastProgress = p);

            // Act
            var result = await strategy.ComputeAsync(progress, CancellationToken.None);

            // Assert
            Assert.Equal("42", result.Answer); // The only allowed number
            Assert.Equal(100, lastProgress);
        }
    }
}
