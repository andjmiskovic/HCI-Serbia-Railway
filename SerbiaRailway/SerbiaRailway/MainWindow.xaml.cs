using SerbiaRailway.services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginService login = new LoginService();
            UserType type = login.Login(Username.Text, Password.Password);
            if (type == UserType.MANAGER)
            {
                ManagerWindow managerWindow = new ManagerWindow();
                managerWindow.Show();
            }
            else if (type == UserType.CLIENT)
            {
                ClientWindow clientWindow = new ClientWindow();
                clientWindow.Show();
            }
            else
            {
                ErrorMessage.Opacity = 1;
            }
        }
    }
}
