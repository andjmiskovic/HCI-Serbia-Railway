using SerbiaRailway.model;
using System;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TicketCard.xaml
    /// </summary>
    public partial class TicketCardManager : UserControl
    {
        public Ticket ticket { get; set; }
        public TicketCardManager(Ticket ticket)
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
            TimeSpan duration = DateTime.Parse(ticket.PartialLine.EndTime.ToString()).Subtract(DateTime.Parse(ticket.PartialLine.StartTime.ToString()));
            WagonNumber.Content = "Wagon: " + ticket.Wagon + ", Seat: " + ticket.Seat.SeatNumber;
            Price.Content = "Price: " + ticket.Price.ToString() + "rsd";
            Date.Content = ticket.Date.ToString("MM/dd/yyyy");
            Client.Content = ticket.Client.Name + " " + ticket.Client.LastName;
        }
    }
}
