using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for Lines.xaml
    /// </summary>
    public partial class Routes : Page
    {
        ObservableCollection<Route> observableCollection = new ObservableCollection<Route>();
        public Routes()
        {
            InitializeComponent();
            foreach (Route route in DataService.Data.Routes)
            {
                observableCollection.Add(route);
            }
            DataGridXAML.ItemsSource = observableCollection;
        }

        private void btnDeleteLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataService.Data.Routes.Remove((Route)DataGridXAML.SelectedItem);
            observableCollection.Remove((Route)DataGridXAML.SelectedItem);
        }

        private void btnAddNewLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddRoute addRoute = new AddRoute(observableCollection);
            addRoute.ShowDialog();
            DataGridXAML.Items.Refresh();
        }

        private void btnEditLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditRoute editRoute = new EditRoute((Route)DataGridXAML.SelectedItem, observableCollection);
            editRoute.ShowDialog();
            DataGridXAML.Items.Refresh();
        }
    }
}
