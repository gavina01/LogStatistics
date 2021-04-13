using Log.Statistics.Interface;
using Log.Statistics.Service;
using Log.Statistics.Tests.Helper;
using System.IO;
using Xunit;

namespace Log.Statistics.Tests
{
    public class ConsoleApplicationTests
    {
        private readonly ILogParser _logParser;
        private IConsole _consoleWrapperStub;

        public ConsoleApplicationTests()
        {
            _consoleWrapperStub = new ConsoleWrapperStub();
            _logParser = new CustomLogFileParser();
        }

        //  The number of unique IP addresses
        //  The top 3 most visited URLs
        //  The top 3 most active IP addresses

        [Fact]
        public void OutputTest_WhenNumberOfUniqueIPAddress_ReturnCount()
        {
            string logPath = Path.Combine(System.AppContext.BaseDirectory, @"sample\programming-task-example-data.log");

            var consoleApplication = new ConsoleApplication(_logParser, _consoleWrapperStub);

            string[] args = new string[]
            {
                $"-f {logPath}",
                "-s count"
            };

            consoleApplication.Run(args);

            var expectedOutput = "Unique count for field ClientIP - 11\r\n";

            Assert.Equal(expectedOutput, _consoleWrapperStub.ToString());
        }

        [Fact]
        public void OutputTest_When3MostActiveIP_ReturnList()
        {
            string logPath = Path.Combine(System.AppContext.BaseDirectory, @"sample\programming-task-example-data.log");

            var consoleApplication = new ConsoleApplication(_logParser, _consoleWrapperStub);

            string[] args = new string[]
            {
                $"-f {logPath}",
                "-s top"
            };

            consoleApplication.Run(args);

            var expectedOutput = "Top results for ClientIP\r\n168.41.191.40 - 4\r\n177.71.128.21 - 3\r\n50.112.00.11 - 3\r\n";

            Assert.Equal(expectedOutput, _consoleWrapperStub.ToString());
        }

        [Fact]
        public void OutputTest_When3MostVisitedURL_ReturnList()
        {
            string logPath = Path.Combine(System.AppContext.BaseDirectory, @"sample\programming-task-example-data.log");

            var consoleApplication = new ConsoleApplication(_logParser, _consoleWrapperStub);

            string[] args = new string[]
            {
                $"-f {logPath}",
                "-s top",
                "-p Uri"
            };

            consoleApplication.Run(args);

            var expectedOutput = "Top results for Uri\r\n/docs/manage-websites/ - 2\r\n/intranet-analytics/ - 1\r\nhttp://example.net/faq/ - 1\r\n";

            Assert.Equal(expectedOutput, _consoleWrapperStub.ToString());
        }
    }
}