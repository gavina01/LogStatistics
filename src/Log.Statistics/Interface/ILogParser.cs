using Log.Statistics.Model;
using System.Collections.Generic;

namespace Log.Statistics.Interface
{
    /// <summary>
    /// Abstract Log Parser for multiple log file parsers
    /// </summary>
    public interface ILogParser
    {
        List<LogItem> ParseLogFile(string inputPath);

        public int MaxRecordsToRead { get; set; }
    }
}