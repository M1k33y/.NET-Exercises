using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadingList.src.Domain;
using Xunit;
namespace ReadingList.tests
{
    

    public class BookTests
    {
        [Fact]
        public void SetRating_Valid_SetsValue()
        {
            var b = new Book();
            b.SetRating(4.5);
            Assert.Equal(4.5, b.Rating);
        }

        [Fact]
        public void SetRating_Invalid_Throws()
        {
            var b = new Book();
            Assert.Throws<ArgumentOutOfRangeException>(() => b.SetRating(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => b.SetRating(6));
        }
    }
}
