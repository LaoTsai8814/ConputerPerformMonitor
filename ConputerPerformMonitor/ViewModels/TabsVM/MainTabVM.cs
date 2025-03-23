using ConputerPerformMonitor.Command;
using ConputerPerformMonitor.Models;
using LiveCharts.Wpf;
using LiveCharts;

using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

using System.Timers;
using System.Windows.Input;

namespace ConputerPerformMonitor.ViewModels.TabsVM
{
    public class MainTabVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private System.Timers.Timer _timer;
        private int _timeCounter = 0;
        
        public void CpuUsageViewModel()
        {
            
            // 初始化 Y 轴数据
            UsageSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "CPU Usage",
                    Values = new ChartValues<double> { 0 },
                    Stroke = System.Windows.Media.Brushes.Green,
                    Fill = System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "GPU Usage",
                    Values = new ChartValues<double> { 0 },
                    Stroke = System.Windows.Media.Brushes.Blue,
                    Fill = System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "RAM Usage",
                    Values = new ChartValues<double> { 0 },
                    Stroke = System.Windows.Media.Brushes.Red,
                    Fill = System.Windows.Media.Brushes.Transparent
                }
            };

            // X 轴（时间）数据
            Labels = new List<string> { "0" };

            // 定时器更新数据
            _timer = new System.Timers.Timer(1000); // 每秒更新
            _timer.Elapsed += UpdateCpuUsage;
            _timer.Start();
        }

        public SeriesCollection UsageSeries { get; set; }
        public List<string> Labels { get; set; }

        private void UpdateCpuUsage(object sender, ElapsedEventArgs e)
        {

            double cpuUsage = CpuUsage;
            double gpuUsage = GpuUsage;
            double ramUsage = RamUsage;  // 这里应该换成 CPU 实时数据
            _timeCounter++;

            // 确保 UI 线程更新数据
            App.Current.Dispatcher.Invoke(() =>
            {
                if (UsageSeries[0].Values.Count > 60)
                {
                    UsageSeries[0].Values.RemoveAt(0);
                    UsageSeries[1].Values.RemoveAt(0);
                    UsageSeries[2].Values.RemoveAt(0);
                    Labels.RemoveAt(0);
                }

                UsageSeries[0].Values.Add(cpuUsage);
                UsageSeries[1].Values.Add(gpuUsage);
                UsageSeries[2].Values.Add(ramUsage);
                Labels.Add(_timeCounter.ToString());

                OnPropertyChanged(nameof(UsageSeries));
                OnPropertyChanged(nameof(Labels));
            });
        }



        /// <summary>
        /// CPU Usage
        /// </summary>
        private int _cpuUsage;

        public int CpuUsage
        {
            get { return _cpuUsage; }
            set
            {
                _cpuUsage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CpuUsageString));
            }
        }
        public string CpuUsageString
        {
            get
            {

                return _cpuUsage.ToString() + "%";

            }
        }

        /// <summary>
        /// GPU Usage
        /// </summary>

        private int _gpuUsage;

        public int GpuUsage
        {
            get { return _gpuUsage; }
            set
            {
                _gpuUsage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GpuUsageString));
            }
        }
        public string GpuUsageString
        {
            get
            {

                return _gpuUsage.ToString() + "%";

            }
        }
        /// <summary>
        /// RAM Usage
        /// </summary>
        private int _ramUsage;

        public int RamUsage
        {
            get { return _ramUsage; }
            set
            {
                _ramUsage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RamUsageString));
            }
        }
        public string RamUsageString
        {
            get
            {

                return _ramUsage.ToString() + "%";

            }
        }
        ICommand CpuDetail { get; set; }
        ICommand GpuDetail { get; set; }
        ICommand RamDetail { get; set; }
        public MainTabVM()
        {
            TemperatureManage.UpdateTemp += UpdateUITemp;
            CpuUsageViewModel();
            CpuDetail = new RelayCommand(CheckCpuDetail);
            GpuDetail = new RelayCommand(CheckGpuDetail);
            RamDetail = new RelayCommand(CheckRamDetail);
        }
        

        private void CheckRamDetail(object obj)
        {
            throw new NotImplementedException();
        }

        private void CheckGpuDetail(object obj)
        {
            throw new NotImplementedException();
        }

        private void CheckCpuDetail(object obj)
        {
            throw new NotImplementedException();
        }
        protected void UpdateUITemp(TemperatureData temperatureData)
        {
            temperatureData.temperatures.ForEach(x =>
            {
                Trace.WriteLine($"Name: {x.Name}, Value: {x.Value}");
                if (x.Name == "CPU Total")
                {
                    Trace.WriteLine($"CPU Total: {x.Value}");
                    CpuUsage = (int)x.Value;
                }
                if (x.Name == "GPU Core")
                {
                    Trace.WriteLine($"GPU Core: {x.Value}");
                    GpuUsage = (int)x.Value;
                }
                if (x.Name == "Memory")
                {
                    Trace.WriteLine($"Memory: {x.Value}");
                    RamUsage = (int)x.Value;
                }
            });

        }
        protected void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
