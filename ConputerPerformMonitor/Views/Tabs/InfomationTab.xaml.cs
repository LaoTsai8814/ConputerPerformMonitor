using ConputerPerformMonitor.Models;
using ConputerPerformMonitor.ViewModels.TabsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConputerPerformMonitor.Views.Tabs
{
    /// <summary>
    /// InfomationTab.xaml 的互動邏輯
    /// </summary>
    public partial class InfomationTab : UserControl
    {
        
        public InfomationTab(string component)
        {
            InitializeComponent();
            InfomationTabVM vm = new InfomationTabVM(component);
            DataContext = vm;
            //TabManager.MaunallyAddTab(typeof(InfomationTab), this);
        }
    }
}
