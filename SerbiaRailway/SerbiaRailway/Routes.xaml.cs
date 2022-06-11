using SerbiaRailway.services;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for Lines.xaml
    /// </summary>
    public partial class Routes : Page
    {
        public Routes()
        {
            InitializeComponent();
            DataGridXAML.ItemsSource = DataService.Data.Routes;
        }
    }
}
