using Log.Statistics.Common;
using Log.Statistics.Interface;
using Log.Statistics.Model;
using System.Collections.Generic;
using System.Linq;

namespace Log.Statistics.Service
{
    /// <summary>
    /// Resolver for top count for specific column
    /// </summary>
    public class TopCountResolver : IResultResolver
    {
        private readonly IConsole _console;
        private string defaultField = "ClientIP";

        public TopCountResolver(IConsole console)
        {
            _console = console;
        }

        private int maxResults = 3;

        public void Resolve(IList<LogItem> logItems, string field)
        {
            field ??= defaultField;
            var result = logItems.GroupBy(x => x.Field<string>(field)).Select(s => new { key = s.Key, counter = s.Count() }).OrderByDescending(o => o.counter).Take(maxResults).Select(f => new { key = f.key, counter = f.counter });

            _console.WriteLine($"Top results for {field}");

            foreach (var item in result)
            {
                _console.WriteLine($"{item.key} - {item.counter}");
            }
        }
    }
}