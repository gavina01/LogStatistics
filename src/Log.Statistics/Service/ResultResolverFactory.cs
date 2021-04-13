using Log.Statistics.Interface;

namespace Log.Statistics.Service
{
    /// <summary>
    /// Factory to determine result required from commandline
    /// </summary>
    public class ResultResolverFactory
    {
        public IResultResolver GetResolver(string stat, IConsole console)
        {
            IResultResolver resolver = null;

            switch (stat)
            {
                case "count":
                    resolver = new UniqueCountResolver(console);
                    break;

                case "top":
                    resolver = new TopCountResolver(console);
                    break;

                default:
                    break;
            }
            return resolver;
        }
    }
}