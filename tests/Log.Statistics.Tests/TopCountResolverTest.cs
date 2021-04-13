using Log.Statistics.Interface;
using Log.Statistics.Model;
using Log.Statistics.Service;
using Log.Statistics.Tests.Helper;
using System.Collections.Generic;
using Xunit;

namespace Log.Statistics.Tests
{
    public class TopCountResolverTest
    {
        private readonly IResultResolver _topCountResolverTest;
        private IConsole _consoleWrapperStub;

        public TopCountResolverTest()
        {
            _consoleWrapperStub = new ConsoleWrapperStub();
            _topCountResolverTest = new TopCountResolver(_consoleWrapperStub);
        }

        [Fact]
        public void Resolve_WhenValidClientIPRecords_ReturnList()
        {
            var logItems = new List<LogItem>()
            {
                new LogItem(){ClientIP = "1.1.1.1"},
                new LogItem(){ClientIP = "1.1.1.1"},
                new LogItem(){ClientIP = "1.1.1.1"},
                new LogItem(){ClientIP = "2.2.2.2"},
                new LogItem(){ClientIP = "2.2.2.2"},
                new LogItem(){ClientIP = "3.3.3.3"},
            };

            var expectedOutput = "Top results for ClientIP\r\n1.1.1.1 - 3\r\n2.2.2.2 - 2\r\n3.3.3.3 - 1\r\n";

            _topCountResolverTest.Resolve(logItems, "ClientIP");

            Assert.Equal(expectedOutput, _consoleWrapperStub.ToString());
        }

        [Fact]
        public void Resolve_WhenValidUriRecords_ReturnList()
        {
            var logItems = new List<LogItem>()
            {
                new LogItem(){Uri = "uri1"},
                new LogItem(){Uri = "uri1"},
                new LogItem(){Uri = "uri1"},
                new LogItem(){Uri = "uri1"},
                new LogItem(){Uri = "uri1"},
                new LogItem(){Uri = "uri2"},
            };

            var expectedOutput = "Top results for Uri\r\nuri1 - 5\r\nuri2 - 1\r\n";

            _topCountResolverTest.Resolve(logItems, "Uri");

            Assert.Equal(expectedOutput, _consoleWrapperStub.ToString());
        }
    }
}