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
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private bool _isSnackbarActive;
        private ILog _log;
        private IAudioService _audioService;

        public bool IsSnackbarActive
        {
            get
            {
                return _isSnackbarActive;
            }
            set
            {
                _isSnackbarActive = value;
                OnPropertyChanged(nameof(IsSnackbarActive));
            }
        }

        public ShellViewModel(ILog log, IAudioService audioService)
        {
            IsSnackbarActive = true;
            _log = log;
            _audioService = audioService;

            _audioService.OnServiceConnected += OnServiceConnected;
            _audioService.OnServiceDisconnected += OnServiceDisconnected;

            _audioService.Connect();

            _log.Debug("Created ShellViewModel");
        }

        private void OnServiceDisconnected()
        {
            IsSnackbarActive = true;
            _audioService.Connect();
        }

        private void OnServiceConnected()
        {
            IsSnackbarActive = false;
        }
    }
}
