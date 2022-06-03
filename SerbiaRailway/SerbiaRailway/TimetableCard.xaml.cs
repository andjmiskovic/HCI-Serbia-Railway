using System;
using System.Windows;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TicketCard.xaml
    /// </summary>
    public partial class TimetableCard : UserControl
    {
        private PartialLine line;
        public TimetableCard()
        {
            InitializeComponent();
        }

        public TimetableCard(string startTime, string endTime, string travelTime, PartialLine line, string manufacturer)
        {
            InitializeComponent();
            StartTime.Content = startTime.Substring(0, 5);
            EndTime.Content = endTime.Substring(0, 5);
            TravelTime.Content = travelTime.Substring(0, 5);
            Manufacturer.Content = manufacturer;
            this.line = line;
        }

        public void BuyTicket(object sender, RoutedEventArgs e)
        {
            BuyTickets buyTickets = new BuyTickets(line);
            buyTickets.Show();
        }
    }
}
