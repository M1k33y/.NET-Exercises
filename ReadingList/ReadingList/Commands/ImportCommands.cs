using ReadingList.src.Infrastructure.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.App.Commands
{
    internal class ImportCommands
    {
        public static async Task HandleAsync(
        ImportService service,
        IEnumerable<string> filePaths,
        CancellationToken token)
        {
            var files = filePaths.ToList();

            if (!files.Any())
            {
                Console.WriteLine("Usage: import file1.csv [file2.csv ...]");
                return;
            }

            var result = await service.ImportAsync(files, token);
            Console.WriteLine(result);
        }
    }
}
