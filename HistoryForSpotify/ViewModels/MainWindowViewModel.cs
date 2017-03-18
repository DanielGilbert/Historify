using HistoryForSpotify.Commons.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _selectedIndex;
        private ILog _log;

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

        public MainWindowViewModel()
        {
        }
    }
}
