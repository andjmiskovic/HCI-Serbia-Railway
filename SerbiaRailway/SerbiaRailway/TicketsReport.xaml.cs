using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TicketsReport.xaml
    /// </summary>
    public partial class TicketsReport : Page
    {
        private StackPanel monthlyPanel = new StackPanel();
        private StackPanel ridePanel = new StackPanel();
        public TicketsReport()
        {
            InitializeComponent();
            FillLines();
        }

        private void FillMonthlyTickets(int month, int year)
        {
            bool zeroTickets = true;
            monthlyPanel.Children.Clear();
            foreach (Ticket ticket in DataService.Data.Tickets)
            {
                if (correctMonth(ticket.Date, month, year))
                {
                    Tickets.Children.Add(new TicketCardManager(ticket));
                    monthlyPanel.Children.Add(new TicketCardManager(ticket));
                    zeroTickets = false;
                }
            }
            if (zeroTickets)
            {
                Label label = new Label();
                label.FontSize = 20;
                label.Content = "No tickets for this month.";
                Tickets.Children.Add(label);
            }
        }

        private bool checkRide(Ticket ticket, int lineId, DateTime date)
        {
            return ticket.PartialLine.Line.Id == lineId && date == ticket.Date;
        }

        private void FillRideTickets(int lineId, DateTime date)
        {
            bool zeroTickets = true;
            ridePanel.Children.Clear();
            foreach (Ticket ticket in DataService.Data.Tickets)
            {
                if (checkRide(ticket, lineId, date))
                {
                    Tickets.Children.Add(new TicketCardManager(ticket));
                    ridePanel.Children.Add(new TicketCardManager(ticket));
                    zeroTickets = false;
                }
            }
            if (zeroTickets)
            {
                Label label = new Label();
                label.FontSize = 20;
                label.Content = "No tickets for this ride.";
                Tickets.Children.Add(label);
            }
        }

        private bool correctMonth(DateTime date, int month, int year)
        {
            return date.Year == year && date.Month == month;
        }

        private void FillLines()
        {
            Line.Items.Clear();
            foreach(Line line in DataService.Data.Lines) 
            {
                Line.Items.Add(line.Id + " (" + line.Route.Name + ")");
            }
        }

        private void Monthly_Checked(object sender, RoutedEventArgs e)
        {
            if (Tickets != null)
            {
                Tickets.Children.Clear();
                foreach (TicketCardManager ticketManager in monthlyPanel.Children)
                {
                    Tickets.Children.Add(new TicketCardManager(ticketManager.ticket));
                }
            }
            if (Year != null)
            {
                Year.Visibility = Visibility.Visible;
                Month.Visibility = Visibility.Visible;
                Line.Visibility = Visibility.Hidden;
                Date.Visibility = Visibility.Hidden;
            }
        }

        private void Ride_Checked(object sender, RoutedEventArgs e)
        {
            if (Tickets != null)
            {
                Tickets.Children.Clear();
                foreach (TicketCardManager ticketManager in ridePanel.Children)
                {
                    Tickets.Children.Add(new TicketCardManager(ticketManager.ticket));
                }
            }
            if (Year != null)
            {
                Year.Visibility = Visibility.Hidden;
                Month.Visibility = Visibility.Hidden;
                Line.Visibility = Visibility.Visible;
                Date.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadTickets();
        }

        private void LoadTickets()
        {
            Tickets.Children.Clear();
            if ((bool)MonthlyTickets.IsChecked)
            {
                try
                {
                    int year = int.Parse(Year.SelectedValue.ToString());
                    int month = Month.SelectedIndex + 1;
                    FillMonthlyTickets(month, year);
                }
                catch
                {
                    MessageBox.Show("Please choose month and year.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    DateTime date = Date.SelectedDate.Value;
                    int lineId = int.Parse(Line.SelectedItem.ToString().Split('(')[0]);
                    FillRideTickets(lineId, date);
                }
                catch
                {
                    MessageBox.Show("Please choose line and date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
                HelpProvider.SetHelpKey((DependencyObject)sender, "ticketsReport");
                HelpProvider.ShowHelp(HelpProvider.GetHelpKey((DependencyObject)sender), this);
            }
            if(e.Key == Key.Enter)
            {
                LoadTickets();
            }
            if (e.Key == Key.D && Keyboard.Modifiers == ModifierKeys.Control)
            {
                PlayDemo();
            }
        }

        private void demoBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayDemo();
        }

        private void PlayDemo()
        {

        }
    }
}
