using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConputerPerformMonitor.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {

        public static event Action<object> ChangeView;

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainWindowVM()
        {
            ChangeView += OnChangingView;

        }
        public void SetView(object view)
        {
            ChangeView?.Invoke(view);
        }
        
        private void OnChangingView(object view)
        {
            CurrentView = view;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
