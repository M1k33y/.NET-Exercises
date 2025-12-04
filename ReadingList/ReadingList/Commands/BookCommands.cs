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
    internal class BookCommands
    {
        public static void Handle(IRepository<Book> repo, string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid command. Type 'help'.");
                return;
            }

            var cmd = args[0].ToLower();

            switch (cmd)
            {
                case "list":
                    ListAll(repo);
                    break;

                case "filter":
                    if (args[1] == "finished")
                        ListFinished(repo);
                    break;

                case "top":
                    if (args[1] == "rated" && args.Length >= 3 && int.TryParse(args[2], out int n))
                        TopRated(repo, n);
                    break;

                case "by":
                    if (args[1] == "author" && args.Length >= 3)
                        ByAuthor(repo, string.Join(' ', args.Skip(2)));
                    break;

                case "mark":
                    if (args[1] == "finished" && args.Length >= 3 && int.TryParse(args[2], out int id1))
                        MarkFinished(repo, id1);
                    break;

                case "rate":
                    if (args.Length >= 3 && int.TryParse(args[1], out int id2) &&
                        double.TryParse(args[2], out double rating))
                        Rate(repo, id2, rating);
                    break;

                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        private static void ListAll(IRepository<Book> repo)
        {
            foreach (var b in repo.All())
                Print(b);
        }

        private static void ListFinished(IRepository<Book> repo)
        {
            foreach (var b in repo.All().Where(b => b.Finished))
                Print(b);
        }

        private static void TopRated(IRepository<Book> repo, int n)
        {
            foreach (var b in repo.All().OrderByDescending(b => b.Rating).Take(n))
                Print(b);
        }

        private static void ByAuthor(IRepository<Book> repo, string text)
        {
            foreach (var b in repo.All().Where(b => b.Author.StartsWith(text, StringComparison.OrdinalIgnoreCase)))
                Print(b);
        }

        private static void MarkFinished(IRepository<Book> repo, int id)
        {
            if (!repo.TryGet(id, out var book))
            {
                Console.WriteLine("404 Book not found");
                return;
            }

            book!.MarkFinished();
            Console.WriteLine("200 OK");
        }

        private static void Rate(IRepository<Book> repo, int id, double rating)
        {
            if (!repo.TryGet(id, out var book))
            {
                Console.WriteLine("404 Book not found");
                return;
            }

            try
            {
                book!.SetRating(rating);
                Console.WriteLine("200 OK");
            }
            catch
            {
                Console.WriteLine("400 Bad Request (rating must be 0–5).");
            }
        }

        private static void Print(Book b)
            => Console.WriteLine($"{b.Id} | {b.Title} | {b.Author} | {b.Year} | Finished: {b.Finished} | Rating: {b.Rating}");
    
}
}
