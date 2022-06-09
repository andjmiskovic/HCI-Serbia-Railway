using SerbiaRailway.model;
using System;
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
            TimeSpan duration = DateTime.Parse(ticket.PartialLine.EndTime.ToString()).Subtract(DateTime.Parse(ticket.PartialLine.StartTime.ToString()));
            TravelTime.Content = duration.ToString().Substring(0, 5);
            SeatNumber.Content = "Seat: " + ticket.Seat.SeatNumber;
            WagonNumber.Content = "Wagon: " + ticket.Wagon;
            Price.Content = "Price: " + ticket.Price.ToString() + "rsd";
            Date.Content = ticket.Date.ToString("MM/dd/yyyy");
        }
    }
}
