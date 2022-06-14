using SerbiaRailway.demos;
using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TimetableClient.xaml
    /// </summary>
    public partial class TimetableClient : Page
    {
        public TimetableClient()
        {
            InitializeComponent();
            FromSelect.ItemsSource = DataService.Data.GetStationNames();
            ToSelect.ItemsSource = DataService.Data.GetStationNames();
            Calendar.BlackoutDates.AddDatesInPast();
        }

        private void SearchLines(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search() 
        { 
            Station from = DataService.Data.GetStationByName(FromSelect.Text);
            Station to = DataService.Data.GetStationByName(ToSelect.Text);
            if (from == null || to == null)
            {
                MessageBox.Show("Please select start and end station.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime date;
            if (Calendar.SelectedDate != null)
                date = (DateTime)Calendar.SelectedDate;
            else
            {
                MessageBox.Show("Please select the desired date of your travel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CardStack.Children.RemoveRange(0, CardStack.Children.Count);
            List<PartialLine> lines = Search(from, to);
            int count = 0;
            foreach (PartialLine line in lines)
            {
                TimeSpan duration = DateTime.Parse(line.EndTime().ToString()).Subtract(DateTime.Parse(line.StartTime().ToString()));
                Ride ride = DataService.Data.GetRide(date, line.Line);
                if (ride != null)
                {
                    int isToday = DateTime.Compare(ride.Date.Date, DateTime.Now.Date);
                    if (isToday == 1 || (isToday == 0 && TimeSpan.Compare(line.StartTime(), DateTime.Now.TimeOfDay) == 1))
                    {
                        CardStack.Children.Add(new TimetableCard(line.StartTime().ToString(), line.EndTime().ToString(), duration.ToString(), line, line.Line.Route.Train.Manufacturer, ride));
                        count++;
                    }
                }
            }
            if (count == 0)
            {
                Label label = new Label
                {
                    Content = "No available lines for the selected date.",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 20
                };
                CardStack.Children.Add(label);
            }
        }

        public List<PartialLine> Search(Station from, Station to)
        {
            WelcomePicture.Visibility = Visibility.Hidden;
            List<PartialLine> lines = new List<PartialLine>();

            foreach (Line line in DataService.Data.Lines)
            {
                bool hasStart = false;
                bool hasEnd = false;
                foreach (StationSchedule s in line.StationSchedules)
                {
                    if (!hasStart && s.StartingStation.Name.Equals(from.Name))
                    {
                        hasStart = true;
                    }
                    if (hasStart && s.EndStation.Name.Equals(to.Name))
                    {
                        hasEnd = true;
                        break;
                    }
                }
                if (hasEnd)
                {
                    lines.Add(new PartialLine(from, to, line));
                }
            }
            return lines;
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpProvider.SetHelpKey((DependencyObject)sender, "timetableClient");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
            if (e.Key == Key.Enter)
            {
                Search();
            }
            if (e.Key == Key.D && Keyboard.Modifiers == ModifierKeys.Control)
            {
                PlayDemo();
            }
        }

        private void PlayDemo()
        {
            TimetableClientDemo timetableClientDemo = new TimetableClientDemo();
            timetableClientDemo.ShowDialog();
        }

        private void demoBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayDemo();
        }
    }
}
