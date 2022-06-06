using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void SearchLines(object sender, System.Windows.RoutedEventArgs e)
        {
            Station from = DataService.Data.GetStationByName(FromSelect.Text);
            Station to = DataService.Data.GetStationByName(ToSelect.Text);
            DateTime date = DateTime.Now;
            if (Calendar.SelectedDate != null)
                date = (DateTime)Calendar.SelectedDate;
            CardStack.Children.RemoveRange(0, CardStack.Children.Count);
            List<PartialLine> lines = Search(from, to);
            foreach (PartialLine line in lines)
            {
                TimeSpan duration = DateTime.Parse(line.EndTime.ToString()).Subtract(DateTime.Parse(line.StartTime.ToString()));
                // ovde treba da se dobavlja iz fajla ride
                Ride ride = new Ride(date, line.Line);
                CardStack.Children.Add(new TimetableCard(line.StartTime.ToString(), line.EndTime.ToString(), duration.ToString(), line, line.Line.Train.Manufacturer, ride));
            }
            if (lines.Count() == 0)
            {
                Label label = new Label();
                label.Content = "No available lines with these search data.";
                CardStack.Children.Add(label);
            }
        }

        public List<PartialLine> Search(Station from, Station to)
        {
            List<PartialLine> lines = new List<PartialLine>();

            foreach(Line line in DataService.Data.Lines)
            {
                bool hasStart = false;
                bool hasEnd = false;
                TimeSpan startTime = new TimeSpan();
                TimeSpan endTime = new TimeSpan();
                foreach (StationSchedule s in line.StationSchedule)
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

    // deo linije, od jedne stranice do druge
    public class PartialLine
    {
        public Station Start { get; set; }
        public Station End { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Line Line { get; set; }

        public PartialLine(Station start, Station end, TimeSpan startTime, TimeSpan endTime, Line line)
        {
            Start = start;
            End = end;
            StartTime = startTime;
            EndTime = endTime;
            Line = line;
        }
    }
}
