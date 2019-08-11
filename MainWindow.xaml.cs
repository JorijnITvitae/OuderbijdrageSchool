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

namespace OuderbijdrageSchool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateCalendarDateRanges(DateTime.Now);
        }

        private void UpdateCalendarDateRanges(DateTime today)
        {
            var fourYearsAgo = new DateTime(today.Year - 4, today.Month, today.Day);
            CalendarDateRange_TooYoung.Start = new DateTime(3000, 1, 1);
            CalendarDateRange_TooYoung.End = fourYearsAgo;

            var fourteenYearsAgo = new DateTime(today.Year - 14, today.Month, today.Day);
            CalendarDateRange_TooOld.Start = new DateTime(1000, 1, 1);
            CalendarDateRange_TooOld.End = fourteenYearsAgo;
        }

        private void Button_ChildNumber_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Calendar_ReferenceDate_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            UpdateCalendarDateRanges(Calendar_ReferenceDate.SelectedDates[0]);
        }
    }
}
