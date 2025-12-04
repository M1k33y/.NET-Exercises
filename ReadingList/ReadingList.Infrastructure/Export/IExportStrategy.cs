using ReadingList.src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Export
{
    internal interface IExportStrategy
    {
        Task ExportAsync(IEnumerable<Book> books, string path, CancellationToken token);
    }
}
