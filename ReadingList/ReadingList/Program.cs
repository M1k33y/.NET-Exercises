using ReadingList;
using ReadingList.App;
using ReadingList.src.App;
using ReadingList.src.Domain;
using ReadingList.src.Infrastructure.Export;
using ReadingList.src.Infrastructure.Import;
using ReadingList.src.Infrastructure.Repositories;

class Program
{
    static async Task Main()
    {
        var cts = new CancellationTokenSource();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
            Console.WriteLine("\nCancellation requested...");
        };

        var repo = new InMemoryRepository<Book, int>(b => b.Id);
        var importService = new ImportService(repo);
        var exportService = new ExportService(repo);

        var router = new CommandRouter(repo, importService, exportService);

        Console.WriteLine("Reading List App - Type 'help' for commands.");
        Console.WriteLine();

        await CommandLoop.RunAsync(router, cts.Token);
    }
}
