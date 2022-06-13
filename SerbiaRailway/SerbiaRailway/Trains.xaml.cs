using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for Trains.xaml
    /// </summary>
    public partial class Trains : Page
    {
        public List<XAMLDATA> xamlData { get; set; }
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
            xamlData = new List<XAMLDATA>();
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
                    xamlData.Add(new XAMLDATA(train.Wagons[0].Seats.Count * train.Wagons.Count, train.Wagons.Count, 
                        train.Id, train.Manufacturer));
                }
                else
                {
                    if (train_id.Contains(mSearchText) || train_manufacturer.Contains(mSearchText) ||
                    train_wagon_num.Contains(mSearchText) || train_seat_num.Contains(mSearchText))
                    {
                        xamlData.Add(new XAMLDATA(train.Wagons[0].Seats.Count * train.Wagons.Count, train.Wagons.Count,
                        train.Id, train.Manufacturer));
                    }
                }
            }
            DataGridXAML.Items.Refresh();
        }

        public void fillData()
        {
            foreach (Train t in DataService.Data.Trains)
            {
                XAMLDATA xamldata = new XAMLDATA(t.Wagons[0].Seats.Count * t.Wagons.Count, t.Wagons.Count, t.Id,
                    t.Manufacturer);
                xamlData.Add(xamldata);
            }
            DataGridXAML.ItemsSource = xamlData;
        }

        private void btnAddNewTrain_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddTrainWindow at1 = new AddTrainWindow(xamlData);
            at1.ShowDialog();
            DataGridXAML.Items.Refresh();
        }
    }

    public class XAMLDATA
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfWagons { get; set; }

        public XAMLDATA()
        {

        }

        public XAMLDATA(int seats, int wagons, int id, string manufacturer)
        {
            this.Id = id;
            this.Manufacturer = manufacturer;
            this.NumberOfSeats = seats;
            this.NumberOfWagons = wagons;

        }
    }
}
