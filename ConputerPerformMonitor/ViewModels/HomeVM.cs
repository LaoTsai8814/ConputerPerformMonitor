using LibreHardwareMonitor.Hardware;
using ConputerPerformMonitor.Models;
using System.ComponentModel;

using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Windows.Input;
using ConputerPerformMonitor.Command;
using ConputerPerformMonitor.Views.Tabs;

namespace ConputerPerformMonitor.ViewModels
{
    public class HomeVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static event Action<Type> UpdateTab;

        private object _currentTab;

        public object CurrentTab
        {
            get { return _currentTab; }
            set {
                _currentTab = value;
                OnPropertyChanged();
            }
        }

        
        TemperatureManage temp = new();
        public HomeVM()
        {
            
            UpdateTab += OnUpdateTab;
            UpdateTab?.Invoke(typeof(MainTab));
           

        }
        private void OnUpdateTab(Type type)
        {
            CurrentTab = TabManager.GetTab(type);
            //throw new NotImplementedException();
        }


        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        


    }
}
