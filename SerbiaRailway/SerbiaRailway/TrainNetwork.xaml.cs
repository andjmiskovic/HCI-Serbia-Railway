using Microsoft.Maps.MapControl.WPF;
using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TrainNetwork.xaml
    /// </summary>
    public partial class TrainNetwork : Page
    {
        public string Line_ID { get; set; }
        public string Line_Name { get; set; }
        public List<Line> Lines = Data.Instance.Lines;
        public TrainNetwork()
        {
            InitializeComponent();
            myComboBox.ItemsSource = Lines;
        }

        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbo = sender as ComboBox;
            Line selectedLine = (Line)cbo.SelectedItem;
            changeTextBlockData(selectedLine);
            showLineOnMap(selectedLine);
        }

        private void changeTextBlockData(Line line)
        {
            string outputString = "Timeline: \n\n";
            int order = 1;
            foreach (StationSchedule ss in line.StationSchedules)
            {
                outputString += order.ToString() + ".  " + ss.StartingStation.ToString() + "\n";
                outputString += "Departure: " + ss.Departure.ToString(@"hh\:mm") + "h\n\n";
                order++;
            }
            outputString += order.ToString() + ".  " + line.StationSchedules.Last().EndStation.ToString() + "\n";
            outputString += "Arrival: " + line.StationSchedules.Last().Arrival.ToString(@"hh\:mm") + "h";
            textblock.Text = outputString;
        }

        private void showLineOnMap(Line line)
        {
            this.mapa.Children.Clear();
            for(int i = 0; i< line.StationSchedules.Count(); i++)
            {
                addNewPolyline(line.StationSchedules[i].StartingStation.Location, line.StationSchedules[i].EndStation.Location);
                addMarker(line.StationSchedules[i].StartingStation.Location, i + 1);
                addMarker(line.StationSchedules[i].EndStation.Location, i + 2);
            }
        }

        void addMarker(Location location, int number)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Location = location;
            pushpin.Content = number;
            this.mapa.Children.Add(pushpin);
        }

        void addNewPolyline(Location location1, Location location2)
        {
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
            polyline.StrokeThickness = 3;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection() {
                location1, location2
            };

            this.mapa.Children.Add(polyline);
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpProvider.SetHelpKey((DependencyObject)sender, "trainNetwork");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
        }

    }

}
