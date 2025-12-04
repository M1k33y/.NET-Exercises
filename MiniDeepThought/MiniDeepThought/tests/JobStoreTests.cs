using MiniDeepThought.src.Domain;
using MiniDeepThought.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniDeepThought.tests
{
    public class JobStoreTests
    {
        [Fact]
        public void JobStore_SavesAndLoads_JobsRoundTrip()
        {
            // Arrange
            string tempFile = Path.GetTempFileName();
            var store1 = new JobStore(tempFile);

            var job = new Job("Test question", "Trivial");
            store1.Add(job);

            // Act
            var store2 = new JobStore(tempFile);
            var loadedJob = store2.GetById(job.JobId);

            // Assert
            Assert.NotNull(loadedJob);
            Assert.Equal(job.JobId, loadedJob!.JobId);
            Assert.Equal("Test question", loadedJob.QuestionText);
            Assert.Equal("Trivial", loadedJob.AlgorithmKey);
        }
    }
}
