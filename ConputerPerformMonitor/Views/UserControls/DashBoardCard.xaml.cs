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
    /// DashBoardCard.xaml 的互動邏輯
    /// </summary>
    public partial class DashBoardCard : UserControl
    {
        public DashBoardCard()
        {
            InitializeComponent();
        }
        public int CoreTemp
        {
            get => (int)GetValue(CoreTempProperty);
            set => SetValue(CoreTempProperty, value);
        }

        public static readonly DependencyProperty CoreTempProperty =
            DependencyProperty.Register(nameof(CoreTemp), typeof(int), typeof(DashBoardCard), new PropertyMetadata(0));

        // ✅ 定义 CircleDashBoardCenterText 依赖属性
        public string CircleDashBoardCenterText
        {
            get => (string)GetValue(CircleDashBoardCenterTextProperty);
            set => SetValue(CircleDashBoardCenterTextProperty, value);
        }

        public static readonly DependencyProperty CircleDashBoardCenterTextProperty =
            DependencyProperty.Register(nameof(CircleDashBoardCenterText), typeof(string), typeof(DashBoardCard), new PropertyMetadata("N/A"));



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(DashBoardCard));



        public string HardwareType
        {
            get { return (string)GetValue(HardwareTypeProperty); }
            set { SetValue(HardwareTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HardwareType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HardwareTypeProperty =
            DependencyProperty.Register("HardwareType", typeof(string), typeof(DashBoardCard));


        public ObservableCollection<string> Spec
        {
            get { return (ObservableCollection<string>)GetValue(CpuSpecProperty); }
            set { SetValue(CpuSpecProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CpuSpecProperty =
            DependencyProperty.Register("Spec", typeof(ObservableCollection<string>), typeof(DashBoardCard),new PropertyMetadata(null));




    }
}
