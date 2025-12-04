using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.src.Domain.Extensions
{
    internal static class StringExtensions
    {
        public static string NormalizeInput(this string s)
        => s.Trim().ToLowerInvariant();

        public static bool ContainsIgnoreCase(this string text, string value)
            => text?.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
