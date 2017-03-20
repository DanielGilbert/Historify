using HistoryForSpotify.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryForSpotify.ViewModels.Dummies
{
    public class DummyShellViewModel : ViewModelBase, IShellViewModel
    {
        bool _isSnackbarActive;

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

        public DummyShellViewModel()
        {
            IsSnackbarActive = true;
        }
    }
}
