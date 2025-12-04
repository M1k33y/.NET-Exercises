using ReadingList.src.Infrastructure.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.App.Commands
{
    internal class ExportCommands
    {
        public static async Task HandleAsync(
        ExportService service,
        IEnumerable<string> args,
        CancellationToken token)
        {
            var list = args.ToList();
            if (list.Count < 2)
            {
                Console.WriteLine("Usage: export json <path> | export csv <path>");
                return;
            }

            string type = list[0].ToLower();
            string path = list[1];

            IExportStrategy strategy = type switch
            {
                "json" => new JsonExportStrategy(),
                "csv" => new CsvExportStrategy(),
                _ => null!
            };

            if (strategy is null)
            {
                Console.WriteLine("Unknown export type. Use json or csv.");
                return;
            }

            await service.ExportAsync(strategy, path, token);
        }
    }
}
