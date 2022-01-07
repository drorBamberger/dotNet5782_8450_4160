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

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayParcel.xaml
    /// </summary>
    public partial class DisplayParcel : Window
    {
        BLApi.IBL myBl;
        public BO.Parcel newParcel { get; set; }
        public DisplayParcel()
        {
            InitializeComponent();
        }
        public DisplayParcel(BLApi.IBL bl) //add Parcel
        {
            InitializeComponent();
            DataContext = this;
            newParcel = new BO.Parcel();
            newParcel.Sender = new BO.CustomerInParcel();
            newParcel.Target = new BO.CustomerInParcel();
            newParcel.MyDrone = new BO.DroneInParcel();
            myBl = bl;


        }
        public DisplayParcel(BLApi.IBL bl, BO.ParcelForList customer) //display and edit Parcel
        {
            InitializeComponent();
        }
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(myBl).Show();
            this.Close();
        }
        private void SenderClick(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(myBl).Show();
            this.Close();
        }
        private void TargetClick(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(myBl).Show();
            this.Close();
        }
        private void DroneClick(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(myBl).Show();
            this.Close();
        }

        private void Commit_All(object sender, RoutedEventArgs e)
        {
            try
            {
                //myBl.AddStation(newStation.Id, newStation.Name, newStation.MyLocation, newStation.ChargeSlots);
            }
            catch (BO.IdTakenException)
            {
                //myBl.StationUpdate(newStation.Id, newStation.Name, newStation.ChargeSlots + newStation.Drones.Count());
            }
            MessageBox.Show("Commited.");
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {

        }
        private void StatusClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
