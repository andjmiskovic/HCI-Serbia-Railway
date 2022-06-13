using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            Main.Content = new TimetableManager();
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
        }

        private void SwitchToTrainsPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new Trains();
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            RoutesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TicketsReportBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToLinesPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new Routes();
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            RoutesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TicketsReportBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));

            List<model.Line> lines = services.Data.Instance.Lines;
            foreach(model.Line line in lines)
            {
                line.StationStr = line.GetStationString();
                line.TrainName = line.Route.Train.Manufacturer;
                line.Traveling = line.TravelTime();
                //Lines.DataGridXAML.Items.Add(line);
            }
        }

        private void SwitchToTimetablePage(object sender, RoutedEventArgs e)
        {
            Main.Content = new TimetableManager();
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            RoutesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TicketsReportBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToTicketsReportPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new TicketsReport();
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            RoutesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TicketsReportBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
