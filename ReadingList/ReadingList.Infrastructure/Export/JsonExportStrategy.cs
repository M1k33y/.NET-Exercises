using ReadingList.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Export
{
    internal class JsonExportStrategy: IExportStrategy
    {
        public async Task ExportAsync(IEnumerable<Book> books, string path, CancellationToken token)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            await using var stream = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            await JsonSerializer.SerializeAsync(stream, books, options, token);
        }
    }
}
