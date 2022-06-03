using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for MyTickets.xaml
    /// </summary>
    public partial class MyTickets : Page
    {
        public Client Client { get; set; }
        public MyTickets()
        {
            this.Client = LoginService.CurrentlyLoggedClient;
            InitializeComponent();
            FillTickets();
        }

        private void FillTickets()
        {
            Tickets.Children.RemoveRange(0, Tickets.Children.Count);
            List<Ticket> tickets;
            if ((bool)Reserved.IsChecked)
                tickets = Client.Reserved;
            else
                tickets = Client.Bought;
            foreach (Ticket ticket in tickets)
            {
                Tickets.Children.Add(new TicketCard(ticket));
            }
        }
    }
}
