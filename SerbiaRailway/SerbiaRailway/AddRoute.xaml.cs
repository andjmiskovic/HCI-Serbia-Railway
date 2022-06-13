using SerbiaRailway.demos;
using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for AddRoute.xaml
    /// </summary>
    public partial class AddRoute : Window
    {
        private ObservableCollection<Route> _observableCollection;
        public AddRoute(ObservableCollection<Route> observableCollection)
        {
            InitializeComponent();
            foreach (Station station in DataService.Data.Stations)
            {
                ListBoxItem listBoxItem = new ListBoxItem
                {
                    Content = station.ToString()
                };
                AllStations.Items.Add(listBoxItem);
            }
            _observableCollection = observableCollection;
            trainComboBox.ItemsSource = DataService.Data.Trains;
            trainComboBox.DisplayMemberPath = "Manufacturer";
            trainComboBox.SelectedValuePath = "Id";
        }

        Point AllStationsStartMousePos;
        Point RouteStationsStartMousePos;

        private void AllStations_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AllStationsStartMousePos = e.GetPosition(null);
        }

        private void AllStations_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                if (!AllStations.Items.Contains(listItem))
                {
                    AllStations.Items.Add(listItem);
                }
            }
        }

        private void AllStations_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                AllStations.Items.Remove(listItem);
            }
        }

        private void AllStations_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selectedItem = (ListBoxItem)AllStations.SelectedItem;
                    AllStations.Items.Remove(selectedItem);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Move);
                    if (selectedItem.Parent == null)
                    {
                        AllStations.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }

        private void RouteStations_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RouteStationsStartMousePos = e.GetPosition(null);
        }

        private void RouteStations_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                if (!RouteStations.Items.Contains(listItem))
                {
                    RouteStations.Items.Add(listItem);
                }
            }
        }

        private void RouteStations_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                RouteStations.Items.Remove(listItem);
            }
        }

        private void RouteStations_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selectedItem = (ListBoxItem)RouteStations.SelectedItem;
                    RouteStations.Items.Remove(selectedItem);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Move);
                    if (selectedItem.Parent == null)
                    {
                        RouteStations.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpProvider.SetHelpKey((DependencyObject)sender, "addRoute");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (RouteStations.Items.Count < 2)
            {
                MessageBox.Show("Please add 2 stations to your route stations list!", "Not enough stations", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (trainComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a train!", "No train selected", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult decision = MessageBox.Show("Are you sure you want to add this route?",
                    "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (decision == MessageBoxResult.Yes)
                {
                    List<Station> stations = new List<Station>();
                    foreach (ListBoxItem station in RouteStations.Items)
                    {
                        stations.Add(DataService.Data.GetStationByName((string)station.Content));
                    }
                    Train train = (Train)trainComboBox.SelectedItem;
                    string name = stations[0] + "-" + stations[stations.Count - 1];
                    int id = ++DataService.Data.Num_routes;
                    Route route = new Route(id, name, stations, train);
                    DataService.Data.Routes.Add(route);
                    _observableCollection.Add(route);
                    MessageBox.Show("Route has been added successfully!",
                    "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows[2].Close();
                }
            }
        }

        private void demoBtn_Click(object sender, RoutedEventArgs e)
        {
            AddRouteDemo addRouteDemo = new AddRouteDemo();
            addRouteDemo.ShowDialog();
        }
    }
}
