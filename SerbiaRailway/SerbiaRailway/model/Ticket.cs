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
        public TicketState State { get; set; }
        public PartialLine PartialLine { get; set; }
        public DateTime Date { get; set; }
        public Seat Seat { get; set; }
        public int Wagon { get; set; }

        public Ticket() { }

        public Ticket(Client client, DateTime date, double price, PartialLine line, TicketState state, Seat seat, int wagon)
        {
            Client = client;
            Price = price;
            Date = date;
            PartialLine = line;
            State = state;
            Seat = seat;
            Wagon = wagon;
        }
    }
}
