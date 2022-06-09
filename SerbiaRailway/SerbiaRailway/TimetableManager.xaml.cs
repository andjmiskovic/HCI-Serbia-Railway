using SerbiaRailway.model;
using SerbiaRailway.services;
using System.Windows.Controls;

namespace SerbiaRailway
{
    /// <summary>
    /// Interaction logic for TimetableManager.xaml
    /// </summary>
    public partial class TimetableManager : Page
    {
        public TimetableManager()
        {
            InitializeComponent();
            FillLines();
        }

        private void FillLines()
        {
            Line.Items.Clear();
            foreach (Line line in DataService.Data.Lines)
            {
                Line.Items.Add(line.Id + " (" + line.Name + ")");
            }
        }

        public void SetLines()
        {
            int lineId = int.Parse(Line.SelectedItem.ToString().Split('(')[0]);
        }

        private void SaveChanges(object sender, System.Windows.RoutedEventArgs e)
        {
            // save changes
        }
    }
}
