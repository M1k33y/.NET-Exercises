using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Repositories
{
    internal interface IRepository<T>
    {
        bool Add(T item);
        bool TryGet(object key,out T? item);
        IEnumerable<T> All();
    }
}
