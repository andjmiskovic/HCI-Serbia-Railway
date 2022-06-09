using SerbiaRailway.model;
using SerbiaRailway.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for EnterLineData.xaml
    /// </summary>
    public partial class EnterLineData : Page
    {
        private List<StationSchedule> _stationSchedules { get; set; }

        public EnterLineData()
        {
            InitializeComponent();
        }

        public EnterLineData(List<StationSchedule> stationSchedules)
        {
            InitializeComponent();
            _stationSchedules = stationSchedules;
            dataGrid.ItemsSource = stationSchedules;
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.CanUserReorderColumns = false;
            dataGrid.CanUserSortColumns = false;
            trainComboBox.ItemsSource = DataService.Data.Trains;
            trainComboBox.DisplayMemberPath = "Manufacturer";
            trainComboBox.SelectedValuePath = "Id";
            trainComboBox.SelectedItem = 1;
        }

        private void saveLine_Click(object sender, RoutedEventArgs e)
        {
            List<StationSchedule> stationSchedules = new List<StationSchedule>();
            foreach (StationSchedule stationSchedule in _stationSchedules)
            {
                stationSchedules.Add(new StationSchedule(stationSchedule.StartingStation, stationSchedule.EndStation,
                    stationSchedule.Departure, stationSchedule.Arrival, stationSchedule.Price));
            }
            int id = DataService.Data.Lines.Count + 1;
            string name = stationSchedules[0].StartingStation + "-" + stationSchedules[stationSchedules.Count - 1].EndStation;
            DataService.Data.Lines.Add(new model.Line(id, name, stationSchedules, (Train)trainComboBox.SelectedItem));
            Application.Current.Windows[1].Close();
        }
    }
}
