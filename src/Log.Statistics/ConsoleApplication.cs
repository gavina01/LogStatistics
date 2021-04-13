using CommandLine;
using Log.Statistics.Common;
using Log.Statistics.Interface;
using Log.Statistics.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Log.Statistics
{
    /// <summary>
    /// Console Application to initiate results required on file input
    /// </summary>
    public class ConsoleApplication
    {
        private ILogParser _logParser;
        private IConsole _console;
        private static ArgOptions _options = new ArgOptions();

        public ConsoleApplication(ILogParser logParser, IConsole console)
        {
            _logParser = logParser;
            _console = console;
        }

        public void Run(string[] args)
        {
            var result = Parser.Default.ParseArguments<ArgOptions>(args)
                        .WithParsed(RunOptions)
                        .WithNotParsed(HandleParseError);

            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine($"Starting Parser at {DateTime.Now.ToShortDateString()}");

            var logItems = _logParser.ParseLogFile(_options.filename);

            if (logItems.Count == 0)
            {
                Console.WriteLine("No log records found");
                return;
            }

            IResultResolver resolver = new ResultResolverFactory().GetResolver(_options.command, _console);

            resolver.Resolve(logItems, _options.field);

            Console.WriteLine($"End Parser at {DateTime.Now.ToShortDateString()} - elapsed time {stopwatch.Elapsed.TotalSeconds} s");
        }

        private static void RunOptions(ArgOptions opts)
        {
            _options.filename = opts.filename.Trim();
            _options.command = opts.command.Trim();
            _options.field = opts.field?.Trim() ?? null;
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.Write("Please check inputs error found");
        }
    }
}