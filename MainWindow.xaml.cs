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
            public bool Older;

            public Child()
            {
                this.birthDate = new DateTime(1, 1, 1);
                this.Older = false;
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
            UpdateCost();
        }

        private Child[] children;
        private int currentChild;

        private void InitChildren()
        {
            children = new Child[5];
            for (int i = 0; i < 5; i++)
                children[i] = new Child();

            currentChild = 1;
            Button_Child1.IsEnabled = false;
        }

        private void Button_ChildNumber_Click(object sender, RoutedEventArgs e)
        {
            Button_Child1.IsEnabled = true;
            Button_Child2.IsEnabled = true;
            Button_Child3.IsEnabled = true;
            Button_Child4.IsEnabled = true;
            Button_Child5.IsEnabled = true;

            var button = (Button)(sender);
            button.IsEnabled = false;

            currentChild = Convert.ToInt32(button.Content);
            DateTime birthDate = children[currentChild - 1].BirthDate;

            if (birthDate.Year != 1)
                DatePicker_BirthDate.SelectedDate = birthDate;
            else
                DatePicker_BirthDate.Text = "";
        }

        private void UpdateCalendarDateRanges(DateTime reference)
        {
            var fifteenYearsAgo = new DateTime(reference.Year - 15, reference.Month, reference.Day);
            DatePicker_BirthDate.DisplayDateStart = fifteenYearsAgo;
            var threeYearsAgo = new DateTime(reference.Year - 3, reference.Month, reference.Day);
            DatePicker_BirthDate.DisplayDateEnd = threeYearsAgo;
            DatePicker_BirthDate.DisplayDate = threeYearsAgo;
        }

        private void DatePicker_ReferenceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var reference = DatePicker_ReferenceDate.SelectedDate ?? DateTime.Now;
            UpdateCalendarDateRanges(reference);
            UpdateOlder(reference);
            UpdateCost();
        }

        private void DatePicker_BirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePicker_BirthDate.SelectedDate != null)
                children[currentChild - 1].BirthDate = DatePicker_BirthDate.SelectedDate ?? DateTime.Now;
            else
                children[currentChild - 1].BirthDate = new DateTime(1, 1, 1);

            var reference = DatePicker_ReferenceDate.SelectedDate ?? DateTime.Now;
            UpdateOlder(reference);
            UpdateCost();
        }

        private void CheckBox_SingleParent_Changed(object sender, RoutedEventArgs e)
        {
            UpdateCost();
        }

        private void UpdateOlder(DateTime reference)
        {
            var tenYearsBeforeReferenceDate = new DateTime(reference.Year - 10, reference.Month, reference.Day);

            if (children[currentChild - 1].BirthDate > tenYearsBeforeReferenceDate)
                children[currentChild - 1].Older = false;
            else
                children[currentChild - 1].Older = true;
        }

        private void UpdateCost()
        {
            double cost = 50;

            int childrenYounger = 0;
            foreach (var child in children)
            {
                if (child.BirthDate.Year == 1)
                    continue;

                if (child.Older)
                {
                    cost += 37;
                    childrenYounger++;
                }

                if (childrenYounger > 3)
                    break;
            }

            int childrenOlder = 0;
            foreach (var child in children)
            {
                if (child.BirthDate.Year == 1)
                    continue;

                if (!child.Older)
                {
                    cost += 25;
                    childrenOlder++;
                }

                if (childrenOlder > 2)
                    break;
            }

            if (cost > 150)
                cost = 150;

            if (CheckBox_SingleParent.IsChecked ?? true)
                cost *= 0.75;

            TextBox_Cost.Text = cost.ToString();
        }
    }
}
