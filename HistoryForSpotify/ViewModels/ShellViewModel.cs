using HistoryForSpotify.Commons.Logging.Interfaces;
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

        public ShellViewModel(ILog log)
        {
            _log = log;
        }
    }
}
