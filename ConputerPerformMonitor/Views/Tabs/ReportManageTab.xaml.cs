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
    /// ReportManageTab.xaml 的互動邏輯
    /// </summary>
    public partial class ReportManageTab : UserControl
    {
        public ReportManageTab()
        {
            InitializeComponent();
            
        }

        public bool IsAutoUpdateActive
        {
            get { return (bool)GetValue(IsAutoUpdateActiveProperty); }
            set { SetValue(IsAutoUpdateActiveProperty, value); 
                    MessageBox.Show("IsAutoUpdateActive: " + value.ToString());
            }
        }

        // Using a DependencyProperty as the backing store for IsAutoUpdateActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoUpdateActiveProperty =
            DependencyProperty.Register("IsAutoUpdateActive", typeof(bool), typeof(ReportManageTab), new PropertyMetadata(true, OnManualChanged));

        private static void OnManualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportManageTab reportManageTab = d as ReportManageTab;
            if (reportManageTab.IsAutoUpdateActive)
            {
                reportManageTab.IsManuallyUpdateActive = false;
            }
            //throw new NotImplementedException();
        }

        public bool IsManuallyUpdateActive
        {
            get { return (bool)GetValue(IsManuallyUpdateActiveProperty); }
            set { SetValue(IsManuallyUpdateActiveProperty, value);
                
            }
        }

        // Using a DependencyProperty as the backing store for IsManuallyUpdateActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsManuallyUpdateActiveProperty =
            DependencyProperty.Register("IsManuallyUpdateActive", typeof(bool), typeof(ReportManageTab), new PropertyMetadata(false, OnAutomaticChanged));

        private static void OnAutomaticChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportManageTab reportManageTab = d as ReportManageTab;
            if (reportManageTab.IsManuallyUpdateActive)
            {
                reportManageTab.IsAutoUpdateActive = false;
            }
            //throw new NotImplementedException();
        }

        public string Hour
        {
            get { return (string)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(string), typeof(ReportManageTab), new PropertyMetadata("0", OnTimeChangedHR));

        private static void OnTimeChangedHR(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportManageTab reportManageTab = d as ReportManageTab;
            if (VerifyHR(e.NewValue))
            {
                reportManageTab.Hour = (string)e.NewValue;
            }
            else
            {
                reportManageTab.Hour = "0";
            }
        }
        private static void OnTimeChangedMM(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportManageTab reportManageTab = d as ReportManageTab;
            if (VerifyMMSS(e.NewValue))
            {
                reportManageTab.Minute = (string)e.NewValue;
            }
            else
            {
                reportManageTab.Minute = "0";
            }
        }
        private static void OnTimeChangedSS(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportManageTab reportManageTab = d as ReportManageTab;
            if (VerifyMMSS(e.NewValue))
            {
                reportManageTab.Second = (string)e.NewValue;
            }
            else
            {
                reportManageTab.Second = "0";
            }
        }
        public string Minute
        {
            get { return (string)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Minute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(string), typeof(ReportManageTab), new PropertyMetadata("0", OnTimeChangedMM));

        public string Second
        {
            get { return (string)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Second.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(string), typeof(ReportManageTab), new PropertyMetadata("0", OnTimeChangedSS));

        private static bool VerifyMMSS(object value)
        {   
            string? intValue = (string)value;

            if (intValue == null)
                intValue = "0";

            if (int.TryParse(intValue, out int result))
            {
                if (result < 0 || result >= 60)
                {
                    MessageBox.Show("Must be between 0 and 60.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Must be a number.");
                return false;
            }

        }
        private static bool VerifyHR(object value)
        {
            string? intValue = (string)value;

            if (intValue == null)
                intValue = "0";

            if (int.TryParse(intValue, out int result))
            {
                if (result < 0 || result >= 24)
                {
                    MessageBox.Show("Hour must be between 0 and 23.");
                    return false;
                }
                else
                {

                    return true;
                }
            }
            else
            {
                MessageBox.Show("Hour must be a number.");
                return false;
            }
        }
    }

}
