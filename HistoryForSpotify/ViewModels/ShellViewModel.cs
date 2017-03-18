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
        private int _selectedIndex;
        private ILog _log;
        private IAudioService _audioService;

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public ShellViewModel(ILog log, IAudioService audioService)
        {
            _log = log;
            _audioService = audioService;

            _audioService.OnServiceConnected += OnServiceConnected;
            _audioService.OnServiceDisconnected += OnServiceDisconnected;

            _log.Debug("Created ShellViewModel");
        }

        private void OnServiceDisconnected()
        {
            SelectedIndex = 0;
        }

        private void OnServiceConnected()
        {
            SelectedIndex = 1;
        }
    }
}
