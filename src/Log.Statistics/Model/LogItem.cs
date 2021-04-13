using System;

namespace Log.Statistics.Model
{
    /// <summary>
    /// Standard mapped log items
    /// </summary>
    public class LogItem
    {
        public string ClientIP { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public string Method { get; set; }

        public string Username { get; set; }

        public string UserAgent { get; set; }

        public string Uri { get; set; }

        public string StatusCode { get; set; }

        public string Protocol { get; set; }
    }
}