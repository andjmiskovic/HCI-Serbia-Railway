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
        public int ExtraPrice { get; set; }
        public int SeatsNumber { get; set; }

        public Dictionary<int, List<Seat>> wagons = new Dictionary<int, List<Seat>>();
        public int SelectedWagon { get; set; }
        public AddTrainWindow()
        {
            InitializeComponent();
        }

        /*private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (Manufacturer_TextBox.Text == "Insert train manufacturer name" || Manufacturer_TextBox.Text == "")
            {
                MessageBox.Show("Please fill manufacturer name.");
            }
            else
            {
                Manufacturer = Manufacturer_TextBox.Text;
                ComboBoxItem typeItem = (ComboBoxItem)Wagon_ComboBox.SelectedItem;
                WagonsNumber = Convert.ToInt32(typeItem.Content.ToString());
                if (Price_TextBox.Text == "Price" || Price_TextBox.Text == "")
                {
                    MessageBox.Show("Please fill extra price.");
                }
                else if (int.TryParse(Price_TextBox.Text, out _) == false)
                {
                    MessageBox.Show("Seats input must be number!");
                }
                else
                {
                    ExtraPrice = Convert.ToInt32(Price_TextBox.Text);
                   // SeatsNumber = Convert.ToInt32(Seat_TextBox.Value);
                }
            }
        }*/

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            int wagonsNumber = Convert.ToInt32(WagonsNumber.SelectedValue.ToString());
            List<Wagon> wagonsList = new List<Wagon>();
            for (int i = 1; i < wagonsNumber+1; i++)
            {
                Wagon wagon = new Wagon(i, wagons[i]);
                wagonsList.Add(wagon);
            }
            
            Train train = new Train(services.Data.Instance.Trains.Last().Id+1, Manufacturer.Text, wagonsList, true);
            services.DataService.Data.Trains.Add(train);
            MessageBox.Show("Train added successfully!");
            this.Close();
        }

        private void FillWagonsComboBox(object sender, SelectionChangedEventArgs e)
        {
            int wagonsNumber = Convert.ToInt32(WagonsNumber.SelectedValue.ToString());
            Wagon_ComboBox.Items.Clear();
            for (int i = 1; i <= wagonsNumber; i++)
            {
                Wagon_ComboBox.Items.Add(i);
            }
        }

        public void FillWagons()
        {
            int wagonsNumber = Convert.ToInt32(WagonsNumber.SelectedValue.ToString());
            for (int i = 1; i <= wagonsNumber; i++)
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
            SelectedWagon = (int)Wagon_ComboBox.SelectedItem;
            SeatsMap.Children.Clear();
            Draw(wagons[SelectedWagon]);
        }

        private void Draw(List<Seat> seats)
        {
            int numberOfCols = seats.Count / 4 + 1;
            SeatsMap.ColumnDefinitions.Clear();
            for (int i = 0; i < numberOfCols; i++)
                SeatsMap.ColumnDefinitions.Add(new ColumnDefinition());
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
                SeatsMap.Children.Add(seat);
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
            SeatsMap.Children.Clear(); // da ne iscrtava mapu sedista jednu preko druge
            Draw(wagons[SelectedWagon]);
        }

        private void CreateSeats(object sender, RoutedEventArgs e)
        {
            if (Manufacturer.Text == "")
            {
                MessageBox.Show("You have to enter train name.");
                return;
            }
            if (!int.TryParse(Price.Text, out _))
            {
                MessageBox.Show("Price has to be a number.");
                return;
            }
            if (int.Parse(Price.Text) < 50)
            {
                MessageBox.Show("Price has to be bigger than 50.");
                return;
            }
            if (!int.TryParse(SeatsPerWagon.Text, out _))
            {
                MessageBox.Show("Seats number has to be a number.");
                return;
            }
            if (int.Parse(SeatsPerWagon.Text) > 40 || int.Parse(SeatsPerWagon.Text) < 10)
            {
                MessageBox.Show("Seats number has to be in range 10 to 40.");
                return;
            }
            wagons = new Dictionary<int, List<Seat>>();
            SeatsNumber = int.Parse(SeatsPerWagon.Text);
            WagonToEdit.Visibility = Visibility.Visible;
            ExtraPrice = int.Parse(Price.Text);
            FillWagons();
            Wagon_ComboBox.SelectedIndex = 0;
        }
    }
}
