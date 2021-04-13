using Log.Statistics.Interface;
using Log.Statistics.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Log.Statistics.Service
{
    /// <summary>
    /// Custom Log file Parser.
    /// Support only for ClientIP and Uri - To be extended for further fields
    /// </summary>
    public class CustomLogFileParser : ILogParser
    {
        private int _maxRecordsToRead = 1000;

        public int MaxRecordsToRead
        {
            get { return _maxRecordsToRead; }
            set { _maxRecordsToRead = value; }
        }

        public List<LogItem> ParseLogFile(string inputPath)
        {
            _ = inputPath ?? throw new ArgumentNullException(nameof(inputPath));

            List<LogItem> logItems = new List<LogItem>();
            var lineCounter = 0;

            try
            {
                using (StreamReader sr = File.OpenText(inputPath))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        LogItem item = new LogItem();

                        ParseClientIP(line, item);

                        ParseUrl(line, item);

                        // Parsing can be extended to include further fields

                        logItems.Add(item);

                        lineCounter++;
                        if (lineCounter >= _maxRecordsToRead)
                            break;
                    }
                }

                Console.WriteLine($"File read successfull with {lineCounter} lines");

                return logItems;
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not be found");
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured: " + ex.Message);
            }
        }

        private static void ParseUrl(string line, LogItem item)
        {
            // Match Url
            Match UrlMatch = Regex.Match(line, @"(?:GET|POST|PATCH|PUT|DELETE)(.*)HTTP", RegexOptions.IgnoreCase);

            if ((UrlMatch.Success))
            {
                item.Method = UrlMatch.Groups[0].Value.Trim();
                item.Uri = UrlMatch.Groups[1].Value.Trim();
                item.Protocol = UrlMatch.Groups[2].Value.Trim();
            }
        }

        private static void ParseClientIP(string line, LogItem item)
        {
            // Match IP Address
            Match IpMatch = Regex.Match(line, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

            if ((IpMatch.Success))
            {
                item.ClientIP = IpMatch.Value;
            }
        }
    }
}