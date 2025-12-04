using ReadingList.Infrastructure.Parsing;
using ReadingList.Infrastructure.Repositories;
using ReadingList.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Import
{
    internal class ImportService
    {
        private readonly IRepository<Book> _repository;

        public ImportService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<ImportResult> ImportAsync(
            IEnumerable<string> filePaths,
            CancellationToken cancellationToken)
        {
            var result = new ImportResult();

            await Parallel.ForEachAsync(filePaths, cancellationToken, async (file, ct) =>
            {
                if (ct.IsCancellationRequested)
                    return;

                if (!File.Exists(file))
                {
                    Console.WriteLine($"Warning: File '{file}' not found.");
                    return;
                }

                string[] lines;

                try
                {
                    lines = await File.ReadAllLinesAsync(file, ct);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file '{file}': {ex.Message}");
                    return;
                }

                foreach (var line in lines.Skip(1)) // skip header
                {
                    if (ct.IsCancellationRequested)
                        return;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parseResult = CsvParser.Parse(line);

                    if (!parseResult.Ok)
                    {
                        result.IncrementMalformed();
                        Console.WriteLine($"Malformed row in '{file}': {parseResult.Error}");
                        continue;
                    }

                    var book = parseResult.Value!;

                    if (_repository.Add(book))
                        result.IncrementImported();
                    else
                        result.IncrementDuplicates();
                }
            });

            return result;
        }
    }
}
