using CommandLine;

namespace Log.Statistics.Common
{
    /// <summary>
    /// Commandline input arguments
    /// </summary>
    public class ArgOptions
    {
        [Option('f', "file", Required = true, HelpText = "File Path")]
        public string filename { get; set; }

        [Option('s', "stat", Required = true, HelpText = "Type of statistic required top / cnt (top x results / unique count)")]
        public string command { get; set; }

        [Option('p', "property", Required = false, HelpText = "Field to perform stat on")]
        public string field { get; set; }
    }
}