using HistoryForSpotify.Commons.Logging.Interfaces;
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
        private IShellViewModel _shellViewModel;

        public IShellViewModel ShellViewModel
        {
            get
            {
                return _shellViewModel;
            }
        }

        public ViewModelLocator()
        {
            _shellViewModel = new ShellViewModel(null);
        }
    }
}
