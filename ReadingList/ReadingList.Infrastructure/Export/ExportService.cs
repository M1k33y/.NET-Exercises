using ReadingList.Infrastructure.Repositories;
using ReadingList.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Export
{
    internal class ExportService
    {
        private readonly IRepository<Book> _repository;

        public ExportService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task ExportAsync(
            IExportStrategy strategy,
            string path,
            CancellationToken token)
        {
            if (File.Exists(path))
            {
                Console.Write($"File '{path}' exists. Overwrite? (y/n): ");
                var answer = Console.ReadLine()?.Trim().ToLowerInvariant();

                if (answer != "y" && answer != "yes")
                {
                    Console.WriteLine("Export cancelled.");
                    return;
                }
            }

            try
            {
                await strategy.ExportAsync(_repository.All(), path, token);
                Console.WriteLine($"Export completed successfully - {path}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Export cancelled by user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Export error: {ex.Message}");
            }
        }
    }
}
