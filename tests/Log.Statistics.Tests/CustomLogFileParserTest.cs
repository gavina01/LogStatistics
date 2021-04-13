using Log.Statistics.Interface;
using Log.Statistics.Service;
using System;
using System.IO;
using Xunit;

namespace Log.Statistics.Tests
{
    public class CustomLogFileParserTest
    {
        private readonly ILogParser _customerLogFileParser;

        public CustomLogFileParserTest()
        {
            _customerLogFileParser = new CustomLogFileParser();
        }

        [Fact]
        public void ParseLogFile_WhenNullPath_ThrowArgumentNullException()
        {
            string logPath = null;

            Assert.Throws<ArgumentNullException>(() => _customerLogFileParser.ParseLogFile(logPath));
        }

        [Fact]
        public void ParseLogFile_WhenFileNotFound_ThrowException()
        {
            string logPath = @"c:\nofilefound.log";

            var ex = Assert.Throws<Exception>(() => _customerLogFileParser.ParseLogFile(logPath));
            Assert.Equal("File not be found", ex.Message);
        }

        [Fact]
        public void ParseLogFile_WhenError_ThrowException()
        {
            string logPath = @"c:\pathdoesnotexist\nofilefound.log";

            var ex = Assert.Throws<Exception>(() => _customerLogFileParser.ParseLogFile(logPath));
        }

        [Fact]
        public void ParseLogFile_WhenExceedMaxRecords_ReturnResults()
        {
            string logPath = Path.Combine(System.AppContext.BaseDirectory, @"sample\programming-task-example-data.log");

            _customerLogFileParser.MaxRecordsToRead = 2;

            var result = _customerLogFileParser.ParseLogFile(logPath);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ParseLogFile_WhenSuccessRecords_ReturnResults()
        {
            string logPath = Path.Combine(System.AppContext.BaseDirectory, @"sample\programming-task-example-data.log");

            var result = _customerLogFileParser.ParseLogFile(logPath);

            Assert.True(result.Count > 0, "Records parsed in log file");
        }

        [Fact]
        public void ParseLogFile_WhenSuccessValidParse_ReturnResults()
        {
            var validIPParse = "177.71.128.21";
            var validUri = "/intranet-analytics/";

            string logPath = Path.Combine(System.AppContext.BaseDirectory, @"sample\programming-task-example-data.log");

            _customerLogFileParser.MaxRecordsToRead = 1;

            var result = _customerLogFileParser.ParseLogFile(logPath);

            Assert.Equal(validIPParse, result[0].ClientIP);
            Assert.Equal(validUri, result[0].Uri);
        }

        [Fact]
        public void ParseLogFile_WhenSuccessInValidParse_ReturnResults()
        {
            string logPath = Path.Combine(AppContext.BaseDirectory, @"sample\programming-task-example-data-invalid.log");

            _customerLogFileParser.MaxRecordsToRead = 1;

            var result = _customerLogFileParser.ParseLogFile(logPath);

            Assert.Null(result[0].ClientIP);
            Assert.Null(result[0].Uri);
        }

        // Parse Tests can be extended to cater for further parsing on the log file.
    }
}