using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HistoryForSpotify.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public virtual Dispatcher DispatcherObject { get; protected set; }
        protected ViewModelBase()
        {
            DispatcherObject = Dispatcher.CurrentDispatcher;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
            => this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
            => this.PropertyChanged?.Invoke(this, e);
        
    }
}
