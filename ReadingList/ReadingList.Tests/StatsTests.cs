using ReadingList.src.Domain;
using System;
using System.Collections.Generic;
using ReadingList.src.Domain.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReadingList.tests
{
    public class StatsTests
    {
        [Fact]
        public void AverageRating_Empty_ReturnsZero()
        {
            var list = new List<Book>();
            Assert.Equal(0, list.AverageRatingOrDefault());
        }

        [Fact]
        public void AverageRating_ComputesCorrectly()
        {
            var list = new List<Book>
        {
            new Book { },
            new Book { }
        };
            list[0].SetRating(4);
            list[1].SetRating(2);

            Assert.Equal(3, list.AverageRatingOrDefault());
        }
    }
}
