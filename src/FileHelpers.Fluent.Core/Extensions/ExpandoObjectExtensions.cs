using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FileHelpers.Fluent.Fixed")]
[assembly: InternalsVisibleTo("FileHelpers.Fluent.Xml")]
[assembly: InternalsVisibleTo("FileHelpers.Fluent.Delimited")]

namespace FileHelpers.Fluent.Core.Extensions
{
    internal static class ExpandoObjectExtensions
    {
        internal static void InternalTryAdd(this ExpandoObject item, string key, object value)
        {
#if NETSTANDARD2_0
            ((IDictionary<string, object>)item).Add(key, value);
#else
            item.TryAdd(key, value);
#endif
        }
    }
}
