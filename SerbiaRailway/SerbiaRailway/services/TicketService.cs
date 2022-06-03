using SerbiaRailway.model;

namespace SerbiaRailway.services
{
    class TicketService
    {
        public void BuyTicket(Client client, double price, Seat seat, Ride ride, Station startStation, Station endStation, int wagon)
        {
            Ticket ticket = new Ticket
            {
                Seat = seat,
                Ride = ride,
                State = TicketState.BOUGHT,
                Client = client,
                EndStation = endStation,
                StartStation = startStation,
                Wagon = wagon,
                Price = CalculateTicketPrice(ride, startStation, endStation, seat)
            };
            client.Bought.Add(ticket);
        }

        public void ReserveTicket(Client client, double price, Seat seat, Ride ride, Station startStation, Station endStation, int wagon)
        {
            Ticket ticket = new Ticket
            {
                Seat = seat,
                Ride = ride,
                State = TicketState.RESERVED,
                Client = client,
                EndStation = endStation,
                StartStation = startStation,
                Wagon = wagon,
                Price = CalculateTicketPrice(ride, startStation, endStation, seat)
            };
            client.Reserved.Add(ticket);
        }

        private double CalculateTicketPrice(Ride ride, Station startStation, Station endStation, Seat seat)
        {
            double price = 0;
            price += seat.ExtraPrice;
            bool riding = false;
            foreach(StationSchedule station in ride.Line.StationSchedule)
            {
                if(station.StartingStation == startStation)
                {
                    riding = true;
                }
                if(riding)
                {
                    price += station.Price;
                }
                if (station.StartingStation == endStation)
                {
                    riding = false;
                    break;
                }
            }
            return price;
        }
    }
}
