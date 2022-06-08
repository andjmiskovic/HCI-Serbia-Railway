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
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        public AddLine()
        {
            InitializeComponent();
            foreach (Station station in DataService.Data.Stations)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = station.ToString();
                AllStations.Items.Add(listBoxItem);
            }
        }

        Point AllStationsStartMousePos;
        Point LineStationsStartMousePos;

        private void AllStations_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AllStationsStartMousePos = e.GetPosition(null);
        }

        private void AllStations_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                if (!AllStations.Items.Contains(listItem))
                {
                    AllStations.Items.Add(listItem);
                }
            }
        }

        private void AllStations_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                AllStations.Items.Remove(listItem);
            }
        }

        private void AllStations_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selectedItem = (ListBoxItem)AllStations.SelectedItem;
                    AllStations.Items.Remove(selectedItem);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Move);
                    if (selectedItem.Parent == null)
                    {
                        AllStations.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }

        private void LineStations_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LineStationsStartMousePos = e.GetPosition(null);
        }

        private void LineStations_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                if (!LineStations.Items.Contains(listItem))
                {
                    LineStations.Items.Add(listItem);
                }
            }
        }

        private void LineStations_DragLeave(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                LineStations.Items.Remove(listItem);
            }
        }

        private void LineStations_MouseMove(object sender, MouseEventArgs e)
        {
            Point mPos = e.GetPosition(null);
            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selectedItem = (ListBoxItem)LineStations.SelectedItem;
                    LineStations.Items.Remove(selectedItem);
                    DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem), DragDropEffects.Move);
                    if (selectedItem.Parent == null)
                    {
                        LineStations.Items.Add(selectedItem);
                    }
                }
                catch { }
            }
        }
    }
}
