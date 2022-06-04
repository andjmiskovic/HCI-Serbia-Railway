namespace SerbiaRailway.model
{
    public enum SeatType
    {
        FIRST_CLASS,
        SECOND_CLASS
    }

    public class Seat
    {
        public int SeatNumber { get; set; }
        public SeatType Type { get; set; }
        public double ExtraPrice { get; set; }

        public Seat()
        {
        }

        public Seat(int seatNumber, SeatType type, double price)
        {
            SeatNumber = seatNumber;
            Type = type;
            ExtraPrice = price;
        }
    }
}
