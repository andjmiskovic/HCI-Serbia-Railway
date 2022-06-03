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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        Trains Trains;
        Lines Lines;
        TimetableManager TimetableManager;
        TicketsReport TicketsReport;
        public ManagerWindow()
        {
            InitializeComponent();
            Trains = new Trains();
            Lines = new Lines();
            TimetableManager = new TimetableManager();
            TicketsReport = new TicketsReport();
            Main.Content = TimetableManager;
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
        }

        private void SwitchToTrainsPage(object sender, RoutedEventArgs e)
        {
            Main.Content = Trains;
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            LinesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TicketsReportBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToLinesPage(object sender, RoutedEventArgs e)
        {
            Main.Content = Lines;
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            LinesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TicketsReportBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToTimetablePage(object sender, RoutedEventArgs e)
        {
            Main.Content = TimetableManager;
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            LinesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TicketsReportBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToTicketsReportPage(object sender, RoutedEventArgs e)
        {
            Main.Content = TicketsReport;
            TrainsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            LinesBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
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
