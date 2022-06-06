﻿using SerbiaRailway.model;
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
            bool zeroTickets = true;
            foreach (Ticket ticket in DataService.Data.Tickets)
            {
                if (correctMonth(ticket.Date, month, year))
                {
                    Tickets.Children.Add(new TicketCardManager(ticket));
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
            foreach (Ticket ticket in DataService.Data.Tickets)
            {
                Console.WriteLine(ticket.PartialLine.Line.Id);
                if (checkRide(ticket, lineId, date))
                {
                    Tickets.Children.Add(new TicketCardManager(ticket));
                    zeroTickets = false;
                }
            }
            if(zeroTickets)
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
            Tickets.Children.Clear();
            if ((bool)MonthlyTickets.IsChecked)
            {
                int year = int.Parse(Year.SelectedValue.ToString());
                int month = Month.SelectedIndex + 1;
                FillMonthlyTickets(month, year);
            }
            else
            {
                DateTime date = Date.SelectedDate.Value;
                int lineId = int.Parse(Line.SelectedItem.ToString().Split('(')[0]);
                FillRideTickets(lineId, date);
            }
        }
    }
}
