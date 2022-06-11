using SerbiaRailway.model;
using SerbiaRailway.services;
using System.Collections.Generic;
using System.Windows.Controls;

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

        public void OnLineSelected(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedLine = DataService.Data.GetLineById(int.Parse(Line.SelectedItem.ToString().Split('(')[0]));
            _stationSchedules = DataService.Data.Lines[SelectedLine.Id].StationSchedules;
            StationSchedule.ItemsSource = _stationSchedules;
            Monday.IsChecked = SelectedLine.WeekDays["Monday"];
            Tuesday.IsChecked = SelectedLine.WeekDays["Tuesday"];
            Wednesday.IsChecked = SelectedLine.WeekDays["Wednesday"];
            Thursday.IsChecked = SelectedLine.WeekDays["Thursday"];
            Friday.IsChecked = SelectedLine.WeekDays["Friday"];
            Saturday.IsChecked = SelectedLine.WeekDays["Saturday"];
            Sunday.IsChecked = SelectedLine.WeekDays["Sunday"];
        }

        private void SelectAll_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Monday.IsChecked = true;
            Tuesday.IsChecked = true;
            Wednesday.IsChecked = true;
            Thursday.IsChecked = true;
            Friday.IsChecked = true;
            Saturday.IsChecked = true;
            Sunday.IsChecked = true;
        }

        private void SaveChanges(object sender, System.Windows.RoutedEventArgs e)
        {
            List<StationSchedule> stationSchedules = new List<StationSchedule>();
            foreach (StationSchedule stationSchedule in _stationSchedules)
            {
                stationSchedules.Add(new StationSchedule(stationSchedule.StartingStation, stationSchedule.EndStation,
                    stationSchedule.Departure, stationSchedule.Arrival, stationSchedule.Price));
            }
            DataService.Data.Lines[SelectedLine.Id].StationSchedules = stationSchedules;

            Dictionary<string, bool> weekDays = new Dictionary<string, bool>();
            weekDays.Add("Monday", (bool)Monday.IsChecked);
            weekDays.Add("Tuesday", (bool)Tuesday.IsChecked);
            weekDays.Add("Wednesday", (bool)Wednesday.IsChecked);
            weekDays.Add("Thursday", (bool)Thursday.IsChecked);
            weekDays.Add("Friday", (bool)Friday.IsChecked);
            weekDays.Add("Saturday", (bool)Saturday.IsChecked);
            weekDays.Add("Sunday", (bool)Sunday.IsChecked);
            DataService.Data.Lines[SelectedLine.Id].WeekDays = weekDays;
        }
    }
}
