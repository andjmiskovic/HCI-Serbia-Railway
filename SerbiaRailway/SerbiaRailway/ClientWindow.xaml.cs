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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        MyTickets MyTickets;
        TimetableClient TimetableClient;
        TrainNetwork TrainNetwork;
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
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
