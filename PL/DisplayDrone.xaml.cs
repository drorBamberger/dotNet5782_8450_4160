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
    /// Interaction logic for DisplayDrone.xaml
    /// </summary>
    public partial class DisplayDrone : Window
    {
        private IBL.IBL dataBase;
        public DisplayDrone()
        {
            InitializeComponent();
        }
        public DisplayDrone(IBL.IBL bl) //add drone
        {
            InitializeComponent();
            dataBase = bl;

            Label labelId = new Label();
            labelId.Content = "Enter the id of the drone: ";
            labelId.FontSize = 25;
            Grid.SetRow(labelId, 0);
            Grid.SetColumn(labelId, 0);
            displayDrone.Children.Add(labelId);

            TextBox text = new TextBox();

            Label labelModel = new Label();
            labelModel.Content = "Enter the model of the drone: ";
            labelModel.FontSize = 25;
            Grid.SetRow(labelModel, 2);
            Grid.SetColumn(labelModel, 0);
            displayDrone.Children.Add(labelModel);

            Label labelWeight = new Label();
            labelWeight.Content = "Enter max weight of the drone: ";
            labelWeight.FontSize = 25;
            Grid.SetRow(labelWeight, 4);
            Grid.SetColumn(labelWeight, 0);
            displayDrone.Children.Add(labelWeight);

            Label labelStatId = new Label();
            labelStatId.Content = "Enter station id for initial charging: ";
            labelStatId.FontSize = 25;
            Grid.SetRow(labelStatId, 6);
            Grid.SetColumn(labelStatId, 0);
            displayDrone.Children.Add(labelStatId);


        }

        public DisplayDrone(IBL.IBL bl, IBL.BO.DroneForList drone) //display and edit drone
        {
            InitializeComponent();
            dataBase = bl;
            IBL.BO.Drone myDrone = bl.GetDrone(drone.Id);
            idLabel.Content = "Id: " + myDrone.Id;
            maxWeightLabel.Content = "max weight drone can lift:\n" + myDrone.MaxWeight;
            batteryLabel.Content = (int)myDrone.Battery+"%";
            statusLabel.Content = myDrone.Status;
            parcelIdLabel.Content = "parcel ID: "+myDrone.MyParcel.Id;
            parcelStatusLabel.Content = "parcel status: "+myDrone.MyParcel.Status;
            parcelWeightLabel.Content = "parcel weight: "+myDrone.MyParcel.Weight;
            parcelPriorityLabel.Content = "priority: "+myDrone.MyParcel.Priority;
            senderIdLabel.Content = "sender ID: "+myDrone.MyParcel.Sender.Id;
            senderNameLabel.Content = "sender name: " + myDrone.MyParcel.Sender.Name;
            sendelLocLabel.Content = "sender location: " + myDrone.MyParcel.Collecting;
            targetIdLabel.Content = "target ID: " + myDrone.MyParcel.Receiver.Id;
            targetNameLabel.Content = "target name: " + myDrone.MyParcel.Receiver.Name;
            targetLocLabel.Content = "target location: " + myDrone.MyParcel.Target;
            transferDisLabel.Content = "distance: "+(int)myDrone.MyParcel.TransferDistance;
            droneLocationLabel.Content = "drone location: "+myDrone.MyLocation;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
