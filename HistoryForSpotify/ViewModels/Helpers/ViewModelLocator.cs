using HistoryForSpotify.Commons.Logging.Factories;
using HistoryForSpotify.Commons.Logging.Interfaces;
using HistoryForSpotify.Core.AudioServices;
using HistoryForSpotify.Core.AudioServices.Interfaces;
using HistoryForSpotify.Core.Storage;
using HistoryForSpotify.Core.Storage.Interface;
using HistoryForSpotify.ViewModels.Dummies;
using HistoryForSpotify.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HistoryForSpotify.ViewModels.Helpers
{
    public class ViewModelLocator
    {
        private ILog _log;
        private IAudioService _audioService;

        private IShellViewModel _shellViewModel;
        private IHistoryListViewModel _historyListViewModel;
        private IWaitForSpotifyViewModel _waitForSpotifyViewModel;
        private IHistoryItemPersister _historyItemPersister;

        private IShellViewModel _designShellViewModel;
        private IHistoryListViewModel _designHistoryListViewModel;
        private IWaitForSpotifyViewModel _designWaitForSpotifyViewModel;
        private string _saveFolder;

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

        public IShellViewModel DesignShellViewModel
        {
            get
            {
                return _designShellViewModel;
            }
        }

        public IHistoryListViewModel DesignHistoryListViewModel
        {
            get
            {
                return _designHistoryListViewModel;
            }
        }

        public IWaitForSpotifyViewModel DesignWaitForSpotifyViewModel
        {
            get
            {
                return _designWaitForSpotifyViewModel;
            }
        }

        public ViewModelLocator()
        {
            if (IsInDesignTime())
            {
                _designShellViewModel = new DummyShellViewModel();
                _designHistoryListViewModel = new DummyHistoryListViewModel();
                _designWaitForSpotifyViewModel = new DummyWaitForSpotifyViewModel();
            }
            else
            {
                _saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"HistoryForSpotify\");

                if (!Directory.Exists(_saveFolder))
                    Directory.CreateDirectory(_saveFolder);

                //Create Dependencies
                _log = LoggerFactory.GetLogger(_saveFolder);
                _audioService = new SpotifyAudioService(_log);
                _historyItemPersister = new HistoryItemJsonPersister();

                //Inject them
                _shellViewModel = new ShellViewModel(_log, _audioService);
                _historyListViewModel = new HistoryListViewModel(_log, _audioService, _historyItemPersister, _saveFolder);
                _waitForSpotifyViewModel = new WaitForSpotifyViewModel(_log);
            }
        }

        private bool IsInDesignTime()
        {
           return DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}
