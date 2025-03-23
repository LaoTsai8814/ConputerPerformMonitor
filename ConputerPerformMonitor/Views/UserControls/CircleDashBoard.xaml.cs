using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    /// CircleDashBoard.xaml 的互動邏輯
    /// </summary>
    public partial class CircleDashBoard : UserControl
    {
        public CircleDashBoard()
        {
            InitializeComponent();
        }


        


    }
    public class TemperatureToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int temp)
            {
                return (225+temp*27/10) % 360;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int angle)
            {
                return (angle - 225 + 360) % 360;
            }
            return 0;
        }
    }

}
