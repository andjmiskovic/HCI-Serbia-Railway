using SerbiaRailway.model;
using SerbiaRailway.services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (tickets.Count == 0)
            {
                NoTickets.Visibility = Visibility.Visible;
                TicketsScroll.Visibility = Visibility.Hidden;
            }
            else
            {
                NoTickets.Visibility = Visibility.Hidden;
                TicketsScroll.Visibility = Visibility.Visible;
                foreach (Ticket ticket in tickets)
                {
                    Tickets.Children.Add(new TicketCard(ticket));
                }
            }
        }

        private void Reserved_Checked(object sender, RoutedEventArgs e)
        {
            if(Tickets != null)
                FillTickets();
        }

        private void Bought_Checked(object sender, RoutedEventArgs e)
        {
            if(Tickets != null)
                FillTickets();
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpProvider.SetHelpKey((DependencyObject)sender, "myTickets");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
        }
    }
}
