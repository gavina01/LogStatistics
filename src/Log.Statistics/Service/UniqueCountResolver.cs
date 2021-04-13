using Log.Statistics.Common;
using Log.Statistics.Interface;
using Log.Statistics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Log.Statistics.Service
{
    /// <summary>
    /// Resolver to count unqiue fields in column
    /// </summary>
    public class UniqueCountResolver : IResultResolver
    {
        private readonly IConsole _console;
        private string defaultField = "ClientIP";

        public UniqueCountResolver(IConsole console)
        {
            _console = console;
        }

        public void Resolve(IList<LogItem> logItems, string field)
        {
            field ??= defaultField;
            Expression<Func<IList<LogItem>, int>> expression = list => list.Select(x => x.Field<string>(field)).Distinct().Count();

            Func<IList<LogItem>, int> resolve = expression.Compile();

            var result = resolve(logItems);

            _console.WriteLine($"Unique count for field {field} - {result}");
        }
    }
}