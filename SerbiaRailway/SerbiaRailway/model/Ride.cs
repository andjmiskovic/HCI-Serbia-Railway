using System;
using System.Collections.Generic;

namespace SerbiaRailway.model
{
    public class Ride
    {
        // gleda se samo datum
        public DateTime Date { get; set; }
        public Line Line { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Ride(DateTime date, Line line)
        {
            Line = line;
            Date = date;
            Tickets = new List<Ticket>();
        }

        // za voznju vraca slobodna sedista od 1 do 2. deonice 
        public Dictionary<int, List<TakenSeat>> GetAvailableSeats(Station start, Station end)
        {
            Dictionary<int, List<TakenSeat>> seats = new Dictionary<int, List<TakenSeat>>();
            foreach(Wagon wagon in Line.Route.Train.Wagons)
            {
                List<TakenSeat> takenSeats = new List<TakenSeat>();
                foreach (Seat seat in wagon.Seats)
                {
                    takenSeats.Add(new TakenSeat(seat, true));
                }
                seats.Add(wagon.Number, takenSeats);
            }
            foreach(Ticket ticket in Tickets)
            {
                seats[ticket.Wagon][ticket.Seat.SeatNumber - 1].isAvailable = IsSeatAvailable(start, end, ticket);
            }
            return seats;
        }

        // da li je sediste sa karte zauzeto na toj deonici?
        public bool IsSeatAvailable(Station start, Station end, Ticket ticket)
        {
            bool available = false;
            foreach(StationSchedule stationSchedule in Line.StationSchedules)
            {
                // na pocetku putovanja gledamo da je slobodno sediste, posle nas boli uvo
                if(start.Name.Equals(stationSchedule.StartingStation.Name))
                {
                    available = true;
                }
                if(available && ticket.PartialLine.Start.Name.Equals(stationSchedule.StartingStation.Name))
                {
                    return false;
                }
            }
            return true;
        }
    }

}
