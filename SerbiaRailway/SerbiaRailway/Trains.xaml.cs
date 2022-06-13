using SerbiaRailway.model;
using System;
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
        public Trains()
        {
            InitializeComponent();
            fillData();
        }

        public void fillData()
        {
            List<XAMLDATA> xamlData = new List<XAMLDATA>();
            foreach (Train t in services.DataService.Data.Trains)
            {
                XAMLDATA xamldata = new XAMLDATA(t.Wagons[0].Seats.Count*t.Wagons.Count, t.Wagons.Count, t.Id, t.Manufacturer);
                xamlData.Add(xamldata);
            }
            DataGridXAML.ItemsSource = xamlData;
        }

        private void btnAddNewTrain_Click(object sender, RoutedEventArgs e)
        {
            AddTrainWindow at1 = new AddTrainWindow();
            at1.Show();
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
