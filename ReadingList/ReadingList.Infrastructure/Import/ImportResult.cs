using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Import
{
    internal class ImportResult
    {
        private int _imported;
        private int _duplicates;
        private int _malformed;

        public int Imported => _imported;
        public int Duplicates => _duplicates;
        public int Malformed => _malformed;

        public void IncrementImported() => Interlocked.Increment(ref _imported);
        public void IncrementDuplicates() => Interlocked.Increment(ref _duplicates);
        public void IncrementMalformed() => Interlocked.Increment(ref _malformed);

        public override string ToString()
            => $"Imported: {Imported}, Duplicates: {Duplicates}, Malformed: {Malformed}";
    }
}
