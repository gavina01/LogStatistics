using Log.Statistics.Model;
using System.Collections.Generic;

namespace Log.Statistics.Interface
{
    /// <summary>
    /// Resolve required result
    /// </summary>
    public interface IResultResolver
    {
        void Resolve(IList<LogItem> logItems, string field = "ClientIP");
    }
}