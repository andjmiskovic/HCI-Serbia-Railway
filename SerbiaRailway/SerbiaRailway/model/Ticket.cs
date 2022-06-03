using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public enum TicketState { BOUGHT, RESERVED }

    public class Ticket
    {
        public Client Client { get; set; }
        public double Price { get; set; }
        public Ride Ride { get; set; }
        public TicketState State { get; set; }
        public Station StartStation { get; set; }
        public Station EndStation { get; set; }
        public Seat Seat { get; set; }
        public int Wagon { get; set; }

        public Ticket() { }

        public Ticket(Client client, double price, Ride ride, TicketState state, Station startStation, Station endStation, Seat seat, int wagon)
        {
            Client = client;
            Price = price;
            Ride = ride;
            State = state;
            StartStation = startStation;
            EndStation = endStation;
            Seat = seat;
            Wagon = wagon;
        }
    }
}
