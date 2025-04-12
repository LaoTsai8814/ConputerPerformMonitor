using ConputerPerformMonitor.Models;
using ConputerPerformMonitor.Views.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConputerPerformMonitor.ViewModels.TabsVM
{
    public class InfomationTabVM : INotifyPropertyChanged
    {
        private ObservableCollection<string> _info;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string> Info
        {
            get { return _info; }
            set 
            { 

                _info = value;
                OnPropertyChanged();

            }
        }

        public InfomationTabVM(string component)
        {
            try
            {
                Info = new ObservableCollection<string>(HardwareCpuSpec.DetailSpec[component]);
                
                
            }
            catch
            {

            }
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
