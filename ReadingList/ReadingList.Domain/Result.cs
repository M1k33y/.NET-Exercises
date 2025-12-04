using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.src.Domain
{
    internal class Result<T>
    {
        public bool Ok { get; }
        public T? Value { get; }
        public string? Error { get; }

        private Result(bool ok, T? value, string? error)
        {
            Ok = ok;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
            => new Result<T>(true, value, null);

        public static Result<T> Fail(string error)
            => new Result<T>(false, default, error);
    }
}
