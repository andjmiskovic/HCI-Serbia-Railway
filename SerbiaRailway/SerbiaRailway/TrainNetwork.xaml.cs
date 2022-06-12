using Microsoft.Maps.MapControl.WPF;
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
            //this.myComboBox.Text = "LINE - " + selectedLine.Id + " - " + selectedLine.Name;
            changeTextBlockData(selectedLine);
            showLineOnMap(selectedLine);
        }

        private void changeTextBlockData(Line line)
        {
            string outputString = "\n\tStation-Time list: \n\n";
            int order = 1;
            foreach (StationSchedule ss in line.StationSchedules)
            {
                outputString += order.ToString() + ".  " + ss.StartingStation.ToString() + "\n";
                outputString += "\t" + ss.Departure.ToString(@"hh\:mm") + "\n\n";
                order++;
            }
            outputString += order.ToString() + ".  " + line.StationSchedules.Last().EndStation.ToString() + "\n";
            outputString += "\t" + line.StationSchedules.Last().Arrival.ToString(@"hh\:mm");
            textblock.Text = outputString;
        }

        private void showLineOnMap(Line line)
        {
            this.mapa.Children.Clear();

            foreach (StationSchedule ss in line.StationSchedules)
            {
                addMarker(ss.StartingStation.Location);
                addMarker(ss.EndStation.Location);
                addNewPolyline(ss.StartingStation.Location, ss.EndStation.Location);
            }
        }

        void addMarker(Location location)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Location = location;
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

    }

}
