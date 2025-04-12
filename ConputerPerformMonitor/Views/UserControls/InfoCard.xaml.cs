using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ConputerPerformMonitor.Views.UserControls
{
    /// <summary>
    /// InfoCard.xaml 的互動邏輯
    /// </summary>
    public partial class InfoCard : UserControl
    {
        public InfoCard()
        {
            InitializeComponent();
        }



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(InfoCard));



        public string HardwareType
        {
            get { return (string)GetValue(HardwareTypeProperty); }
            set { SetValue(HardwareTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HardwareType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HardwareTypeProperty =
            DependencyProperty.Register("HardwareType", typeof(string), typeof(InfoCard));


       

    }
}
