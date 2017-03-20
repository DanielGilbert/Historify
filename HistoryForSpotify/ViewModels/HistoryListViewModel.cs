using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.Commons.Models;
using HistoryForSpotify.Core.AudioServices.Interfaces;
using HistoryForSpotify.Core.Storage.Interface;
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
        private IHistoryItemPersister _historyItemPersister;
        private string _saveFolder;

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

        public HistoryListViewModel(ILog log,
                                    IAudioService audioService,
                                    IHistoryItemPersister historyItemPersister,
                                    string saveFolder)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));

            _log = log;
            _audioService = audioService;
            _historyItemPersister = historyItemPersister;
            _saveFolder = saveFolder;

            _audioService.OnNewHistoryItem += OnNewHistoryItem;
            _audioService.OnNewHistoryItemTrackTime += OnNewHistoryItemTrackTime;
            _audioService.OnServiceConnected += OnServiceConnected;

            _historyItemViewModels = LoadExistingItemViewModels(_historyItemPersister, _saveFolder);

            _log.Debug("Created HistoryListViewModel");
        }

        private ObservableCollection<HistoryItemViewModel> LoadExistingItemViewModels(IHistoryItemPersister historyItemPersister, string saveFolder)
        {
            List<HistoryItem> historyItems = historyItemPersister.Load(saveFolder);

            ObservableCollection<HistoryItemViewModel> historyItemViewModels = new ObservableCollection<HistoryItemViewModel>();

            foreach(var item in historyItems)
            {
                DispatcherObject.Invoke((Action)(() =>
                {
                    historyItemViewModels.Add(CreateHistoryItemViewModelFrom(item));
                }));
            }

            return historyItemViewModels;
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
                _historyItemViewModels.Insert(0, CreateHistoryItemViewModelFrom(historyItem));
                SaveExistingHistoryItemsFrom(_historyItemPersister, _historyItemViewModels, _saveFolder);
            }));

            
            
        }

        private void SaveExistingHistoryItemsFrom(IHistoryItemPersister historyItemPersister, 
                                                  ObservableCollection<HistoryItemViewModel> historyItemViewModels,
                                                  string _saveFolder)
        {
            List<HistoryItem> historyItems = new List<HistoryItem>();

            foreach(var historyItemViewModel in historyItemViewModels)
            {
                historyItems.Add(historyItemViewModel.HistoryItem);
            }

            historyItemPersister.Save(historyItems, _saveFolder);
        }

        private HistoryItemViewModel CreateHistoryItemViewModelFrom(HistoryItem historyItem)
        {
            HistoryItemViewModel historyItemViewModel = new HistoryItemViewModel(historyItem);
            historyItemViewModel.OnPlayHistoryItem += OnPlayHistoryItem;
            historyItemViewModel.OnDeleteHistoryItem += OnDeleteHistoryItem;

            return historyItemViewModel;
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