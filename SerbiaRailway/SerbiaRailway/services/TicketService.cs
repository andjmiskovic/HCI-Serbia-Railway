using SerbiaRailway.model;
using System;

namespace SerbiaRailway.services
{
    class TicketService
    {
        public static void BuyTicket(Client client, Seat seat, PartialLine partial, int wagon, DateTime date)
        {
            Ticket ticket = new Ticket
            {
                Seat = seat,
                PartialLine = partial,
                State = TicketState.BOUGHT,
                Client = client,
                Wagon = wagon,
                Date = date,
                Price = CalculateTicketPrice(partial) + seat.ExtraPrice
            };
            LoginService.AddTicket(ticket);
        }

        public static void ReserveTicket(Client client, Seat seat, PartialLine partial, int wagon, DateTime date)
        {
            Ticket ticket = new Ticket
            {
                Seat = seat,
                State = TicketState.RESERVED,
                Client = client,
                PartialLine = partial,
                Wagon = wagon,
                Date = date,
                Price = CalculateTicketPrice(partial) + seat.ExtraPrice
            };
            LoginService.AddTicket(ticket);
        }

        public static double CalculateTicketPrice(PartialLine partial)
        {
            double price = 0;
            bool riding = false;
            foreach(StationSchedule station in partial.Line.StationSchedule)
            {
                if(station.StartingStation.Name.Equals(partial.Start.Name))
                {
                    riding = true;
                }
                if(riding)
                {
                    price += station.Price;
                }
                if (station.StartingStation.Name.Equals(partial.End.Name))
                {
                    riding = false;
                    break;
                }
            }
            return price;
        }
    }
}
