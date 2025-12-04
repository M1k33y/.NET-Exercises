using ReadingList.src.Domain;
using ReadingList.src.Infrastructure.Import;
using ReadingList.src.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReadingList.tests
{
    public class ImportServiceTests
    {
        [Fact]
        public async Task Import_TwoFiles_CountsCorrectly()
        {
            // Arrange
            var repo = new InMemoryRepository<Book, int>(b => b.Id);
            var service = new ImportService(repo);

            // Cream două fișiere temporare
            var f1 = Path.GetTempFileName();
            var f2 = Path.GetTempFileName();

            await File.WriteAllLinesAsync(f1, new[]
            {
            "Id,Title,Author,Year,Pages,Genre,Finished,Rating",
            "1,Book1,A,2020,100,fiction,yes,4",
            "2,Book2,B,2021,200,fiction,no,3"
        });

            await File.WriteAllLinesAsync(f2, new[]
            {
            "Id,Title,Author,Year,Pages,Genre,Finished,Rating",
            "2,Book2,B,2021,200,fiction,no,3",   // duplicate
            "invalid,line"                      // malformed
        });

            // Act
            var result = await service.ImportAsync(new[] { f1, f2 }, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Imported);
            Assert.Equal(1, result.Duplicates);
            Assert.Equal(1, result.Malformed);
        }
    }
}
