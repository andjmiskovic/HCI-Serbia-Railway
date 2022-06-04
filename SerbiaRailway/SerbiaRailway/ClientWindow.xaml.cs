using System.Windows;
using System.Windows.Media;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            Main.Content = new MyTickets();
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
        }

        private void SwitchToMyTickets(object sender, RoutedEventArgs e)
        {
            Main.Content = new MyTickets();
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TrainNetworkBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToTimetableClient(object sender, RoutedEventArgs e)
        {
            Main.Content = new TimetableClient();
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
            TrainNetworkBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
        }

        private void SwitchToTrainNetwork(object sender, RoutedEventArgs e)
        {
            Main.Content = new TrainNetwork();
            MyTicketsBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TimetableBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            TrainNetworkBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCA311"));
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
