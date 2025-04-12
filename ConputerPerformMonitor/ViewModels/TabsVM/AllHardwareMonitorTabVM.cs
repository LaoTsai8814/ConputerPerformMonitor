using ConputerPerformMonitor.Command;
using ConputerPerformMonitor.Models;
using ConputerPerformMonitor.Views.Tabs;
using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConputerPerformMonitor.ViewModels.TabsVM
{
    internal class AllHardwareMonitorTabVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private object _currentHardwareTab;

        public object CurrentHardwareTab
        {
            get { return _currentHardwareTab; }
            set {
                _currentHardwareTab = value;
                OnPropertyChanged();
            
            }
        }

        Dictionary<string, object> TabManagement = new Dictionary<string, object>();

        private Dictionary<string, ICommand> _hardwareInfoProperty = new();

        public Dictionary<string, ICommand> HardwareInfoProperty
        {
            get { return _hardwareInfoProperty; }
            set 
            {
                _hardwareInfoProperty = value;
                OnPropertyChanged();
            
            
            }
        }
        private void OnRequestSpecType(object obj)
        {
            string str = (string)obj;
            if (str != null)
            {
                
                CurrentHardwareTab =  new InfomationTab(str);
                

            }
        }




        public AllHardwareMonitorTabVM() 
        {
            foreach (var key in HardwareCpuSpec.DetailSpec.Keys) 
            {
                HardwareInfoProperty.Add(key, new RelayCommand((object obj) => OnRequestSpecType(key)));

            }

            CurrentHardwareTab = new InfomationTab("CPU");



        }



        

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
