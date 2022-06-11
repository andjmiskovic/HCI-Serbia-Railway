using Microsoft.Maps.MapControl.WPF;
using SerbiaRailway.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerbiaRailway.services
{
    public sealed class Data
    {
        private static readonly Data instance = new Data();
        public List<Manager> Managers { get; set; }
        public List<Client> Clients { get; set; }
        public List<Station> Stations { get; set; }
        public List<Train> Trains { get; set; }
        private List<StationSchedule> _stationSchedules;
        public List<Route> Routes { get; set; }
        public List<Line> Lines { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Ride> Rides { get; set; }
        static Data()
        {

        }

        public List<string> GetStationNames()
        {
            List<string> stations = new List<string>();
            foreach (Station s in Stations)
                stations.Add(s.Name);
            return stations;
        }

        internal Line GetLineById(int v)
        {
            foreach(Line line in Lines)
            {
                if (line.Id == v)
                    return line;
            }
            return null;
        }

        public Station GetStationByName(string name)
        {
            foreach (Station s in Stations)
                if (s.Name.Equals(name))
                    return s;
            return null;
        }

        private void AddUsers()
        {
            Managers.Add(new Manager("ana", "123", "Ana", "Avramovski"));
            Clients.Add(new Client("pera", "123", "Rastko", "Sakal"));
            Clients.Add(new Client("zdera", "123", "Miloje", "Milic"));
            Clients.Add(new Client("lera", "123", "Luka", "Banac"));
            Clients.Add(new Client("tera", "123", "Tomislav", "Stan"));
        }

        private void AddStations()
        {
            Stations.Add(new Station(1, "Belgrade center", new Location(44.79804986986281, 20.469038993624373)));
            Stations.Add(new Station(2, "New Belgrade", new Location(44.807148878217795, 20.418676428361984)));
            Stations.Add(new Station(3, "Novi Sad - railway station", new Location(45.26574551158948, 19.82776423525647)));
            Stations.Add(new Station(4, "Subotica center", new Location(46.097736208068675, 19.66535268470493)));
            Stations.Add(new Station(5, "Sremska Mitrovica - railway station", new Location(44.982591767221535, 19.61372849905475)));
            Stations.Add(new Station(6, "Sombor center", new Location(45.77289565685169, 19.114974219610215)));
            Stations.Add(new Station(7, "Kraljevo center", new Location(43.72394895953815, 20.68597718576761)));
            Stations.Add(new Station(8, "Kragujevac center", new Location(44.01306948716552, 20.91634370652272)));
            Stations.Add(new Station(9, "Vranje center", new Location(42.552404405533665, 21.900640818808117)));
            Stations.Add(new Station(10, "Kosovska Mitrovica center", new Location(42.89615490730306, 20.868055000038066)));
        }

        private void AddTrains() 
        {
            List<Seat> seats1 = new List<Seat>
            {
                new Seat(1, SeatType.FIRST_CLASS, 250),
                new Seat(2, SeatType.FIRST_CLASS, 250),
                new Seat(3, SeatType.FIRST_CLASS, 250),
                new Seat(4, SeatType.FIRST_CLASS, 250),
                new Seat(5, SeatType.FIRST_CLASS, 250),
                new Seat(6, SeatType.FIRST_CLASS, 250),
                new Seat(7, SeatType.FIRST_CLASS, 250),
                new Seat(8, SeatType.FIRST_CLASS, 250),
                new Seat(9, SeatType.FIRST_CLASS, 250),
                new Seat(10, SeatType.FIRST_CLASS, 250),
                new Seat(11, SeatType.SECOND_CLASS, 0),
                new Seat(12, SeatType.SECOND_CLASS, 0),
                new Seat(13, SeatType.SECOND_CLASS, 0),
                new Seat(14, SeatType.SECOND_CLASS, 0),
                new Seat(15, SeatType.SECOND_CLASS, 0),
                new Seat(16, SeatType.SECOND_CLASS, 0),
                new Seat(17, SeatType.SECOND_CLASS, 0),
                new Seat(18, SeatType.SECOND_CLASS, 0),
                new Seat(19, SeatType.SECOND_CLASS, 0),
                new Seat(20, SeatType.SECOND_CLASS, 0)
            };
            List<Seat> seats2 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Seat> seats3 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Seat> seats4 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Seat> seats5 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Seat> seats6 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Seat> seats7 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Seat> seats8 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Seat> seats9 = seats1.ConvertAll(seat => new Seat(seat.SeatNumber, seat.Type, seat.ExtraPrice));
            List<Wagon> wagons1 = new List<Wagon>
            {
                new Wagon(1, seats1),
                new Wagon(2, seats2),
                new Wagon(3, seats3),
                new Wagon(4, seats4),
            };
            List<Wagon> wagons2 = new List<Wagon>
            {
                new Wagon(1, seats5),
                new Wagon(2, seats6),
            };
            List<Wagon> wagons3 = new List<Wagon>
            {
                new Wagon(1, seats7),
                new Wagon(2, seats8),
                new Wagon(3, seats9),
            };
            Trains.Add(new Train(1, "Speedy Gonzales", wagons1, true));
            Trains.Add(new Train(2, "SirmiumTrains", wagons2, true));
            Trains.Add(new Train(3, "Serbia trains", wagons3, true));
        }

        private void AddStationSchedules()
        {
            _stationSchedules = new List<StationSchedule>
            {
                new StationSchedule(Stations[0], Stations[1], new TimeSpan(10, 15, 0),
                new TimeSpan(10, 20, 0), 100),
                new StationSchedule(Stations[1], Stations[2], new TimeSpan(10, 25, 0),
                new TimeSpan(10, 55, 0), 700),
                new StationSchedule(Stations[2], Stations[1], new TimeSpan(11, 5, 0),
                new TimeSpan(11, 25, 0), 700),
                new StationSchedule(Stations[1], Stations[0], new TimeSpan(11, 30, 0),
                new TimeSpan(11, 35, 0), 100),
                new StationSchedule(Stations[0], Stations[1], new TimeSpan(11, 0, 0),
                new TimeSpan(11, 5, 0), 100),
                new StationSchedule(Stations[1], Stations[2], new TimeSpan(11, 10, 0),
                new TimeSpan(11, 40, 0), 700),
                new StationSchedule(Stations[2], Stations[3], new TimeSpan(11, 45, 0),
                new TimeSpan(12, 25, 0), 500),
                new StationSchedule(Stations[3], Stations[2], new TimeSpan(12, 35, 0),
                new TimeSpan(13, 15, 0), 500),
                new StationSchedule(Stations[2], Stations[1], new TimeSpan(13, 20, 0),
                new TimeSpan(13, 50, 0), 700),
                new StationSchedule(Stations[1], Stations[0], new TimeSpan(13, 55, 0),
                new TimeSpan(14, 0, 0), 100),
                new StationSchedule(Stations[2], Stations[3], new TimeSpan(8, 45, 0),
                new TimeSpan(9, 25, 0), 500),
                new StationSchedule(Stations[3], Stations[2], new TimeSpan(9, 40, 0),
                new TimeSpan(10, 20, 0), 500),
                new StationSchedule(Stations[2], Stations[5], new TimeSpan(12, 0, 0),
                new TimeSpan(12, 30, 0), 350),
                new StationSchedule(Stations[5], Stations[2], new TimeSpan(12, 50, 0),
                new TimeSpan(13, 20, 0), 100),
                new StationSchedule(Stations[3], Stations[5], new TimeSpan(7, 30, 0),
                new TimeSpan(7, 40, 0), 100),
                new StationSchedule(Stations[5], Stations[3], new TimeSpan(7, 50, 0),
                new TimeSpan(8, 0, 0), 100),
                new StationSchedule(Stations[0], Stations[1], new TimeSpan(11, 0, 0),
                new TimeSpan(11, 5, 0), 100),
                new StationSchedule(Stations[1], Stations[4], new TimeSpan(11, 10, 0),
                new TimeSpan(11, 25, 0), 400),
                new StationSchedule(Stations[4], Stations[1], new TimeSpan(11, 30, 0),
                new TimeSpan(11, 45, 0), 400),
                new StationSchedule(Stations[1], Stations[0], new TimeSpan(11, 50, 0),
                new TimeSpan(11, 55, 0), 100),
                new StationSchedule(Stations[2], Stations[4], new TimeSpan(15, 25, 0),
                new TimeSpan(15, 50, 0), 350),
                new StationSchedule(Stations[4], Stations[2], new TimeSpan(16, 0, 0),
                new TimeSpan(16, 25, 0), 350),
                new StationSchedule(Stations[0], Stations[7], new TimeSpan(13, 35, 0),
                new TimeSpan(14, 25, 0), 650),
                new StationSchedule(Stations[7], Stations[0], new TimeSpan(14, 35, 0),
                new TimeSpan(15, 25, 0), 650),
                new StationSchedule(Stations[7], Stations[6], new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0), 400),
                new StationSchedule(Stations[6], Stations[7], new TimeSpan(15, 40, 0),
                new TimeSpan(16, 10, 0), 400),
                new StationSchedule(Stations[6], Stations[8], new TimeSpan(16, 0, 0),
                new TimeSpan(17, 55, 0), 700),
                new StationSchedule(Stations[8], Stations[6], new TimeSpan(18, 5, 0),
                new TimeSpan(20, 0, 0), 700),
                new StationSchedule(Stations[6], Stations[9], new TimeSpan(15, 50, 0),
                new TimeSpan(18, 30, 0), 500),
                new StationSchedule(Stations[9], Stations[6], new TimeSpan(18, 40, 0),
                new TimeSpan(21, 20, 0), 500)
            };
        }

        private void AddRoutes()
        {
            List<Station> stations1 = new List<Station>
            {
                Stations[0],
                Stations[1],
                Stations[2]
            };
            Routes.Add(new Route(1, "Belgrade-Novi Sad", stations1, Trains[1]));

            List<Station> stations2 = new List<Station>
            {
                Stations[2],
                Stations[1],
                Stations[0]
            };
            Routes.Add(new Route(2, "Novi Sad-Belgrade", stations2, Trains[1]));

            List<Station> stations3 = new List<Station>
            {
                Stations[0],
                Stations[1],
                Stations[2],
                Stations[3],
            };
            Routes.Add(new Route(3, "Belgrade-Subotica", stations3, Trains[1]));

            List<Station> stations4 = new List<Station>
            {
                Stations[3],
                Stations[2],
                Stations[1],
                Stations[0],
            };
            Routes.Add(new Route(4, "Subotica-Belgrade", stations4, Trains[1]));

            List<Station> stations5 = new List<Station>
            {
                Stations[2],
                Stations[3]
            };
            Routes.Add(new Route(5, "Novi Sad-Subotica", stations5, Trains[0]));

            List<Station> stations6 = new List<Station>
            {
                Stations[3],
                Stations[2]
            };
            Routes.Add(new Route(6, "Subotica-Novi Sad", stations6, Trains[0]));

            List<Station> stations7 = new List<Station>
            {
                Stations[2],
                Stations[5]
            };
            Routes.Add(new Route(7, "Novi Sad-Sombor", stations7, Trains[0]));

            List<Station> stations8 = new List<Station>
            {
                Stations[5],
                Stations[2]
            };
            Routes.Add(new Route(8, "Sombor-Novi Sad", stations8, Trains[0]));

            List<Station> stations9 = new List<Station>
            {
                Stations[3],
                Stations[5],
            };
            Routes.Add(new Route(9, "Subotica-Sombor", stations9, Trains[0]));

            List<Station> stations10 = new List<Station>
            {
                Stations[5],
                Stations[3],
            };
            Routes.Add(new Route(10, "Sombor-Subotica", stations10, Trains[0]));

            List<Station> stations11 = new List<Station>
            {
                Stations[0],
                Stations[1],
                Stations[4]
            };
            Routes.Add(new Route(11, "Belgrade-Sremska Mitrovica", stations11, Trains[2]));

            List<Station> stations12 = new List<Station>
            {
                Stations[4],
                Stations[1],
                Stations[0]
            };
            Routes.Add(new Route(12, "Sremska Mitrovica-Belgrade", stations12, Trains[2]));

            List<Station> stations13 = new List<Station>
            {
                Stations[2],
                Stations[4],
            };
            Routes.Add(new Route(13, "Novi Sad-Sremska Mitrovica", stations13, Trains[2]));

            List<Station> stations14 = new List<Station>
            {
                Stations[4],
                Stations[2],
            };
            Routes.Add(new Route(14, "Sremska Mitrovica-Novi Sad", stations14, Trains[2]));

            List<Station> stations15 = new List<Station>
            {
                Stations[0],
                Stations[7],
            };
            Routes.Add(new Route(15, "Belgrade-Kragujevac", stations15, Trains[1]));

            List<Station> stations16 = new List<Station>
            {
                Stations[7],
                Stations[0],
            };
            Routes.Add(new Route(16, "Kragujevac-Belgrade", stations16, Trains[1]));

            List<Station> stations17 = new List<Station>
            {
                Stations[7],
                Stations[6],
            };
            Routes.Add(new Route(17, "Kragujevac-Kraljevo", stations17, Trains[1]));

            List<Station> stations18 = new List<Station>
            {
                Stations[6],
                Stations[7],
            };
            Routes.Add(new Route(18, "Kraljevo-Kragujevac", stations18, Trains[1]));

            List<Station> stations19 = new List<Station>
            {
                Stations[6],
                Stations[8],
            };
            Routes.Add(new Route(19, "Kraljevo-Vranje", stations19, Trains[1]));

            List<Station> stations20 = new List<Station>
            {
                Stations[8],
                Stations[6],
            };
            Routes.Add(new Route(20, "Vranje-Kraljevo", stations20, Trains[1]));

            List<Station> stations21 = new List<Station>
            {
                Stations[6],
                Stations[9],
            };
            Routes.Add(new Route(21, "Kraljevo-Kosovska Mitrovica", stations21, Trains[1]));

            List<Station> stations22 = new List<Station>
            {
                Stations[9],
                Stations[6],
            };
            Routes.Add(new Route(22, "Kosovska Mitrovica-Kraljevo", stations22, Trains[1]));
        }

        private void AddLines()
        {
            Dictionary<string, bool> weekDays1 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules1 = new List<StationSchedule>();
            for (int i = 0; i < 2; i++)
            {
                stationSchedules1.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(1, Routes[0], weekDays1, stationSchedules1));

            Dictionary<string, bool> weekDays2 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules2 = new List<StationSchedule>();
            for (int i = 2; i < 4; i++)
            {
                stationSchedules2.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(2, Routes[1], weekDays2, stationSchedules2));

            Dictionary<string, bool> weekDays3 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules3 = new List<StationSchedule>();
            for (int i = 4; i < 7; i++)
            {
                stationSchedules3.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(3, Routes[2], weekDays3, stationSchedules3));

            Dictionary<string, bool> weekDays4 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules4 = new List<StationSchedule>();
            for (int i = 7; i < 10; i++)
            {
                stationSchedules4.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(4, Routes[3], weekDays4, stationSchedules4));

            Dictionary<string, bool> weekDays5 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules5 = new List<StationSchedule>();
            for (int i = 10; i < 11; i++)
            {
                stationSchedules5.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(5, Routes[4], weekDays5, stationSchedules5));

            Dictionary<string, bool> weekDays6 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules6 = new List<StationSchedule>();
            for (int i = 11; i < 12; i++)
            {
                stationSchedules6.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(6, Routes[5], weekDays6, stationSchedules6));

            Dictionary<string, bool> weekDays7 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules7 = new List<StationSchedule>();
            for (int i = 12; i < 13; i++)
            {
                stationSchedules7.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(7, Routes[6], weekDays7, stationSchedules7));

            Dictionary<string, bool> weekDays8 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules8 = new List<StationSchedule>();
            for (int i = 13; i < 14; i++)
            {
                stationSchedules8.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(8, Routes[7], weekDays8, stationSchedules8));

            Dictionary<string, bool> weekDays9 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules9 = new List<StationSchedule>();
            for (int i = 14; i < 15; i++)
            {
                stationSchedules9.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(9, Routes[8], weekDays9, stationSchedules9));

            Dictionary<string, bool> weekDays10 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules10 = new List<StationSchedule>();
            for (int i = 15; i < 16; i++)
            {
                stationSchedules10.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(10, Routes[9], weekDays10, stationSchedules10));

            Dictionary<string, bool> weekDays11 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules11 = new List<StationSchedule>();
            for (int i = 16; i < 18; i++)
            {
                stationSchedules11.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(11, Routes[10], weekDays11, stationSchedules11));

            Dictionary<string, bool> weekDays12 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules12 = new List<StationSchedule>();
            for (int i = 18; i < 20; i++)
            {
                stationSchedules12.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(12, Routes[11], weekDays12, stationSchedules12));

            Dictionary<string, bool> weekDays13 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules13 = new List<StationSchedule>();
            for (int i = 20; i < 21; i++)
            {
                stationSchedules13.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(13, Routes[12], weekDays13, stationSchedules13));

            Dictionary<string, bool> weekDays14 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules14 = new List<StationSchedule>();
            for (int i = 21; i < 22; i++)
            {
                stationSchedules14.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(14, Routes[13], weekDays14, stationSchedules14));

            Dictionary<string, bool> weekDays15 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules15 = new List<StationSchedule>();
            for (int i = 22; i < 23; i++)
            {
                stationSchedules15.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(15, Routes[14], weekDays15, stationSchedules15));

            Dictionary<string, bool> weekDays16 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules16 = new List<StationSchedule>();
            for (int i = 23; i < 24; i++)
            {
                stationSchedules16.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(16, Routes[15], weekDays16, stationSchedules16));

            Dictionary<string, bool> weekDays17 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules17 = new List<StationSchedule>();
            for (int i = 24; i < 25; i++)
            {
                stationSchedules17.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(17, Routes[16], weekDays17, stationSchedules17));

            Dictionary<string, bool> weekDays18 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules18 = new List<StationSchedule>();
            for (int i = 25; i < 26; i++)
            {
                stationSchedules18.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(18, Routes[17], weekDays18, stationSchedules18));

            Dictionary<string, bool> weekDays19 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules19 = new List<StationSchedule>();
            for (int i = 26; i < 27; i++)
            {
                stationSchedules19.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(19, Routes[18], weekDays19, stationSchedules19));

            Dictionary<string, bool> weekDays20 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules20 = new List<StationSchedule>();
            for (int i = 27; i < 28; i++)
            {
                stationSchedules20.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(20, Routes[19], weekDays20, stationSchedules20));

            Dictionary<string, bool> weekDays21 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules21 = new List<StationSchedule>();
            for (int i = 28; i < 29; i++)
            {
                stationSchedules21.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(21, Routes[20], weekDays21, stationSchedules21));

            Dictionary<string, bool> weekDays22 = new Dictionary<string, bool>
            {
                { "Monday", true },
                { "Tuesday", true },
                { "Wednesday", true },
                { "Thursday", true },
                { "Friday", true },
                { "Saturday", true },
                { "Sunday", true }
            };
            List<StationSchedule> stationSchedules22 = new List<StationSchedule>();
            for (int i = 29; i < 30; i++)
            {
                stationSchedules22.Add(_stationSchedules[i]);
            }
            Lines.Add(new Line(22, Routes[21], weekDays22, stationSchedules22));
            
        }

        private void AddSchedules()
        {
            foreach(Line line in Lines)
            {
                DateTime dateTime = new DateTime(2022, 6, 4);
                for(int i = 0; i < 31; i++)
                {
                    Schedules.Add(new Schedule(line, dateTime));
                    dateTime = dateTime.AddDays(1);
                }
            }
        }

        private void AddRides()
        {
            Rides.Add(new Ride(new DateTime(2022, 6, 4), Lines[10]));
            List<Ticket> tickets1 = new List<Ticket>();
            PartialLine partialLine1 = new PartialLine(Stations[1], Stations[4], new TimeSpan(11, 10, 0),
                new TimeSpan(11, 25, 0), Lines[10]);
            Ticket ticket1 = new Ticket(Clients[0], new DateTime(2022, 6, 4), 400, partialLine1, TicketState.BOUGHT, 
                partialLine1.Line.Route.Train.Wagons[0].Seats[16], 1);
            Tickets.Add(ticket1);
            tickets1.Add(ticket1);
            Ticket ticket2 = new Ticket(Clients[1], new DateTime(2022, 6, 4), 400, partialLine1, TicketState.RESERVED,
                partialLine1.Line.Route.Train.Wagons[0].Seats[17], 1);
            Tickets.Add(ticket2);
            tickets1.Add(ticket2);
            Ticket ticket3 = new Ticket(Clients[2], new DateTime(2022, 6, 4), 400, partialLine1, TicketState.RESERVED,
                partialLine1.Line.Route.Train.Wagons[0].Seats[18], 1);
            Tickets.Add(ticket3);
            tickets1.Add(ticket3);
            Ticket ticket4 = new Ticket(Clients[3], new DateTime(2022, 6, 4), 400, partialLine1, TicketState.BOUGHT,
                partialLine1.Line.Route.Train.Wagons[0].Seats[19], 1);
            Tickets.Add(ticket4);
            tickets1.Add(ticket4);
            Rides[0].Tickets = tickets1;

            Rides.Add(new Ride(new DateTime(2022, 6, 4), Lines[4]));
            List<Ticket> tickets2 = new List<Ticket>();
            PartialLine partialLine2 = new PartialLine(Stations[2], Stations[3], new TimeSpan(8, 45, 0),
                new TimeSpan(9, 25, 0), Lines[4]);
            Ticket ticket5 = new Ticket(Clients[0], new DateTime(2022, 6, 4), 500, partialLine2, TicketState.RESERVED,
                partialLine1.Line.Route.Train.Wagons[0].Seats[16], 1);
            Tickets.Add(ticket5);
            tickets2.Add(ticket5);
            Ticket ticket6 = new Ticket(Clients[1], new DateTime(2022, 6, 4), 500, partialLine2, TicketState.BOUGHT,
                partialLine1.Line.Route.Train.Wagons[0].Seats[17], 1);
            Tickets.Add(ticket6);
            tickets2.Add(ticket6);
            Ticket ticket7 = new Ticket(Clients[2], new DateTime(2022, 6, 4), 500, partialLine2, TicketState.RESERVED,
                partialLine1.Line.Route.Train.Wagons[0].Seats[18], 1);
            Tickets.Add(ticket7);
            tickets2.Add(ticket7);
            Ticket ticket8 = new Ticket(Clients[3], new DateTime(2022, 6, 4), 500, partialLine2, TicketState.BOUGHT,
                partialLine1.Line.Route.Train.Wagons[0].Seats[19], 1);
            Tickets.Add(ticket8);
            tickets2.Add(ticket8);
            Rides[1].Tickets = tickets2;

            Rides.Add(new Ride(new DateTime(2022, 6, 4), Lines[16]));
            List<Ticket> tickets3 = new List<Ticket>();
            PartialLine partialLine3 = new PartialLine(Stations[7], Stations[6], new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0), Lines[16]);
            Ticket ticket9 = new Ticket(Clients[0], new DateTime(2022, 6, 4), 400, partialLine3, TicketState.RESERVED,
                partialLine1.Line.Route.Train.Wagons[0].Seats[16], 1);
            Tickets.Add(ticket9);
            tickets3.Add(ticket9);
            Ticket ticket10 = new Ticket(Clients[1], new DateTime(2022, 6, 4), 400, partialLine3, TicketState.BOUGHT,
                partialLine1.Line.Route.Train.Wagons[0].Seats[17], 1);
            Tickets.Add(ticket10);
            tickets3.Add(ticket10);
            Ticket ticket11 = new Ticket(Clients[2], new DateTime(2022, 6, 4), 400, partialLine3, TicketState.RESERVED,
                partialLine1.Line.Route.Train.Wagons[0].Seats[18], 1);
            Tickets.Add(ticket11);
            tickets3.Add(ticket11);
            Ticket ticket12 = new Ticket(Clients[3], new DateTime(2022, 6, 4), 400, partialLine3, TicketState.BOUGHT,
                partialLine1.Line.Route.Train.Wagons[0].Seats[19], 1);
            Tickets.Add(ticket12);
            tickets3.Add(ticket12);
            Rides[2].Tickets = tickets3;
        }

        private Data()
        {
            Clients = new List<Client>();
            Managers = new List<Manager>();
            Stations = new List<Station>();
            Trains = new List<Train>();
            Lines = new List<Line>();
            Schedules = new List<Schedule>();
            Tickets = new List<Ticket>();
            Rides = new List<Ride>();
            Routes = new List<Route>();
            AddUsers();
            AddStations();
            AddTrains();
            AddStationSchedules();
            AddRoutes();
            AddLines();
            AddSchedules();
            AddRides();
        }

        public static Data Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
