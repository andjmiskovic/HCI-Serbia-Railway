using SerbiaRailway.model;
using SerbiaRailway.services;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for MyTickets.xaml
    /// </summary>
    public partial class MyTickets : Page
    {
        public MyTickets()
        {
            InitializeComponent();
            FillTickets();
        }

        private void FillTickets()
        {
            Tickets.Children.RemoveRange(0, Tickets.Children.Count);
            List<Ticket> tickets;
            if ((bool)Reserved.IsChecked)
                tickets = LoginService.CurrentlyLoggedClient.Reserved;
            else
                tickets = LoginService.CurrentlyLoggedClient.Bought;
            foreach (Ticket ticket in tickets)
            {
                Tickets.Children.Add(new TicketCard(ticket));
            }
        }

        private void Reserved_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if(Tickets != null)
                FillTickets();
        }

        private void Bought_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if(Tickets != null)
                FillTickets();
        }
    }
}
