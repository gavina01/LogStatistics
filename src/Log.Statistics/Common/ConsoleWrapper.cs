using Log.Statistics.Interface;
using System;

namespace Log.Statistics.Common
{
    /// <summary>
    /// Abstract write for Console Output
    /// </summary>
    public class ConsoleWrapper : IConsole
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}