using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using Microsoft.VisualBasic;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayStation.xaml
    /// </summary>
    public partial class DisplayStation : Window
    {
        BLApi.IBL myBl;
        public BO.Station newStation { get; set; }
        public DisplayStation()
        {
            InitializeComponent();
        }
        public DisplayStation(BLApi.IBL bl) //add station
        {
            InitializeComponent();
            DataContext = this;
            newStation = new BO.Station();
            newStation.MyLocation = new BO.Location();
            myBl = bl;
        }
        public DisplayStation(BLApi.IBL bl, BO.StationForList station) //display and edit station
        {
            InitializeComponent();
            DataContext = this;
            myBl = bl;
            newStation = myBl.GetStation(station.Id);
            IdTextBox.IsReadOnly = true;
            LonTextBox.IsReadOnly = true;
            LatTextBox.IsReadOnly = true;
        }
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(myBl).Show();
            this.Close();
        }
        private void Commit_All(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.AddStation(newStation.Id, newStation.Name, newStation.MyLocation, newStation.ChargeSlots);
            }
            catch (BO.IdTakenException)
            {
                myBl.StationUpdate(newStation.Id, newStation.Name, newStation.ChargeSlots + newStation.Drones.Count());
            }
            MessageBox.Show("Commited.");
        }
        private void DroneListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            new DisplayDrone(myBl, myBl.GetDroneForList(((BO.DroneInCharge)DroneListView.SelectedItem).Id)).Show();
            this.Close();
        }
    }
}
