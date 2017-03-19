using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.Core.AudioServices.Interfaces;
using HistoryForSpotify.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.ViewModels
{
    public class HistoryListViewModel : ViewModelBase, IHistoryListViewModel
    {
        private ILog _log;
        private IAudioService _audioService;
        public HistoryListViewModel(ILog log, IAudioService audioService)
        {
            _log = log;
            _audioService = audioService;

            _audioService.OnNewSong += OnNewSong;
            _audioService.OnNewTrackTime += OnNewTrackTime;

            _log.Debug("Created HistoryListViewModel");
        }

        private void OnNewTrackTime()
        {
        }

        private void OnNewSong()
        {
        }
    }
}