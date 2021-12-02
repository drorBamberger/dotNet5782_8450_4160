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

            TextBox droneId = new TextBox();
            droneId.FontSize = 25;
            Grid.SetColumn(droneId, 1);
            Grid.SetRow(droneId, 0);
            droneId.Height = 43;
            droneId.Width = 380;
            displayDrone.Children.Add(droneId);

            TextBox model = new TextBox();
            model.FontSize = 25;
            Grid.SetColumn(model, 1);
            Grid.SetRow(model, 2);
            model.Height = 43;
            model.Width = 380;
            displayDrone.Children.Add(model);

            TextBox maxWeight = new TextBox();
            maxWeight.FontSize = 25;
            Grid.SetColumn(maxWeight, 1);
            Grid.SetRow(maxWeight, 4);
            maxWeight.Height = 43;
            maxWeight.Width = 380;
            displayDrone.Children.Add(maxWeight);

            TextBox stationId = new TextBox();
            stationId.FontSize = 25;
            Grid.SetColumn(stationId, 1);
            Grid.SetRow(stationId, 6);
            stationId.Height = 43;
            stationId.Width = 380;
            displayDrone.Children.Add(stationId);

        /*
        < Button FontStyle="Italic" FontSize="32" Content="Add the drone" HorizontalAlignment="Center" VerticalAlignment="Top" Click="Button_Click" Height="72" Width="400" Grid.Row="8"/>
        <Label  FontStyle="Italic" FontSize="25" Content="Enter id of drone:" HorizontalAlignment="Center" VerticalAlignment="Center"/>
        <Label  FontStyle="Italic" FontSize="25" Content="Enter model of drone:" HorizontalAlignment="Left" Margin="65,0,0,0" VerticalAlignment="Center" Grid.Row="2"/>
        <Label  FontStyle="Italic" FontSize="25" Content="Enter maximum weight:" HorizontalAlignment="Left" Margin="52,10,0,0" VerticalAlignment="Top" Grid.Row="4"/>
        <Label  FontStyle="Italic" FontSize="25" Content="Enter station id for intial chrging:" HorizontalAlignment="Center" VerticalAlignment="Center" Grid.Row="6"/>
        <TextBox FontStyle="Italic" FontSize="25" Grid.Column="1" HorizontalAlignment="Center" Text="" TextWrapping="Wrap" VerticalAlignment="Center" Width="380" Height="43"/>
        <TextBox FontStyle="Italic" FontSize="25" Grid.Column="1" HorizontalAlignment="Center" Text="" TextWrapping="Wrap" VerticalAlignment="Center" Width="380" Height="43" Grid.Row="6"/>
        <TextBox FontStyle="Italic" FontSize="25" Grid.Column="1" HorizontalAlignment="Center" Text="" TextWrapping="Wrap" VerticalAlignment="Center" Width="380" Height="43" Grid.Row="4"/>
        <TextBox FontStyle="Italic" FontSize="25" Grid.Column="1" HorizontalAlignment="Center" Text="" TextWrapping="Wrap" VerticalAlignment="Center" Width="380" Height="43" Grid.Row = "2" />
        */
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
            droneLocationLabel.Content = "drone location: " + myDrone.MyLocation;


        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
