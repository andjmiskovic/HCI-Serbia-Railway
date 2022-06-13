using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Station from = DataService.Data.GetStationByName(FromSelect.Text);
            Station to = DataService.Data.GetStationByName(ToSelect.Text);
            if (from == null || to == null)
            {
                MessageBox.Show("Please select start and end station.");
                return;
            }
            DateTime date;
            if (Calendar.SelectedDate != null)
                date = (DateTime)Calendar.SelectedDate;
            else
            {
                MessageBox.Show("Please select the desired date of your travel.");
                return;
            }
            CardStack.Children.RemoveRange(0, CardStack.Children.Count);
            List<PartialLine> lines = Search(from, to);
            int count = 0;
            foreach (PartialLine line in lines)
            {
                TimeSpan duration = DateTime.Parse(line.EndTime.ToString()).Subtract(DateTime.Parse(line.StartTime.ToString()));
                Ride ride = DataService.Data.GetRide(date, line.Line);
                if (ride != null)
                {
                    CardStack.Children.Add(new TimetableCard(line.StartTime.ToString(), line.EndTime.ToString(), duration.ToString(), line, line.Line.Route.Train.Manufacturer, ride));
                    count++;
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
                TimeSpan startTime = new TimeSpan();
                TimeSpan endTime = new TimeSpan();
                foreach (StationSchedule s in line.StationSchedules)
                {
                    if (!hasStart && s.StartingStation.Name.Equals(from.Name))
                    {
                        hasStart = true;
                        startTime = s.Departure;
                    }
                    if (hasStart && s.EndStation.Name.Equals(to.Name))
                    {
                        hasEnd = true;
                        endTime = s.Arrival;
                        break;
                    }
                }
                if (hasEnd)
                    lines.Add(new PartialLine(from, to, startTime, endTime, line));
            }
            return lines;
        }

    }
}
