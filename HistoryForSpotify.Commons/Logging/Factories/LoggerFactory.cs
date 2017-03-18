using HistoryForSpotify.Commons.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.Commons.Logging.Factories
{
    public static class LoggerFactory
    {
        private static ILog _log;

        public static ILog GetLogger(string loggerPath)
        {
            if (_log != null) return _log;

            _log = new SerilogWrapper(loggerPath);

            return _log;
        }
    }
}
