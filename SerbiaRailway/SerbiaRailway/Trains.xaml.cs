using SerbiaRailway.model;
using SerbiaRailway.services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for Trains.xaml
    /// </summary>
    public partial class Trains : Page
    {
        public List<TrainXamlData> xamlData { get; set; }
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
        public Trains()
        {
            InitializeComponent();
            xamlData = new List<TrainXamlData>();
            fillData();
            searchBox.Text = SearchText;
            DataContext = this;
        }

        public void Search()
        {
            xamlData.Clear();
            if (SearchText != "" && SearchText != null)
                mSearchText = mSearchText.ToLower();
            foreach (Train train in DataService.Data.Trains)
            {
                string train_id = train.Id.ToString().ToLower();
                string train_manufacturer = train.Manufacturer.ToLower();
                string train_wagon_num = train.Wagons.Count.ToString().ToLower();
                string train_seat_num = (train.Wagons[0].Seats.Count * train.Wagons.Count).ToString().ToLower();
                if (SearchText == "" || SearchText == null)
                {
                    xamlData.Add(new TrainXamlData(train.Wagons[0].Seats.Count * train.Wagons.Count, train.Wagons.Count,
                        train.Id, train.Manufacturer, train.getExtraPrice()));
                }
                else
                {
                    if (train_id.Contains(mSearchText) || train_manufacturer.Contains(mSearchText) ||
                    train_wagon_num.Contains(mSearchText) || train_seat_num.Contains(mSearchText))
                    {
                        xamlData.Add(new TrainXamlData(train.Wagons[0].Seats.Count * train.Wagons.Count, train.Wagons.Count,
                        train.Id, train.Manufacturer, train.getExtraPrice()));
                    }
                }
            }
            DataGridXAML.Items.Refresh();
        }

        public void fillData()
        {
            foreach (Train t in DataService.Data.Trains)
            {
                TrainXamlData xamldata = new TrainXamlData(t.Wagons[0].Seats.Count * t.Wagons.Count, t.Wagons.Count, t.Id,
                    t.Manufacturer, t.getExtraPrice());
                xamlData.Add(xamldata);
            }
            DataGridXAML.ItemsSource = xamlData;
        }

        private void btnAddNewTrain_Click(object sender, RoutedEventArgs e)
        {
            AddTrainWindow at1 = new AddTrainWindow(xamlData);
            at1.ShowDialog();
            DataGridXAML.Items.Refresh();
        }

        private void btnEditTrain_Click(object sender, RoutedEventArgs e)
        {
            TrainXamlData trainXamlData = (TrainXamlData)DataGridXAML.SelectedItem;
            if (trainXamlData == null)
            {
                MessageBox.Show("You have to select train first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Train train = DataService.Data.GetTrainByManufacturer(trainXamlData.Manufacturer);
            EditTrain editTrain = new EditTrain(xamlData, trainXamlData,
                DataService.Data.GetTrainByManufacturer(trainXamlData.Manufacturer));
            editTrain.ShowDialog();
            DataGridXAML.Items.Refresh();
        }

        private void btnDeleteTrain_Click(object sender, RoutedEventArgs e)
        {
            TrainXamlData trainXamlData = (TrainXamlData)DataGridXAML.SelectedItem;
            if (trainXamlData == null)
            {
                MessageBox.Show("You have to select train first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Train train = DataService.Data.GetTrainByManufacturer(trainXamlData.Manufacturer);
            foreach (Route route in DataService.Data.Routes)
            {
                if (route.Train == train)
                {
                    MessageBox.Show("This train is in use and CANNOT be deleted!", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    return;
                }
            }
            MessageBoxResult decision = MessageBox.Show("Are you sure you want to delete this train?",
                    "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (decision == MessageBoxResult.Yes)
            {
                DataService.Data.Trains.Remove(train);
                xamlData.Remove(trainXamlData);
                DataGridXAML.Items.Refresh();
                MessageBox.Show("Train has been deleted successfully!",
                        "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
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
                HelpProvider.SetHelpKey((DependencyObject)sender, "trains");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
        }

        private void demoBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class TrainXamlData
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public int Seats { get; set; }
        public int Wagons { get; set; }
        public double ExtraPrice { get; set; }

        public TrainXamlData()
        {

        }

        public TrainXamlData(int seats, int wagons, int id, string manufacturer, double price)
        {
            this.Id = id;
            this.Manufacturer = manufacturer;
            this.Seats = seats;
            this.Wagons = wagons;
            this.ExtraPrice = price;
        }
    }
}