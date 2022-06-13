using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Line = SerbiaRailway.model.Line;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for EditRoute.xaml
    /// </summary>
    public partial class EditRoute : Window
    {
        private ObservableCollection<Route> _observableCollection;
        private Route _route;
        public EditRoute()
        {
            InitializeComponent();
        }

        public EditRoute(Route route, ObservableCollection<Route> observableCollection)
        {
            InitializeComponent();
            foreach (Station station in DataService.Data.Stations)
            {
                if (!route.Stations.Contains(station))
                {
                    ListBoxItem listBoxItem = new ListBoxItem
                    {
                        Content = station.ToString()
                    };
                    AllStations.Items.Add(listBoxItem);
                }
            }

            foreach (Station station in route.Stations)
            {
                ListBoxItem listBoxItem = new ListBoxItem
                {
                    Content = station.ToString()
                };
                RouteStations.Items.Add(listBoxItem);
            }
            _observableCollection = observableCollection;
            _route = route;
            trainComboBox.ItemsSource = DataService.Data.Trains;
            trainComboBox.DisplayMemberPath = "Manufacturer";
            trainComboBox.SelectedValuePath = "Id";
            trainComboBox.SelectedItem = route.Train;
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
                MessageBoxResult decision = MessageBox.Show("Are you sure you want to edit this route?" +
                    "If you edit this route all lines, rides and tickets connected with this route will be DELETED!",
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
                    _route.Name = name;
                    _route.Stations = stations;
                    _route.Train = train;
                    _route.setStationNames();
                    int i = 0;
                    foreach (Route route in _observableCollection)
                    {
                        if (_route.Id == route.Id)
                        {
                            break;
                        }
                        i++;
                    }
                    _observableCollection[i] = _route;
                    foreach (Ride ride in DataService.Data.Rides.ToList())
                    {
                        if (ride.Line.Route.Id == _route.Id)
                        {
                            foreach (Ticket ticket in ride.Tickets.ToList())
                            {
                                DataService.Data.Tickets.Remove(ticket);
                            }
                            DataService.Data.Rides.Remove(ride);
                        }
                    }
                    foreach (Line line in DataService.Data.Lines.ToList())
                    {
                        if (line.Route.Id == _route.Id)
                        {
                            DataService.Data.Lines.Remove(line);
                        }
                    }
                    MessageBox.Show("Route has been edited successfully!",
                    "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows[2].Close();
                }
            }
        }
    }
}
