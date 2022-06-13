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
using System.Windows.Shapes;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for EditTrain.xaml
    /// </summary>
    public partial class EditTrain : Window
    {
        public int ExtraPrice { get; set; }
        public Dictionary<int, List<Seat>> wagons = new Dictionary<int, List<Seat>>();
        public List<TrainXamlData> TrainXamlDatas { get; set; }
        public TrainXamlData SelectedTrainXamlData { get; set; }
        public int SeatsNumber { get; set; }
        public Train Train { get; set; }
        public int SelectedWagon { get; set; }
        public EditTrain()
        {
            InitializeComponent();
        }

        public EditTrain(List<TrainXamlData> trainXamlDatas, TrainXamlData trainXamlData, Train train)
        {
            InitializeComponent();
            TrainXamlDatas = trainXamlDatas;
            SelectedTrainXamlData = trainXamlData;
            Train = train;
            SeatsNumber = SelectedTrainXamlData.NumberOfSeats / SelectedTrainXamlData.NumberOfWagons;
            Manufacturer.Text = train.Manufacturer;
            WagonsNumber.Text = SelectedTrainXamlData.NumberOfWagons.ToString();
            Price.Text = train.Wagons[0].Seats[0].ExtraPrice.ToString();
            SeatsPerWagon.Text = train.Wagons[0].Seats.Count.ToString();
            WagonsNumber.IsEnabled = false;
            SeatsPerWagon.IsEnabled = false;
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
            if (wagon[seatNumber - 1].Type == SeatType.FIRST_CLASS)
            {
                wagon[seatNumber - 1].Type = SeatType.SECOND_CLASS;
            }
            else
            {
                wagon[seatNumber - 1].Type = SeatType.FIRST_CLASS;
            }
            SeatsMap.Children.Clear(); // da ne iscrtava mapu sedista jednu preko druge
            Draw(wagons[SelectedWagon]);
        }

        private void CreateSeats(object sender, RoutedEventArgs e)
        {
            if (Manufacturer.Text == "")
            {
                MessageBox.Show("You have to enter train manufacturer.", "Error!", MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(Price.Text, out _))
            {
                MessageBox.Show("Price has to be a number.", "Error!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            if (int.Parse(Price.Text) < 50)
            {
                MessageBox.Show("Price has to be bigger than 50.", "Error!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(SeatsPerWagon.Text, out _))
            {
                MessageBox.Show("Seats number has to be a number.", "Error!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            if (int.Parse(SeatsPerWagon.Text) > 40 || int.Parse(SeatsPerWagon.Text) < 10)
            {
                MessageBox.Show("Seats number has to be in range 10 to 40.", "Error!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            wagons = new Dictionary<int, List<Seat>>();
            SeatsNumber = int.Parse(SeatsPerWagon.Text);
            WagonToEdit.Visibility = Visibility.Visible;
            ExtraPrice = int.Parse(Price.Text);
            FillWagons();
            Wagon_ComboBox.SelectedIndex = 0;
        }

        private void FillWagons()
        {
            int wagonsNumber = SelectedTrainXamlData.NumberOfWagons;
            Wagon_ComboBox.Items.Clear();
            for (int i = 1; i <= wagonsNumber; i++)
            {
                Wagon_ComboBox.Items.Add(i);
            }
            for (int i = 1; i <= Train.Wagons.Count; i++)
            {
                wagons.Add(i, Train.Wagons[i - 1].Seats);
            }
        }

        private void editTrain_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult decision = MessageBox.Show("Are you sure you want to edit this train?",
                    "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (decision == MessageBoxResult.Yes)
            {
                List<Wagon> wagonsList = new List<Wagon>();
                for (int i = 1; i < SelectedTrainXamlData.NumberOfWagons + 1; i++)
                {
                    Wagon wagon = new Wagon(i, wagons[i]);
                    foreach (Seat seat in wagon.Seats)
                    {
                        seat.ExtraPrice = Convert.ToInt32(Price.Text);
                    }
                    wagonsList.Add(wagon);
                }
                Train.Manufacturer = Manufacturer.Text;
                Train.Wagons = wagonsList;
                SelectedTrainXamlData.Manufacturer = Manufacturer.Text;
                MessageBox.Show("Train edited successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
