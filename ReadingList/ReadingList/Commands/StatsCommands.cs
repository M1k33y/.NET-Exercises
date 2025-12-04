using ReadingList.src.Domain;
using ReadingList.src.Domain.Extensions;
using ReadingList.src.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.App.Commands
{
    internal class StatsCommands
    {
        public static void Handle(IRepository<Book> repo)
        {
            var books = repo.All().ToList();

            int total = books.Count;
            int finished = books.Count(b => b.Finished);
            double avgRating = books.AverageRatingOrDefault();

            Console.WriteLine($"Total books: {total}");
            Console.WriteLine($"Finished: {finished}");
            Console.WriteLine($"Average rating: {avgRating:F2}");
            Console.WriteLine();

            Console.WriteLine("Pages by genre:");
            foreach (var g in books.GroupBy(b => b.Genre))
                Console.WriteLine($"  {g.Key}: {g.Sum(b => b.Pages)} pages");

            Console.WriteLine();
            Console.WriteLine("Top 3 authors:");
            foreach (var a in books.GroupBy(b => b.Author)
                                   .OrderByDescending(g => g.Count())
                                   .Take(3))
                Console.WriteLine($"  {a.Key}: {a.Count()} books");
        }
    }
}
