using Microsoft.Maps.MapControl.WPF;
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
using SerbiaRailway.model;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        MyTickets MyTickets;
        TimetableClient TimetableClient;
        TrainNetwork TrainNetwork;
        Map Map;
        public ClientWindow()
        {
            InitializeComponent();
            MyTickets = new MyTickets();
            TimetableClient = new TimetableClient();
            TrainNetwork = new TrainNetwork();
            Main.Content = this.MyTickets;
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
        }

        private void SwitchToMyTickets(object sender, RoutedEventArgs e)
        {
            Main.Content = this.MyTickets;
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TrainNetworkBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToTimetableClient(object sender, RoutedEventArgs e)
        {
            Main.Content = this.TimetableClient;
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TrainNetworkBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToTrainNetwork(object sender, RoutedEventArgs e)
        {
            Main.Content = this.TrainNetwork;
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TrainNetworkBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));

            // u svrhu testiranja:
            //List<SerbiaRailway.model.Line> lines = services.Data.Instance.GetLines();

            //List<Location> Locations = SerbiaRailway.services.DataService.getAllLocationsFromLine(lines[3]);

            //foreach (Location loc in Locations)
            //{
            //    addMarker(loc);
            //}

            //for (int i = 0; i < Locations.Count; i++)
            //{
            //    if (Locations.ElementAtOrDefault(i + 1) != null)
            //    {
            //        addNewPolyline(Locations[i], Locations[i + 1]);
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            // ovo treba premestiti samo ne znam gde jos uvek
            void addMarker(Location lok)
            {
                Pushpin pushpin = new Pushpin();
                pushpin.Location = lok;
                this.TrainNetwork.mapa.Children.Add(pushpin);
            }

            void addNewPolyline(Location Lok1, Location Lok2)
            {
                MapPolyline polyline = new MapPolyline();
                polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
                polyline.StrokeThickness = 3;
                polyline.Opacity = 0.7;
                polyline.Locations = new LocationCollection() {
                Lok1, Lok2
            };

                this.TrainNetwork.mapa.Children.Add(polyline);
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


    }
}
