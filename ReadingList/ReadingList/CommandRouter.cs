using ReadingList.App.Commands;
using ReadingList.src.Domain;
using ReadingList.src.Infrastructure.Export;
using ReadingList.src.Infrastructure.Import;
using ReadingList.src.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.App
{
    internal class CommandRouter
    {
        private readonly IRepository<Book> _repo;
        private readonly ImportService _importService;
        private readonly ExportService _exportService;

        public CommandRouter(
            IRepository<Book> repo,
            ImportService importService,
            ExportService exportService)
        {
            _repo = repo;
            _importService = importService;
            _exportService = exportService;
        }

        public async Task ExecuteAsync(string input, CancellationToken token)
        {
            var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                return;

            var command = parts[0].ToLower();

            try
            {
                switch (command)
                {
                    case "import":
                        await ImportCommands.HandleAsync(_importService, parts.Skip(1), token);
                        break;

                    case "list":
                    case "filter":
                    case "top":
                    case "by":
                    case "mark":
                    case "rate":
                        BookCommands.Handle(_repo, parts);
                        break;

                    case "stats":
                        StatsCommands.Handle(_repo);
                        break;

                    case "export":
                        await ExportCommands.HandleAsync(_exportService, parts.Skip(1), token);
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("Unknown command. Type 'help'.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine(@"
Commands:
 import file1.csv [file2.csv ...]
 list all
 filter finished
 top rated <n>
 by author <name>
 mark finished <id>
 rate <id> <0-5>
 stats
 export json <path>
 export csv <path>
 help
 exit");
        }
    }
}
