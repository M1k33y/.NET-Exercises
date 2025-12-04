using ReadingList.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Export
{
    internal class CsvExportStrategy:IExportStrategy
    {
        public async Task ExportAsync(IEnumerable<Book> books, string path, CancellationToken token)
        {
            await using var writer = new StreamWriter(
                path,
                false,
                Encoding.UTF8);

            // header
            await writer.WriteLineAsync("Id,Title,Author,Year,Pages,Genre,Finished,Rating");

            foreach (var b in books)
            {
                token.ThrowIfCancellationRequested();

                string line =
                    $"{b.Id}," +
                    $"\"{b.Title}\"," +
                    $"\"{b.Author}\"," +
                    $"{b.Year}," +
                    $"{b.Pages}," +
                    $"{b.Genre}," +
                    $"{(b.Finished ? "yes" : "no")}," +
                    $"{b.Rating}";

                await writer.WriteLineAsync(line);
            }
        }
    }
}
