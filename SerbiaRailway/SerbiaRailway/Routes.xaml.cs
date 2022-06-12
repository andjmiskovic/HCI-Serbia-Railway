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
        private string mSearchText;
        public string SearchText
        {
            get => mSearchText;
            set
            {
                mSearchText = value;
                Search();
            }
        }

        public void Search()
        {
            observableCollection.Clear();
            mSearchText = mSearchText.ToLower();
            foreach (Route route in DataService.Data.Routes)
            {
                string route_id = route.Id.ToString().ToLower();
                string route_name = route.Name.ToLower();
                string route_stations = route.StationNames.ToLower();
                string route_train = route.Train.ToString().ToLower();
                if (SearchText == "" || SearchText == null)
                {
                    observableCollection.Add(route);
                }
                else
                {
                    if (route_id.Contains(mSearchText) || route_name.Contains(mSearchText) ||
                    route_stations.Contains(mSearchText) || route_train.Contains(mSearchText))
                    {
                        observableCollection.Add(route);
                    }
                }
            }
            DataGridXAML.Items.Refresh();
        }

        public Routes()
        {
            InitializeComponent();
            foreach (Route route in DataService.Data.Routes)
            {
                observableCollection.Add(route);
            }
            DataGridXAML.ItemsSource = observableCollection;
            searchBox.Text = SearchText;
            DataContext = this;
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
            SearchText = mSearchText;
            DataGridXAML.Items.Refresh();
        }
    }
}
