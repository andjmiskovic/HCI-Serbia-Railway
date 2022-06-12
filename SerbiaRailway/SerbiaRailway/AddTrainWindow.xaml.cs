using SerbiaRailway.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for AddTrainWindow.xaml
    /// </summary>
    public partial class AddTrainWindow : Window
    {
        AddTrainPage1 page1 = new AddTrainPage1();
        AddTrainPage2 page2 = new AddTrainPage2();
        public string Manufacturer { get; set; }
        public int WagonsNumber { get; set; }
        public int ExtraPrice { get; set; }
        public int SeatsNumber { get; set; }

        public Dictionary<int, List<Seat>> wagons = new Dictionary<int, List<Seat>>();
        public int SelectedWagon { get; set; }
        public AddTrainWindow()
        {
            InitializeComponent();
            Main.Content = page1;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (page1.Manufacturer_TextBox.Text == "Insert train manufacturer name" || page1.Manufacturer_TextBox.Text == "")
            {
                MessageBox.Show("Please fill manufacturer name.");
            }
            else
            {
                Manufacturer = page1.Manufacturer_TextBox.Text;
                ComboBoxItem typeItem = (ComboBoxItem)page1.Wagons_ComboBox.SelectedItem;
                WagonsNumber = Convert.ToInt32(typeItem.Content.ToString());
                if (page1.Price_TextBox.Text == "Price" || page1.Price_TextBox.Text == "")
                {
                    MessageBox.Show("Please fill extra price.");
                }
                else if (int.TryParse(page1.Price_TextBox.Text, out _) == false)
                {
                    MessageBox.Show("Seats input must be number!");
                }
                else
                {
                    ExtraPrice = Convert.ToInt32(page1.Price_TextBox.Text);

                    if (page1.Seat_TextBox.Text == "Seats" || page1.Seat_TextBox.Text == "")
                    {
                        MessageBox.Show("Please fill number of seats.");
                    }
                    else if (int.TryParse(page1.Seat_TextBox.Text, out _) == false)
                    {
                        MessageBox.Show("Seat input must be number!");
                    }
                    else
                    {
                        SeatsNumber = Convert.ToInt32(page1.Seat_TextBox.Text);
                        LoadSecondPage();
                    }
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            List<Wagon> wagonsList = new List<Wagon>();
            for (int i = 1; i < WagonsNumber+1; i++)
            {
                Wagon wagon = new Wagon(i, wagons[i]);
                wagonsList.Add(wagon);
            }
            
            Train train = new Train(services.Data.Instance.Trains.Last().Id+1, Manufacturer, wagonsList, true);
            services.DataService.Data.Trains.Add(train);
            MessageBox.Show("Uspesno ste dodali voz!");
            this.Close();
        }

        private void LoadSecondPage()
        {
            Main.Content = page2;
            NextButton.Visibility = Visibility.Hidden;            //ovo mora vako jer se pozivaju 
            CreateButton.Visibility = Visibility.Visible; // funkcije kojima trebaju polja iz ove klase
            SeatsLabel.Visibility = Visibility.Visible;
            Wagons_ComboBox.Visibility = Visibility.Visible;

            FillWagonsComboBox();
            FillWagons();
            Wagons_ComboBox.SelectedIndex = 0;
        }

        private void FillWagonsComboBox()
        {
            List<int> wagons = new List<int>();
            for (int i = 1; i <= WagonsNumber; i++)
            {
                wagons.Add(i);
            }
            Wagons_ComboBox.ItemsSource = wagons;
        }

        public void FillWagons()
        {
            for (int i = 1; i < WagonsNumber + 1; i++)
            {
                wagons.Add(i, CreateSeats());
            }
        }

        public List<Seat> CreateSeats()
        {
            List<Seat> defaultSeats = new List<Seat>();
            for (int i = 1; i < SeatsNumber + 1; i++)
            {
                defaultSeats.Add(new Seat(i, SeatType.SECOND_CLASS, 0));
            }
            return defaultSeats;
        }

        private void wagonsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWagon = (int)Wagons_ComboBox.SelectedItem;
            page2.SeatsMap.Children.Clear(); // da ne iscrtava mapu sedista jednu preko druge
            DrawSeats();
        }

        private void DrawSeats()
        {
            List<Seat> seats = wagons[SelectedWagon];
            int numberOfCols = seats.Count / 4 + 1;
            page2.SeatsMap.ColumnDefinitions.Clear();
            for (int i = 0; i < numberOfCols; i++)
                page2.SeatsMap.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < seats.Count; i++)
            {
                Button seat = new Button
                {
                    Content = seats[i].SeatNumber.ToString(),
                    Name = "Seat" + i,
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                    Height = 50,
                    Width = 50,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                //seat.Resources.Add(typeof(Border), style);

                if (seats[i].Type == SeatType.FIRST_CLASS)
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
                seat.Cursor = Cursors.Hand;

                seat.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(click));
                Grid.SetColumn(seat, i / 4);
                Grid.SetRow(seat, 3 - i % 4);
                page2.SeatsMap.Children.Add(seat);
            }
        }

        public void click(Object sender, EventArgs e)
        {
            Button selectedButton = sender as Button;
            string seatNumber = selectedButton.Content.ToString();
            UpdateSeat(Convert.ToInt32(seatNumber));
        }

        public void UpdateSeat(int seatNumber)
        {
            List<Seat> wagon = wagons[SelectedWagon];
            if (wagon[seatNumber-1].Type == SeatType.FIRST_CLASS)
            {
                wagon[seatNumber-1].Type = SeatType.SECOND_CLASS;
            }
            else
            {
                wagon[seatNumber-1].Type = SeatType.FIRST_CLASS;
            }
                page2.SeatsMap.Children.Clear(); // da ne iscrtava mapu sedista jednu preko druge
            DrawSeats();
        }

    }
}
