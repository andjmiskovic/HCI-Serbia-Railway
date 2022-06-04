using SerbiaRailway.model;
using System.Windows;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TicketCard.xaml
    /// </summary>
    public partial class TimetableCard : UserControl
    {
        private PartialLine Line;
        private Ride Ride;
        public TimetableCard()
        {
            InitializeComponent();
        }

        public TimetableCard(string startTime, string endTime, string travelTime, PartialLine line, string manufacturer, Ride ride)
        {
            InitializeComponent();
            StartTime.Content = startTime.Substring(0, 5);
            EndTime.Content = endTime.Substring(0, 5);
            TravelTime.Content = travelTime.Substring(0, 5);
            Manufacturer.Content = manufacturer;
            Line = line;
            Ride = ride;
        }

        public void BuyTicket(object sender, RoutedEventArgs e)
        {
            BuyTickets buyTickets = new BuyTickets(Line, Ride);
            buyTickets.Show();
        }
    }
}
