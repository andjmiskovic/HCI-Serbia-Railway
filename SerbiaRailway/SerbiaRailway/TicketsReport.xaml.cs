using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TicketsReport.xaml
    /// </summary>
    public partial class TicketsReport : Page
    {
        public TicketsReport()
        {
            InitializeComponent();
            FillLines();
        }

        private void FillMonthlyTickets(int month, int year)
        {
            List<Ticket> tickets;
            foreach (Ticket ticket in DataService.Data.Tickets)
            {
                if(correctMonth(ticket.Date, month, year))
                    Tickets.Children.Add(new TicketCardManager(ticket));
            }
        }

        private bool checkRide(Ticket ticket, int lineId, DateTime date)
        {
            return ticket.PartialLine.Line.Id == lineId && date == ticket.Date;
        }

        private void FillRideTickets(int lineId, DateTime date)
        {
            List<Ticket> tickets;
            foreach (Ticket ticket in DataService.Data.Tickets)
            {
                if (checkRide(ticket, lineId, date))
                    Tickets.Children.Add(new TicketCardManager(ticket));
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
                Line.Items.Add(line.Id + " (" + line.Name + ")");
            }
        }

        private void Monthly_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Year != null)
            {
                Year.Visibility = System.Windows.Visibility.Visible;
                Month.Visibility = System.Windows.Visibility.Visible;
                Line.Visibility = System.Windows.Visibility.Hidden;
                Date.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Ride_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Year != null)
            {
                Year.Visibility = System.Windows.Visibility.Hidden;
                Month.Visibility = System.Windows.Visibility.Hidden;
                Line.Visibility = System.Windows.Visibility.Visible;
                Date.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DateTime date = Date.SelectedDate.Value;
            int year = int.Parse(Year.SelectedItem.ToString());
            int month = Month.SelectedIndex + 1;
            int lineId = int.Parse(Line.SelectedItem.ToString().Split('(')[0]);

            Tickets.Children.RemoveRange(0, Tickets.Children.Count);
            if ((bool)MonthlyTickets.IsChecked)
                FillMonthlyTickets(month, year);
            else
                FillRideTickets(lineId, date);
        }
    }
}
