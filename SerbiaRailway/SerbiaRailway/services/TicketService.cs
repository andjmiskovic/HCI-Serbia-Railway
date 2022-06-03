using SerbiaRailway.model;

namespace SerbiaRailway.services
{
    class TicketService
    {
        public static void BuyTicket(Client client, Seat seat, PartialLine partial, int wagon)
        {
            Ticket ticket = new Ticket
            {
                Seat = seat,
                PartialLine = partial,
                State = TicketState.BOUGHT,
                Client = client,
                Wagon = wagon,
                Price = CalculateTicketPrice(partial, seat)
            };
            client.Bought.Add(ticket);
        }

        public static void ReserveTicket(Client client, Seat seat, PartialLine partial, int wagon)
        {
            Ticket ticket = new Ticket
            {
                Seat = seat,
                State = TicketState.RESERVED,
                Client = client,
                PartialLine = partial,
                Wagon = wagon,
                Price = CalculateTicketPrice(partial, seat)
            };
            client.Reserved.Add(ticket);
        }

        private static double CalculateTicketPrice(PartialLine partial, Seat seat)
        {
            double price = 0;
            price += seat.ExtraPrice;
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
