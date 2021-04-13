using Log.Statistics.Interface;
using System;

namespace Log.Statistics.Tests.Helper
{
    /// <summary>
    /// Unit test wrapper stub for testing on output
    /// </summary>
    public class ConsoleWrapperStub : IConsole
    {
        private string Output = string.Empty;

        public void WriteLine(string message)
        {
            Output += message + Environment.NewLine;
        }

        public override string ToString()
        {
            return Output;
        }
    }
}