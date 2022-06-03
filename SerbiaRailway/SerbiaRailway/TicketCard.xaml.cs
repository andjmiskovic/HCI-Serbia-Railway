using SerbiaRailway.model;
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
            StartTime.Content = ticket.PartialLine.StartTime.ToString().Substring(5);
            EndTime.Content = ticket.PartialLine.EndTime.ToString().Substring(5);
            StartStation.Content = ticket.PartialLine.Start.Name;
            EndStation.Content = ticket.PartialLine.End.Name;
            TimeSpan duration = DateTime.Parse(ticket.PartialLine.StartTime.ToString()).Subtract(DateTime.Parse(ticket.PartialLine.EndTime.ToString()));
            TravelTime.Content = duration.ToString().Substring(5);
            SeatNumber.Content = "Seat: " + ticket.Seat.SeatNumber;
            WagonNumber.Content = "Wagon: " + ticket.Wagon;
            Price.Content = "Price: " + ticket.Price.ToString() + "din";
            Date.Content = ticket.Date.ToString();
        }
    }
}
