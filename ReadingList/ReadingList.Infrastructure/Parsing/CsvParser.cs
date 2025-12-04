using ReadingList.src.Domain;
using ReadingList.src.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Parsing
{
    internal static class CsvParser
    {
        private static readonly CsvLineSplitter _splitter = new();

        public static Result<Book> Parse(string line)
        {
            try
            {
                var parts = _splitter.Split(line);

                if (parts.Length < 8)
                    return Result<Book>.Fail("Not enough fields in CSV row.");

                var book = new Book
                {
                    Id = int.Parse(parts[0]),
                    Title = parts[1],
                    Author = parts[2],
                    Year = int.Parse(parts[3]),
                    Pages = int.Parse(parts[4]),
                    Genre = parts[5],
                };

                // Finished field
                var finishedRaw = parts[6].NormalizeInput();
                book.MarkFinishedIf(finishedRaw is "y" or "yes" or "true" or "1");

                // Rating
                if (double.TryParse(parts[7], out double rating))
                    book.SetRating(rating);

                return Result<Book>.Success(book);
            }
            catch (Exception ex)
            {
                return Result<Book>.Fail($"CSV parse error: {ex.Message}");
            }
        }

        private static void MarkFinishedIf(this Book book, bool finished)
        {
            if (finished)
                book.MarkFinished();
        }
    }
}
