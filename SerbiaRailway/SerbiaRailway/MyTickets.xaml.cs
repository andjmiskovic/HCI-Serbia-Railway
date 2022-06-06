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
            Label label = new Label();
            label.FontSize = 20;
            if ((bool)Reserved.IsChecked)
            {
                tickets = LoginService.CurrentlyLoggedClient.Reserved;
                label.Content = "You haven't reserved any tickets yet.";
            }
            else
            {
                tickets = LoginService.CurrentlyLoggedClient.Bought;
                label.Content = "You haven't bought any tickets yet.";
            }
            foreach (Ticket ticket in tickets)
            {
                Tickets.Children.Add(new TicketCard(ticket));
            }
            if (tickets.Count == 0)
                Tickets.Children.Add(label);
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
