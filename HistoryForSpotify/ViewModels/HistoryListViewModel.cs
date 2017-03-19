using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.Commons.Models;
using HistoryForSpotify.Core.AudioServices.Interfaces;
using HistoryForSpotify.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.ViewModels
{
    public class HistoryListViewModel : ViewModelBase, IHistoryListViewModel
    {
        private ILog _log;
        private IAudioService _audioService;

        private ObservableCollection<HistoryItemViewModel> _historyItemViewModels;

        public ObservableCollection<HistoryItemViewModel> HistoryItemViewModels
        {
            get
            {
                return _historyItemViewModels;
            }
            set
            {
                _historyItemViewModels = value;
                OnPropertyChanged(nameof(HistoryItemViewModel));
            }
        }

        public HistoryListViewModel(ILog log, IAudioService audioService)
        {
            _log = log;
            _audioService = audioService;

            _audioService.OnNewHistoryItem += OnNewHistoryItem;
            _audioService.OnNewHistoryItemTrackTime += OnNewHistoryItemTrackTime;
            _audioService.OnServiceConnected += OnServiceConnected;

            _historyItemViewModels = new ObservableCollection<HistoryItemViewModel>();

            _log.Debug("Created HistoryListViewModel");
        }

        private void OnServiceConnected()
        {
            Task.Factory.StartNew(() => OnInternalServiceConnected()).ContinueWith((result) => OnNewHistoryItem(result.Result));
        }

        private HistoryItem OnInternalServiceConnected()
        {
            HistoryItem historyItem = null;

            while (historyItem == null)
            {
                try
                {
                    historyItem = _audioService.GetCurrentHistoryItem();
                }
                catch (Exception ex)
                {
                    _log?.Error(ex.ToString());
                }
            }
            return historyItem;
        }

        private void OnNewHistoryItem(HistoryItem historyItem)
        {
            DispatcherObject.BeginInvoke((Action)(() =>
            {
                HistoryItemViewModel historyItemViewModel = new HistoryItemViewModel(historyItem);
                historyItemViewModel.OnPlayHistoryItem += OnPlayHistoryItem;
                historyItemViewModel.OnDeleteHistoryItem += OnDeleteHistoryItem;

                _historyItemViewModels.Insert(0, historyItemViewModel);
            }));
            
        }

        private void OnDeleteHistoryItem(HistoryItem historyItem, HistoryItemViewModel historyItemViewModel)
        {
            _historyItemViewModels.Remove(historyItemViewModel);
        }

        private void OnPlayHistoryItem(HistoryItem obj)
        {
            _audioService.PlayHistoryItemFromPosition(obj);
        }

        private void OnNewHistoryItemTrackTime(double trackTime, Guid itemId)
        {
            UpdateHistoryItemTrackTime(trackTime, itemId);
        }

        private void UpdateHistoryItemTrackTime(double trackTime, Guid itemId)
        {
            if (_historyItemViewModels != null && _historyItemViewModels.Count > 0)
            {
                var selectedHistoryItem = (from p in _historyItemViewModels where Guid.Equals(p.Id, itemId) select p).FirstOrDefault();

                if (selectedHistoryItem != null)
                    selectedHistoryItem.UpdateTrackTime(trackTime);
            }
        }
    }
}