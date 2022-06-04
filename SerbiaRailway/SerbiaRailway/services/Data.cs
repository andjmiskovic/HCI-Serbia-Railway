using SerbiaRailway.model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SerbiaRailway.services
{
    public sealed class Data
    {
        private static readonly Data instance = new Data();

        private Clients Clients;
        private LineCollection Lines;
        private Managers Managers;
        private TrainStations TrainStations;
        private StationSchedules StationSchedules;
        private ModelTrains Trains;
        private Schedules Schedules;
        static Data()
        {

        }

        public List<Client> GetClients()
        {
            return Clients.ClientUsers;
        }

        public List<Manager> GetManagers()
        {
            return Managers.ManagerUsers;
        }

        public List<Line> GetLines()
        {
            return Lines.Lines;
        }

        public List<Station> GetStations()
        {
            return TrainStations.Stations;
        }

        public List<string> GetStationNames()
        {
            List<string> stations = new List<string>();
            foreach (Station s in TrainStations.Stations)
                stations.Add(s.Name);
            return stations;
        }

        public Station GetStationByName(string name)
        {
            foreach (Station s in TrainStations.Stations)
                if (s.Name.Equals(name))
                    return s;
            return null;
        }

        public List<StationSchedule> GetStationSchedules()
        {
            return StationSchedules.Schedules;
        }

        public List<Train> GetTrains()
        {
            return Trains.TrainModels;
        }

        public List<Schedule> GetSchedules()
        {
            return Schedules.TravelSchedules;
        }

        private void LoadClients()
        {
            string fileName = "../../data/clients.json";
            string jsonString = File.ReadAllText(fileName);
            Clients = JsonSerializer.Deserialize<Clients>(jsonString);
        }

        private void LoadManagers()
        {
            string fileName = "../../data/managers.json";
            string jsonString = File.ReadAllText(fileName);
            Managers = JsonSerializer.Deserialize<Managers>(jsonString);
        }

        private void LoadLines()
        {
            string fileName = "../../data/lines.json";
            string jsonString = File.ReadAllText(fileName);
            Lines = JsonSerializer.Deserialize<LineCollection>(jsonString);
        }

        private void LoadStations()
        {
            string fileName = "../../data/stations.json";
            string jsonString = File.ReadAllText(fileName);
            TrainStations = JsonSerializer.Deserialize<TrainStations>(jsonString);
        }

        private void LoadStationSchedules()
        {
            string fileName = "../../data/stationSchedules.json";
            string jsonString = File.ReadAllText(fileName);
            StationSchedules = JsonSerializer.Deserialize<StationSchedules>(jsonString);
        }

        private void LoadTrains()
        {
            string fileName = "../../data/trains.json";
            string jsonString = File.ReadAllText(fileName);
            Trains = JsonSerializer.Deserialize<ModelTrains>(jsonString);
        }

        private void LoadTrainSchedules()
        {
            string fileName = "../../data/trainSchedules.json";
            string jsonString = File.ReadAllText(fileName);
            Schedules = JsonSerializer.Deserialize<Schedules>(jsonString);
        }

        private Data()
        {
            LoadClients();
            LoadManagers();
            LoadLines();
            LoadStations();
            LoadStationSchedules();
            LoadTrains();
            LoadTrainSchedules();
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
