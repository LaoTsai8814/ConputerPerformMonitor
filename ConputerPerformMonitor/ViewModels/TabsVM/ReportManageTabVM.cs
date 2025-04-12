using ConputerPerformMonitor.Command;
using ConputerPerformMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConputerPerformMonitor.ViewModels.TabsVM
{
    public class ReportManageTabVM : INotifyPropertyChanged
    {
        public int _cpuUsage { get; set; }

        public int _ramUsage { get; set; }

        public int _gpuUsage { get; set; }


        private object _Tab;
        public event PropertyChangedEventHandler? PropertyChanged;


        DataManage _dataManageTab = new();

        private bool _isautoactive;

        public bool IsAutoUpdate
        {
            get { return _isautoactive; }
            set
            {

                _isautoactive = value;
                /*
                if (_isautoactive)
                {
                    DateTime time = DateTime.Today;
                    _dataManageTab.CreateDBFile();
                    Timer timer = new Timer(new TimerCallback((e) => _dataManageTab.InsertLog(DateTime.Today.ToShortDateString(),DateTime.Now.ToShortTimeString(),_cpuUsage.ToString(),_gpuUsage.ToString(),_ramUsage.ToString())), null, 0, 86400);
                }*/
                OnPropertyChanged();

            }
        }
        protected void UpdateUITemp(TemperatureData temperatureData)
        {
            temperatureData.temperatures.ForEach(x =>
            {
                Trace.WriteLine($"Name: {x.Name}, Value: {x.Value}");
                if (x.Name == "CPU Total")
                {
                    Trace.WriteLine($"CPU Total: {x.Value}");
                    _cpuUsage = (int)x.Value;
                }
                if (x.Name == "GPU Core")
                {
                    Trace.WriteLine($"GPU Core: {x.Value}");
                    _gpuUsage = (int)x.Value;
                }
                if (x.Name == "Memory")
                {
                    Trace.WriteLine($"Memory: {x.Value}");
                    _ramUsage = (int)x.Value;
                }
                
            });

        }
        private bool _ismanuallyactive;

        public bool IsManually
        {
            get { return _ismanuallyactive; }
            set
            {

                _ismanuallyactive = value;
                if (_ismanuallyactive)
                {
                    _dataManageTab.CreateDBFile(DateTime.Now);


                }
                OnPropertyChanged();


            }
        }

        public ICommand Save { get; set; } = new RelayCommand((object obj) =>
        {
            // Save logic here
            // For example, you can save the current settings to a file or database





        });


        private string _hour;

        public string Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                OnPropertyChanged();
            }
        }
        private string _minute;

        public string Minute
        {
            get { return _minute; }
            set
            {
                _minute = value;
                OnPropertyChanged();

            }
        }
        private string _second;

        public string Second
        {
            get { return _second; }
            set
            {
                _second = value;
                OnPropertyChanged();


            }
        }



        public object CurrentReportTab
        {
            get { return _Tab; }
            set
            {

                _Tab = value;
                OnPropertyChanged();
            }
        }
        public ReportManageTabVM()
        {

            TemperatureManage.UpdateTemp += UpdateUITemp;


        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
