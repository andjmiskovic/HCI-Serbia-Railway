using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for BuyTickets.xaml
    /// </summary>
    public partial class BuyTickets : Window
    {
        private PartialLine line { get; set; }
        public BuyTickets()
        {
            InitializeComponent();
        }

        public BuyTickets(PartialLine line)
        {
            this.line = line;
            InitializeComponent();
            Console.WriteLine(line.Line.Train);
            Console.WriteLine(line.Line.Train.Wagons);
            AddWagons(line.Line.Train.Wagons.Count());
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
                WagonStack.Children.Add(wagon);
            }
        }

    }
}
