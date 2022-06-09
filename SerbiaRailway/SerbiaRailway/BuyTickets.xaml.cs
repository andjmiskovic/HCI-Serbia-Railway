using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SerbiaRailway.model;
using System.Collections.Generic;
using SerbiaRailway.services;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Input;
using System;

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
        private System.Windows.Controls.RadioButton PreviousSelectedSeat { get; set; }
        private String PreviousSelectedSeatColor { get; set; }
        private double RidePriceValue { get; set; }
        public BuyTickets()
        {
            InitializeComponent();
        }

        public BuyTickets(PartialLine line, Ride ride)
        {
            this.line = line;
            InitializeComponent();
            AddWagons(line.Line.Train.Wagons.Count());
            Date.Content = ride.Date.ToString("MM/dd/yyyy");
            StartingStation.Content = line.Start.Name;
            StartingTime.Content = line.StartTime.ToString().Substring(0, 5);
            EndingTime.Content = line.EndTime.ToString().Substring(0, 5);
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
                System.Windows.Controls.RadioButton wagon = new System.Windows.Controls.RadioButton();
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
            int numberOfCols = seats.Count() / 4 + 1;
            SeatsMap.ColumnDefinitions.Clear();
            for (int i=0; i< numberOfCols; i++)
                SeatsMap.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < seats.Count(); i++)
            {
                System.Windows.Controls.RadioButton seat = new System.Windows.Controls.RadioButton
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

        void SetSeatNumber(object sender, RoutedEventArgs e)
        {
            SelectedSeat = (Seat)((System.Windows.Controls.RadioButton)sender).Tag;
            SeatPrice.Content = SelectedSeat.ExtraPrice + "rsd";
            TotalPrice.Content = RidePriceValue + SelectedSeat.ExtraPrice + "rsd";
            SelectedSeatNumber.Content = SelectedSeat.SeatNumber;
            if(PreviousSelectedSeat != null)
                PreviousSelectedSeat.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.PreviousSelectedSeatColor)); 
            ((System.Windows.Controls.RadioButton)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#069A8E"));
            PreviousSelectedSeat = (System.Windows.Controls.RadioButton)sender;
            PreviousSelectedSeatColor = SelectedSeat.Type == SeatType.FIRST_CLASS ? "#FBA311" : "#F8CB2E";
        }

        private int GetSelectedWagon()
        {
            List<System.Windows.Controls.RadioButton> radioButtons = WagonStack.Children.OfType<System.Windows.Controls.RadioButton>().ToList();
            foreach (System.Windows.Controls.RadioButton rb in radioButtons)
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
            Client client = LoginService.CurrentlyLoggedClient;
            Seat seat = ride.GetAvailableSeats(this.line.Start, this.line.End)[this.GetSelectedWagon()][1].seat;
            int wagon = this.GetSelectedWagon();
            TicketService.ReserveTicket(client, seat, this.line, wagon, ride.Date);
        }

        private void BuyTicket(object sender, RoutedEventArgs e)
        {
            TicketService.BuyTicket(LoginService.CurrentlyLoggedClient, ride.GetAvailableSeats(this.line.Start, this.line.End)[this.GetSelectedWagon()][1].seat, this.line, this.GetSelectedWagon(), ride.Date);
        }
    }
}
