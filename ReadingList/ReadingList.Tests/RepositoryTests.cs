using ReadingList.src.Domain;
using ReadingList.src.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReadingList.tests
{
    public class RepositoryTests
    {
        [Fact]
        public void Add_NewItem_ReturnsTrue()
        {
            var repo = new InMemoryRepository<Book, int>(b => b.Id);
            var b = new Book { Id = 1, Title = "A" };

            Assert.True(repo.Add(b));
        }

        [Fact]
        public void Add_Duplicate_ReturnsFalse()
        {
            var repo = new InMemoryRepository<Book, int>(b => b.Id);
            var b1 = new Book { Id = 1, Title = "A" };
            var b2 = new Book { Id = 1, Title = "B" };

            Assert.True(repo.Add(b1));
            Assert.False(repo.Add(b2));
        }

        [Fact]
        public void TryGet_ReturnsCorrectItem()
        {
            var repo = new InMemoryRepository<Book, int>(b => b.Id);
            var b = new Book { Id = 10 };

            repo.Add(b);

            Assert.True(repo.TryGet(10, out var result));
            Assert.Equal(10, result.Id);
        }
    }
}
