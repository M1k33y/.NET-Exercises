using ReadingList.src.Infrastructure.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReadingList.tests
{
    public class CsvParserTests
    {
        [Fact]
        public void Parse_ValidLine_ReturnsBook()
        {
            string line = "1,Book,Author,2020,300,fiction,yes,4.5";

            var result = CsvParser.Parse(line);

            Assert.True(result.Ok);
            Assert.Equal(1, result.Value.Id);
            Assert.Equal("Book", result.Value.Title);
            Assert.Equal(4.5, result.Value.Rating);
        }

        [Fact]
        public void Parse_InvalidLine_ReturnsError()
        {
            string line = "invalid,data";

            var result = CsvParser.Parse(line);

            Assert.False(result.Ok);
            Assert.NotNull(result.Error);
        }
    }
}
