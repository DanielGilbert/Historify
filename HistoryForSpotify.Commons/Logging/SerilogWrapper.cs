using HistoryForSpotify.Commons.Logging.Interfaces;
using System;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HistoryForSpotify.Commons.Logging
{
    public class SerilogWrapper : ILog
    {
        string _logFolder;
        ILogger _serilogLogger;

        public SerilogWrapper(string logFolder)
        {
            _logFolder = Path.Combine(logFolder, "log-{Date}.txt");

            _serilogLogger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.RollingFile(_logFolder)
                    .CreateLogger();
        }

        public void Debug(string message)
        {
            _serilogLogger.Debug(message);
        }

        public void Error(string message)
        {
            _serilogLogger.Error(message);
        }

        public void Fatal(string message)
        {
            _serilogLogger.Fatal(message);
        }

        public void Info(string message)
        {
            _serilogLogger.Information(message);
        }

        public void Warning(string message)
        {
            _serilogLogger.Warning(message);
        }
    }
}
