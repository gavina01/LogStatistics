using Log.Statistics.Common;
using Log.Statistics.Interface;
using Log.Statistics.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Log.Statistics
{
    /// <summary>
    /// Entry Point
    /// </summary>
    public class Program
    {
        public static IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            var services = ConfigureServices();

            _serviceProvider = services.BuildServiceProvider();

            _serviceProvider.GetService<ConsoleApplication>().Run(args);

            DisposeServices();

            Console.WriteLine("Press any key to close");
            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILogParser, CustomLogFileParser>();
            services.AddSingleton<IConsole, ConsoleWrapper>();
            services.AddSingleton<ConsoleApplication>();
            return services;
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}