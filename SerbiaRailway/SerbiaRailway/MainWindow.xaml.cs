using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserType type = LoginService.Login(Username.Text, Password.Password);
            if (type == UserType.MANAGER)
            {
                ManagerWindow managerWindow = new ManagerWindow();
                managerWindow.Show();
                Close();
            }
            else if (type == UserType.CLIENT)
            {
                ClientWindow clientWindow = new ClientWindow();
                clientWindow.Show();
                Close();
            }
            else
            {
                ErrorMessage.Opacity = 1;
            }
        }
    }
}
