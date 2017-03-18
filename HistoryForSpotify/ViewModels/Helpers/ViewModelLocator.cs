using HistoryForSpotify.Commons.Logging.Factories;
using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.ViewModels.Helpers
{
    public class ViewModelLocator
    {
        private ILog _log;
        private IShellViewModel _shellViewModel;
        private IHistoryListViewModel _historyListViewModel;
        private IWaitForSpotifyViewModel _waitForSpotifyViewModel;

        public IShellViewModel ShellViewModel
        {
            get
            {
                return _shellViewModel;
            }
        }

        public IHistoryListViewModel HistoryListViewModel
        {
            get
            {
                return _historyListViewModel;
            }
        }

        public IWaitForSpotifyViewModel WaitForSpotifyViewModel
        {
            get
            {
                return _waitForSpotifyViewModel;
            }
        }

        public ViewModelLocator()
        {
            _log = LoggerFactory.GetLogger(@"D:\Log\");

            _shellViewModel = new ShellViewModel(_log);
            _historyListViewModel = new HistoryListViewModel(_log);
            _waitForSpotifyViewModel = new WaitForSpotifyViewModel(_log);
        }
    }
}
