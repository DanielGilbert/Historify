using HistoryForSpotify.Commons.Logging.Factories;
using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.Core.AudioServices;
using HistoryForSpotify.Core.AudioServices.Interfaces;
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
        private IAudioService _audioService;
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
            _audioService = new SpotifyAudioService(_log);
            _shellViewModel = new ShellViewModel(_log);
            _historyListViewModel = new HistoryListViewModel(_log, _audioService);
            _waitForSpotifyViewModel = new WaitForSpotifyViewModel(_log);
        }
    }
}
