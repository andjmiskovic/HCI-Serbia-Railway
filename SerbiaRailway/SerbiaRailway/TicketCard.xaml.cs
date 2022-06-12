using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TicketCard.xaml
    /// </summary>
    public partial class TicketCard : UserControl
    {
        public Ticket ticket { get; set; }
        public TicketCard(Ticket ticket)
        {
            InitializeComponent();
            this.ticket = ticket;
            FillData();
        }

        private void FillData()
        {
            StartTime.Content = ticket.PartialLine.StartTime.ToString().Substring(0, 5);
            EndTime.Content = ticket.PartialLine.EndTime.ToString().Substring(0, 5);
            StartStation.Content = ticket.PartialLine.Start.Name;
            EndStation.Content = ticket.PartialLine.End.Name;
            WagonNumber.Content = "Wagon: " + ticket.Wagon + ", Seat: " + ticket.Seat.SeatNumber.ToString();
            Price.Content = "Price: " + ticket.Price.ToString() + "rsd";
            Date.Content = ticket.Date.ToString("MM/dd/yyyy");
            if (ticket.State == TicketState.BOUGHT)
            {
                CancelBtn.Visibility = Visibility.Hidden;
                BuyBtn.Visibility = Visibility.Hidden;
            }
        }

        private void CancelReservation(object sender, RoutedEventArgs e)
        {
            LoginService.CurrentlyLoggedClient.Reserved.Remove(ticket);
            DataService.Data.Tickets.Remove(ticket);
            DataService.Data.GetRide(ticket.Date, ticket.PartialLine.Line).Tickets.Remove(ticket);
            MessageBox.Show("Your reservation has been canceled.");
        }

        private void Buy(object sender, RoutedEventArgs e)
        {
            LoginService.CurrentlyLoggedClient.Reserved.Remove(ticket);
            ticket.State = TicketState.BOUGHT;
            LoginService.CurrentlyLoggedClient.Bought.Add(ticket);
            MessageBox.Show("You have bought this ticket.");
        }
    }
}
