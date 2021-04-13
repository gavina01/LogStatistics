using Log.Statistics.Interface;
using Log.Statistics.Model;
using Log.Statistics.Service;
using Log.Statistics.Tests.Helper;
using System.Collections.Generic;
using Xunit;

namespace Log.Statistics.Tests
{
    public class UniqueCountResolverTest
    {
        private readonly IResultResolver _uniqueCountResolverTest;
        private IConsole _consoleWrapperStub;

        public UniqueCountResolverTest()
        {
            _consoleWrapperStub = new ConsoleWrapperStub();
            _uniqueCountResolverTest = new UniqueCountResolver(_consoleWrapperStub);
        }

        [Fact]
        public void Resolve_WhenValidClientIPRecords_ReturnCount()
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
            var field = "ClientIP";

            var expectedOutput = $"Unique count for field {field} - 3\r\n";

            _uniqueCountResolverTest.Resolve(logItems, field);

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

            var field = "Uri";

            var expectedOutput = $"Unique count for field {field} - 2\r\n";

            _uniqueCountResolverTest.Resolve(logItems, field);

            Assert.Equal(expectedOutput, _consoleWrapperStub.ToString());
        }
    }
}