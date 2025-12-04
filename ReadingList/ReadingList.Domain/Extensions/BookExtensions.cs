using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.src.Domain.Extensions
{
    internal static class BookExtensions
    {
        public static double AverageRatingOrDefault(this IEnumerable<Book> books)
        => books.Any() ? books.Average(b => b.Rating) : 0;

        public static IEnumerable<Book> FinishedBooks(this IEnumerable<Book> books)
            => books.Where(b => b.Finished);

        public static IEnumerable<IGrouping<string, Book>> GroupByGenre(this IEnumerable<Book> books)
            => books.GroupBy(b => b.Genre);

    }
}
