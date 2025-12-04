using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Repositories
{
    internal class InMemoryRepository<T, TKey> : IRepository<T> where TKey : notnull
    {
        private readonly Func<T, TKey> _keySelector;
        private readonly ConcurrentDictionary<TKey, T> _storage = new();

        public InMemoryRepository(Func<T, TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public bool Add(T item)
        {
            var key = _keySelector(item);
            return _storage.TryAdd(key, item);
        }

        public bool TryGet(object key, out T? item)
        {
            if (key is TKey realKey && _storage.TryGetValue(realKey, out var found))
            {
                item = found;
                return true;
            }

            item = default;
            return false;
        }

        public IEnumerable<T> All() => _storage.Values;
    }
}
