using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using SerbiaRailway.model;
using System.Collections.Generic;
using SerbiaRailway.services;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for BuyTickets.xaml
    /// </summary>
    public partial class BuyTickets : Window
    {
        private PartialLine line { get; set; }
        private Ride ride { get; set; }
        public BuyTickets()
        {
            InitializeComponent();
        }

        public BuyTickets(PartialLine line, Ride ride)
        {
            this.line = line;
            InitializeComponent();
            AddWagons(line.Line.Train.Wagons.Count());
            Date.Content = ride.Date.ToString("MM/dd/yyyy");
            StartingStation.Content = line.Start.Name;
            StartingTime.Content = line.StartTime.ToString().Substring(0, 5);
            EndingTime.Content = line.EndTime.ToString().Substring(0, 5);
            EndingStation.Content = line.End.Name;
            this.ride = ride;
        }

        private void AddWagons(int number)
        {
            for(int i=1; i <= number; i++)
            {
                RadioButton wagon = new RadioButton();
                if (i == 1)
                    wagon.IsChecked = true;
                wagon.Content = i;
                wagon.GroupName = "Wagons";
                WagonStack.Children.Add(wagon);
            }
        }

        private void DrawSeats(List<Seat> seats)
        {
            for (int i = 1; i <= seats.Count(); i++)
            {
                RadioButton wagon = new RadioButton();
                if (i == 1)
                    wagon.IsChecked = true;
                wagon.Content = i;
                wagon.GroupName = "Wagons";
                WagonStack.Children.Add(wagon);
            }
        }

        private int GetSelectedWagon()
        {
            List<RadioButton> radioButtons = WagonStack.Children.OfType<RadioButton>().ToList();
            foreach (RadioButton rb in radioButtons)
            {
                if ((bool)rb.IsChecked)
                {
                    return int.Parse(rb.Content.ToString());
                }
            }
            return 1;
        }

        private void ReserveTicket(object sender, RoutedEventArgs e)
        {
            TicketService.ReserveTicket(LoginService.CurrentlyLoggedClient, ride.GetAvailableSeats(this.line.Start, this.line.End)[this.GetSelectedWagon()][1].seat, this.line, this.GetSelectedWagon());
        }

        private void BuyTicket(object sender, RoutedEventArgs e)
        {
            TicketService.BuyTicket(LoginService.CurrentlyLoggedClient, ride.GetAvailableSeats(this.line.Start, this.line.End)[this.GetSelectedWagon()][1].seat, this.line, this.GetSelectedWagon());
        }
    }
}
