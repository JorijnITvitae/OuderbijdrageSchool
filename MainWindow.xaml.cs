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
        private class Child
        {
            private DateTime birthDate;

            public Child()
            {
                this.birthDate = new DateTime(1, 1, 1);
            }

            public DateTime BirthDate
            {
                get => new DateTime(this.birthDate.Ticks);
                set => this.birthDate = new DateTime(value.Ticks);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            UpdateCalendarDateRanges(DateTime.Now);
            InitChildren();
        }

        private Child[] children;

        private void InitChildren()
        {
            children = new Child[5];
        }

        private void Button_ChildNumber_Click(object sender, RoutedEventArgs e)
        {
            Button_Child1.IsEnabled = true;
            Button_Child2.IsEnabled = true;
            Button_Child3.IsEnabled = true;
            Button_Child4.IsEnabled = true;
            Button_Child5.IsEnabled = true;
            ((Button)(sender)).IsEnabled = false;
        }

        private void UpdateCalendarDateRanges(DateTime today)
        {
            var fifteenYearsAgo = new DateTime(today.Year - 15, today.Month, today.Day);
            DatePicker_BirthDate.DisplayDateStart = fifteenYearsAgo;
            var threeYearsAgo = new DateTime(today.Year - 3, today.Month, today.Day);
            DatePicker_BirthDate.DisplayDateEnd = threeYearsAgo;
        }

        private void DatePicker_ReferenceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCalendarDateRanges(DatePicker_ReferenceDate.SelectedDate ?? DateTime.Now);
        }

        private void DatePicker_BirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
