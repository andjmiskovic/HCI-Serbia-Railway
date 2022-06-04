using SerbiaRailway.services;
using System.Windows;

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
