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
            addMarker(partialLine.Start.Location);
            addMarker(partialLine.End.Location);
            addNewPolyline(partialLine.Start.Location, partialLine.End.Location);

        }

        void addMarker(Location location)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Location = location;
            this.mapa.Children.Add(pushpin);
        }

        void addNewPolyline(Location location1, Location location2
            )
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
