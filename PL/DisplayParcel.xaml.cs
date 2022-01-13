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
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayParcel.xaml
    /// </summary>
    public partial class DisplayParcel : Window
    {
        //BackgroundWorker bgw;
        BLApi.IBL myBl;
        public BO.Parcel newParcel { get; set; }
        public string drone { get; set; }
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
            Delete.Visibility = Visibility.Hidden;
            myBl = bl;
        }
        public DisplayParcel(BLApi.IBL bl, BO.ParcelForList parcel) //display and edit Parcel
        {
            InitializeComponent();
            DataContext = this;
            newParcel = new BO.Parcel();
            newParcel.Sender = new BO.CustomerInParcel();
            newParcel.Target = new BO.CustomerInParcel();
            newParcel.MyDrone = new BO.DroneInParcel();
            myBl = bl;
            newParcel = myBl.GetParcel(parcel.Id);
            myBl = bl;
            if(newParcel.Scheduled != null) drone = newParcel.MyDrone.ToString();
            Sender.IsReadOnly = true;
            Target.IsReadOnly = true;
            Priority.IsReadOnly = true;
            Weight.IsReadOnly = true;
             
        }
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcelList(myBl).Show();
            this.Close();
        }
        private void SenderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                new DisplayCustomer(myBl, myBl.CustomerList().Where(x => x.Id == newParcel.Sender.Id).First()).Show();
                this.Close();
            }
            catch
            {
                return;
            }
        }
        private void TargetClick(object sender, RoutedEventArgs e)
        {
            try
            {
                new DisplayCustomer(myBl, myBl.CustomerList().Where(x => x.Id == newParcel.Target.Id).First()).Show();
                this.Close();
            }
            catch
            {
                return;
            }
        }
        private void DroneClick(object sender, RoutedEventArgs e)
        {
            if (newParcel.Scheduled != null)
            {
                new DisplayDrone(myBl, myBl.GetDroneForList(newParcel.MyDrone.Id)).Show();
                this.Close();
            }
        }

        private void Commit_All(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.AddParcel(newParcel.Sender.Id, newParcel.Target.Id, (int)newParcel.Weight, (int)newParcel.Priority);
            }
            catch 
            {
                MessageBox.Show("please enter the reqested field");
                return;
            }
            MessageBox.Show("Commited.");
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if(newParcel.Scheduled != null)
            {
                MessageBox.Show("cant delete parcel!");
                return;
            }
            myBl.DeleteParcel(newParcel.Id);
            new DisplayParcelList(myBl).Show();
            this.Close();
        }
        private void StatusClick(object sender, RoutedEventArgs e)
        {
            if(newParcel.PickedUp == null && newParcel.Scheduled != null)
            {
                myBl.PickedParcelUp(newParcel.Id);
            }
            else if(newParcel.Delivered == null && newParcel.PickedUp != null)
            {
                myBl.ParcelDelivered(newParcel.Id);
            }
            else
            {
                MessageBox.Show("can't update...");
                return;
            }
            MessageBox.Show("sucess!");
            newParcel = myBl.GetParcel(newParcel.Id);
            DataContext = new DisplayParcel(myBl, myBl.ParcelList().Where(x => x.Id == newParcel.Id).First());
        }

        private void Simulator(object sender, RoutedEventArgs e)
        {
            bgw.RunWorkerAsync();
        }
    }
}
