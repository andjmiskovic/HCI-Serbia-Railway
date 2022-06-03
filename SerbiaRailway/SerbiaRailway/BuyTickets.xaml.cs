using System.Windows;

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
        }

    }
}
