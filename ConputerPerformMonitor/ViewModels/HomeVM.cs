using LibreHardwareMonitor.Hardware;
using ConputerPerformMonitor.Models;
using System.ComponentModel;

using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Windows.Input;
using ConputerPerformMonitor.Command;
using ConputerPerformMonitor.Views.Tabs;
using System.Windows.Media;

namespace ConputerPerformMonitor.ViewModels
{
    public class HomeVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static Action<Type> UpdateTab;

        private object _currentTab;

        public object CurrentTab
        {
            get { return _currentTab; }
            set {
                _currentTab = value;
                OnPropertyChanged();
            }
        }
        public Dictionary<string, ICommand> _sideButtonCommand = new();
        public Dictionary<string, ICommand> SideButtonCommand {
            get
            {
                return _sideButtonCommand;
            }
            set
            {
                _sideButtonCommand = value;
                OnPropertyChanged();
            }
        
        } 
        /*
         SideButtonCommand { get; } = new RelayCommand((object obj) =>
        {
            Type type = Type.GetType($"ConputerPerformMonitor.Views.Tabs.{(string)obj}, ConputerPerformMonitor");
            try
            {
                if (type == null)
                {
                    return;
                }
                if (type != null)
                {
                    
                    UpdateTab?.Invoke(type);
                }
            }
            catch
            {
                throw;
                


            };
        });*/
        public void OnSideButtonClicked(object obj)
        {
            Type type = Type.GetType($"ConputerPerformMonitor.Views.Tabs.{(string)obj}, ConputerPerformMonitor");
            try
            {
                if (type == null)
                {
                    return;
                }
                if (type != null)
                {
                    UpdateTab?.Invoke(type);
                }
            }
            catch
            {
                throw;
            };
        }

        TemperatureManage temp = new();
        public HomeVM()
        {
            
            UpdateTab += OnUpdateTab; 
            OnUpdateTab(typeof(MainTab));
            SideButtonCommand.Add("Home", new RelayCommand((object obj)=> OnSideButtonClicked("MainTab")));
            SideButtonCommand.Add("MonitorDashboard", new RelayCommand((object obj) => OnSideButtonClicked("AllHardwareMonitorTab")));
            SideButtonCommand.Add("PerformanceReport", new RelayCommand((object obj) => OnSideButtonClicked("AutomaticManage")));

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
