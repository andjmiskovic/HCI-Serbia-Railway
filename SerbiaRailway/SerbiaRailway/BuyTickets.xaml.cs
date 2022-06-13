using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SerbiaRailway.model;
using System.Collections.Generic;
using SerbiaRailway.services;
using System.Windows.Media;
using System.Windows.Input;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for BuyTickets.xaml
    /// </summary>
    public partial class BuyTickets : Window
    {
        private PartialLine line { get; set; }
        private Ride ride { get; set; }
        private Seat SelectedSeat { get; set; }
        private RadioButton PreviousSelectedSeat { get; set; }
        private string PreviousSelectedSeatColor { get; set; }
        private double RidePriceValue { get; set; }
        private List<TakenSeat> TakenSeats { get; set; }
        public BuyTickets()
        {
            InitializeComponent();
        }

        public BuyTickets(PartialLine line, Ride ride)
        {
            this.line = line;
            InitializeComponent();
            AddWagons(line.Line.Route.Train.Wagons.Count());
            Date.Content = ride.Date.ToString("MM/dd/yyyy");
            StartingStation.Content = line.Start.Name;
            StartingTime.Content = line.StartTime().ToString().Substring(0, 5);
            EndingTime.Content = line.EndTime().ToString().Substring(0, 5);
            EndingStation.Content = line.End.Name;
            this.ride = ride;
            DrawSeats();
            RidePriceValue = TicketService.CalculateTicketPrice(line);
            RidePrice.Content = RidePriceValue + "rsd";
            TotalPrice.Content = RidePriceValue + "rsd";
        }

        private void AddWagons(int number)
        {
            for(int i=1; i <= number; i++)
            {
                RadioButton wagon = new RadioButton();
                if (i == 1)
                    wagon.IsChecked = true;
                wagon.Content = i;
                wagon.GroupName = "Wagons";
                wagon.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(DrawSeatsWhenWagonChange));
                WagonStack.Children.Add(wagon);
            }
        }

        private void DrawSeatsWhenWagonChange(object sender, RoutedEventArgs e)
        {
            DrawSeats();
        }

        private void DrawSeats()
        {
            List<TakenSeat> seats = ride.GetAvailableSeats(line.Start, line.End)[GetSelectedWagon()];
            this.TakenSeats = seats;
            int numberOfCols = seats.Count() / 4 + 1;
            SeatsMap.ColumnDefinitions.Clear();
            for (int i=0; i< numberOfCols; i++)
                SeatsMap.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < seats.Count(); i++)
            {
                RadioButton seat = new RadioButton
                {
                    Content = seats[i].seat.SeatNumber.ToString(),
                    Name = "Seat" + i,
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                    Height = 50,
                    Width = 50,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    IsChecked = false,
                };
                if(seats[i].isAvailable)
                {
                    if (seats[i].seat.Type == SeatType.FIRST_CLASS)
                    { 
                        seat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBA311"));
                        seat.ToolTip = "First class seat";
                    }
                    else
                    {
                        seat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8CB2E"));
                        seat.ToolTip = "Second class seat";
                    }
                    seat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                    seat.Cursor = System.Windows.Input.Cursors.Hand;
                } 
                else
                {
                    seat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                    seat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e5e5e5"));
                }
                seat.GroupName = "Seats";
                seat.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(SetSeatNumber));
                seat.Tag = seats[i].seat;
                Grid.SetColumn(seat, i / 4);
                Grid.SetRow(seat, 3 - i % 4);
                SeatsMap.Children.Add(seat);
            }
        }

        private bool isSeatTaken(int number)
        {
            foreach(TakenSeat takenSeat in this.TakenSeats)
            {
                if (!takenSeat.isAvailable && takenSeat.seat.SeatNumber == number)
                    return true;
            }
            return false;
        }

        void SetSeatNumber(object sender, RoutedEventArgs e)
        {
            Seat selected = (Seat)((RadioButton)sender).Tag;
            if (!isSeatTaken(selected.SeatNumber))
            {
                SelectedSeat = selected;
                SeatPrice.Content = SelectedSeat.ExtraPrice + "rsd";
                TotalPrice.Content = RidePriceValue + SelectedSeat.ExtraPrice + "rsd";
                SelectedSeatNumber.Content = SelectedSeat.SeatNumber;
                if(PreviousSelectedSeat != null)
                    PreviousSelectedSeat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.PreviousSelectedSeatColor)); 
                ((RadioButton)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#069A8E"));
                PreviousSelectedSeat = (RadioButton)sender;
                PreviousSelectedSeatColor = SelectedSeat.Type == SeatType.FIRST_CLASS ? "#FBA311" : "#F8CB2E";
            }
        }

        private int GetSelectedWagon()
        {
            List<RadioButton> radioButtons = WagonStack.Children.OfType<RadioButton>().ToList();
            foreach (RadioButton rb in radioButtons)
            {
                if ((bool)rb.IsChecked)
                {
                    return int.Parse(rb.Content.ToString());
                }
            }
            return 1;
        }

        private void ReserveTicket(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = LoginService.CurrentlyLoggedClient;
                if (SelectedSeat == null)
                    MessageBox.Show("You have to select the seat first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to reserve this ticket?", "Reserve ticket", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        int wagon = this.GetSelectedWagon();
                        TicketService.ReserveTicket(client, SelectedSeat, this.line, wagon, ride.Date);
                        MessageBox.Show("You have successfully reserved a ticket.", "Congritulations", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error occured. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BuyTicket(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = LoginService.CurrentlyLoggedClient;
                if (SelectedSeat == null)
                    MessageBox.Show("You have to select the seat first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to buy this ticket?", "Buy ticket", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        int wagon = this.GetSelectedWagon();
                        TicketService.BuyTicket(client, SelectedSeat, this.line, wagon, ride.Date);
                        MessageBox.Show("You have successfully bought a ticket.", "Congritulations", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error occured. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                HelpProvider.SetHelpKey((DependencyObject)sender, "buyTickets");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
        }

        private void demoBtn_Click(object sender, RoutedEventArgs e)
        {
            BuyReserveTicketDemo demo = new BuyReserveTicketDemo();
            demo.ShowDialog();
        }
    }
}
