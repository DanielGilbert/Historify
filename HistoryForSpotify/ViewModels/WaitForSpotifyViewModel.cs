using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.ViewModels
{
    public class WaitForSpotifyViewModel : ViewModelBase, IWaitForSpotifyViewModel
    {
        private ILog _log;

        public WaitForSpotifyViewModel(ILog log)
        {
            _log = log;

            _log.Debug("Created WaitForSpotifyViewModel");
        }
    }
}
