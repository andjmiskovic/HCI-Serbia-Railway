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
    /// Interaction logic for TimetableManager.xaml
    /// </summary>
    public partial class TimetableManager : Page
    {
        private List<StationSchedule> _stationSchedules { get; set; }
        private Line SelectedLine { get; set; }

        public TimetableManager()
        {
            InitializeComponent();
            FillLines();
        }

        private void FillLines()
        {
            Line.Items.Clear();
            foreach (Line line in DataService.Data.Lines)
            {
                Line.Items.Add(line.Id + " (" + line.Route.Name + ")");
            }
        }

        public void SetLines()
        {
            int lineId = int.Parse(Line.SelectedItem.ToString().Split('(')[0]);
        }

        public void OnLineSelected(object sender, RoutedEventArgs e)
        {
            SelectedLine = DataService.Data.GetLineById(int.Parse(Line.SelectedItem.ToString().Split('(')[0]));
            _stationSchedules = DataService.Data.GetLineById(SelectedLine.Id).StationSchedules;
            StationSchedule.ItemsSource = _stationSchedules;
            Monday.IsChecked = SelectedLine.WeekDays["Monday"];
            Tuesday.IsChecked = SelectedLine.WeekDays["Tuesday"];
            Wednesday.IsChecked = SelectedLine.WeekDays["Wednesday"];
            Thursday.IsChecked = SelectedLine.WeekDays["Thursday"];
            Friday.IsChecked = SelectedLine.WeekDays["Friday"];
            Saturday.IsChecked = SelectedLine.WeekDays["Saturday"];
            Sunday.IsChecked = SelectedLine.WeekDays["Sunday"];
        }

        private void SaveLineChanges()
        {
            if (SelectedLine == null)
            {
                MessageBox.Show("You have to select the line you want to edit first.");
            }
            else
            {
                TimeSpan timeSpan = new TimeSpan(0, 0, 0);
                foreach (StationSchedule stationSchedule in _stationSchedules)
                {
                    if (stationSchedule.Departure >= stationSchedule.Arrival)
                    {
                        MessageBox.Show($"Departure time from station {stationSchedule.StartingStation} must be before arriving to the " +
                            $"{stationSchedule.EndStation} station!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else if (stationSchedule.Departure < timeSpan)
                    {
                        MessageBox.Show($"Departure time from station {stationSchedule.StartingStation} cannot be done " +
                            $"if the train didnt arrive!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else if (stationSchedule.Price < 100)
                    {
                        MessageBox.Show($"Price of a trip between stations cannot be less than 100 dinars!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    timeSpan = stationSchedule.Arrival;
                }
                List<StationSchedule> stationSchedules = new List<StationSchedule>();
                foreach (StationSchedule stationSchedule in _stationSchedules)
                {
                    stationSchedules.Add(new StationSchedule(stationSchedule.StartingStation, stationSchedule.EndStation,
                        stationSchedule.Departure, stationSchedule.Arrival, stationSchedule.Price));
                }
                DataService.Data.GetLineById(SelectedLine.Id).StationSchedules = stationSchedules;

                Dictionary<string, bool> weekDays = new Dictionary<string, bool>();
                weekDays.Add("Monday", (bool)Monday.IsChecked);
                weekDays.Add("Tuesday", (bool)Tuesday.IsChecked);
                weekDays.Add("Wednesday", (bool)Wednesday.IsChecked);
                weekDays.Add("Thursday", (bool)Thursday.IsChecked);
                weekDays.Add("Friday", (bool)Friday.IsChecked);
                weekDays.Add("Saturday", (bool)Saturday.IsChecked);
                weekDays.Add("Sunday", (bool)Sunday.IsChecked);
                DataService.Data.GetLineById(SelectedLine.Id).WeekDays = weekDays;

                MessageBox.Show("Changes saved successfully.");
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            this.SaveLineChanges();
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpProvider.SetHelpKey((DependencyObject)sender, "timetableManager");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                this.SaveLineChanges();
            }
        }
    }

}
