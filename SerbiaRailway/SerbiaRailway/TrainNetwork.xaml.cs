using SerbiaRailway.model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TrainNetwork.xaml
    /// </summary>
    public partial class TrainNetwork : Page
    {
        public List<Station> stations = services.Data.Instance.Stations;
        public TrainNetwork()
        {
            InitializeComponent();
            foreach (var station in stations)
            {
                startingStationComboBox.Items.Add(station);
                endingStationComboBox.Items.Add(station);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (startingStationComboBox.SelectedItem == null)
            {
                if (endingStationComboBox.SelectedItem == null)
                {
                    MessageBox.Show("You must select starting station and ending station!");
                }
                else
                {
                    MessageBox.Show("You must select starting station!");
                }
                
            } else if (endingStationComboBox.SelectedItem == null)
            {
                MessageBox.Show("You must select ending station!");
            }
            else
            {
                Station startingStation = (Station)startingStationComboBox.SelectedItem;
                Station endingStation = (Station)endingStationComboBox.SelectedItem;

                if (startingStation == endingStation)
                {
                    MessageBox.Show("You cannot chose same station for starting and ending!");
                }
                else
                {
                    timetableCardPanel.Children.Clear();
                    //Line line = new Line();
                    //Station start = new Station();
                    //System.TimeSpan ts = new System.TimeSpan();
                    //PartialLine pl = new PartialLine(start, start, ts, ts, line);
                    //TimetableCard ttc = new TimetableCard("13.00", "14.00", "60 mins", pl, "autoprevoz");
                    //timetableCardPanel.Children.Add(ttc);



                    List<Line> lines = services.Data.Instance.Lines;
                    List<Line> foundLines = new List<Line>();
                    bool startingFound = false;
                    foreach (Line line in lines)
                    {
                        foreach (StationSchedule ss in line.StationSchedule)
                        {
                            if (ss.StartingStation == startingStation && startingFound == false)
                            {
                                startingFound = true;
                            }

                            if (ss.EndStation == endingStation & startingFound == true)
                            {
                                foundLines.Add(line);
                                break;
                            }
                        }
                        startingFound = false;
                    }

                    if (foundLines.Count == 0)
                    {
                        MessageBox.Show("Neither line is going from your starting to your ending station");
                    }
                    else
                    {
                        foreach (Line foundLine in foundLines)
                        {
                            TimeSpan departure = foundLine.GetStartingTimeByStation(startingStation);
                            TimeSpan arrival = foundLine.GetEndingTimeByStation(endingStation);
                            double price = foundLine.calculatePriceByTwoStation(startingStation, endingStation);
                            PartialLine partialLine = new PartialLine(startingStation, endingStation, departure, arrival, foundLine);
                            TimetableCard ttc = new TimetableCard(departure.ToString(), arrival.ToString(), (arrival - departure).ToString(), partialLine, foundLine.Train.Manufacturer, price);
                            timetableCardPanel.Children.Add(ttc);
                        }
                    }
                }
            }
        }
    }
}
