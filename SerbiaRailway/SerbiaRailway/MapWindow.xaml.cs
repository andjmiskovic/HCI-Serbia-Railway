using Microsoft.Maps.MapControl.WPF;
using SerbiaRailway.model;
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
using System.Windows.Shapes;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow()
        {
            InitializeComponent();
        }

        public MapWindow(PartialLine partialLine)
        {
            InitializeComponent();
            this.Title = partialLine.Start.Name + " - " + partialLine.End.Name;
            showLineOnMap(partialLine.Line);

        }

        private void showLineOnMap(model.Line line)
        {
            this.mapa.Children.Clear();
            for (int i = 0; i < line.StationSchedules.Count(); i++)
            {
                addNewPolyline(line.StationSchedules[i].StartingStation.Location, line.StationSchedules[i].EndStation.Location);
                addMarker(line.StationSchedules[i].StartingStation.Location, i + 1);
                addMarker(line.StationSchedules[i].EndStation.Location, i + 2);
            }
        }

        private void addMarker(Location location, int number)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Location = location;
            pushpin.Content = number;
            this.mapa.Children.Add(pushpin);
        }

        private void addNewPolyline(Location location1, Location location2)
        {
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new SolidColorBrush(Colors.Black);
            polyline.StrokeThickness = 3;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection() {location1, location2};

            this.mapa.Children.Add(polyline);
        }
    }
}
