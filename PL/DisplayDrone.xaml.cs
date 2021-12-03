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
        IBL.BO.DroneForList localDrone;
        IBL.IBL myBl;

        TextBox droneId = new TextBox();
        TextBox model = new TextBox();
        TextBox maxWeight = new TextBox();
        TextBox stationId = new TextBox();
        public DisplayDrone()
        {
            InitializeComponent();
        }
        public DisplayDrone(IBL.IBL bl) //add drone
        {
            InitializeComponent();
            myBl = bl;

            Label labelId = new Label();
            labelId.Content = "Enter the id of the drone: ";
            labelId.FontSize = 25;
            Grid.SetRow(labelId, 0);
            Grid.SetColumn(labelId, 0);
            displayDrone.Children.Add(labelId);


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

            droneId.FontSize = 25;
            Grid.SetColumn(droneId, 1);
            Grid.SetRow(droneId, 0);
            droneId.Height = 43;
            droneId.Width = 380;
            displayDrone.Children.Add(droneId);

            model.FontSize = 25;
            Grid.SetColumn(model, 1);
            Grid.SetRow(model, 2);
            model.Height = 43;
            model.Width = 380;
            displayDrone.Children.Add(model);

            maxWeight.FontSize = 25;
            Grid.SetColumn(maxWeight, 1);
            Grid.SetRow(maxWeight, 4);
            maxWeight.Height = 43;
            maxWeight.Width = 380;
            displayDrone.Children.Add(maxWeight);

            stationId.FontSize = 25;
            Grid.SetColumn(stationId, 1);
            Grid.SetRow(stationId, 6);
            stationId.Height = 43;
            stationId.Width = 380;
            displayDrone.Children.Add(stationId);

            update.Click += Add_Click;
        }

        public DisplayDrone(IBL.IBL bl, IBL.BO.DroneForList drone) //display and edit drone
        {
            InitializeComponent();

            localDrone = drone;
            myBl = bl;

            IBL.BO.Drone myDrone = bl.GetDrone(drone.Id);
            bool flag = myDrone.Status == IBL.BO.DroneStatuses.Shipping ;
            idLabel.Content = "Id: " + myDrone.Id;
            maxWeightLabel.Content = "max weight drone can lift:\n" + myDrone.MaxWeight;
            batteryLabel.Content = (int)myDrone.Battery+"%";
            statusLabel.Content = myDrone.Status;
            parcelIdLabel.Content = flag?"parcel ID: " +myDrone.MyParcel.Id: "";
            parcelStatusLabel.Content = flag?"parcel status: " +myDrone.MyParcel.Status : "";
            parcelWeightLabel.Content = flag?"parcel weight: " +myDrone.MyParcel.Weight : "";
            parcelPriorityLabel.Content = flag?"priority: " +myDrone.MyParcel.Priority : "";
            senderIdLabel.Content = flag?"sender ID: " +myDrone.MyParcel.Sender.Id : "";
            senderNameLabel.Content = flag ? "sender name: " + myDrone.MyParcel.Sender.Name : "";
            sendelLocLabel.Content = flag ? "sender location: " + myDrone.MyParcel.Collecting : "";
            targetIdLabel.Content = flag ? "target ID: " + myDrone.MyParcel.Receiver.Id : "";
            targetNameLabel.Content = flag ? "target name: " + myDrone.MyParcel.Receiver.Name : "";
            targetLocLabel.Content = flag ? "target location: " + myDrone.MyParcel.Target : "";
            transferDisLabel.Content = flag ? "distance: " +(int)myDrone.MyParcel.TransferDistance + " KM": "";
            droneLocationLabel.Content = "drone location: "+myDrone.MyLocation;

            Grid.SetRow(model, 2);
            model.Height = 20;
            model.Width = 120;
            Thickness myThickness = new Thickness();
            myThickness.Bottom = 6;
            myThickness.Left = 0;
            myThickness.Right = 260;
            myThickness.Top = 28;
            model.Margin = myThickness;
            model.Text = drone.Model;
            displayDrone.Children.Add(model);

            Button func1 = new Button();
            func1.Height = 74;
            func1.Width = 400;
            func1.FontSize = 32;
            func1.FontStyle = FontStyles.Italic;
            Grid.SetRow(func1, 6);

            Button func2 = new Button();
            func2.Height = 74;
            func2.Width = 400;
            func2.FontSize = 32;
            func2.FontStyle = FontStyles.Italic;
            Grid.SetRow(func2, 6);
            Grid.SetColumn(func2, 1);
            if (drone.Status == IBL.BO.DroneStatuses.maintenance)
            {
                func1.Content = "Release drone from charger";
                func1.Click += DisChargeDrone;
            }
            else if(drone.Status == IBL.BO.DroneStatuses.vacant)
            {
                func1.Content = "Send drone to charge";
                func1.Click += ChargeDrone;

                func2.Content = "Link parcel to drone";
                func2.Click += Attribution;
                displayDrone.Children.Add(func2);
            }
            else //shipping
            {
                if (bl.GetParcel(myDrone.MyParcel.Id).PickedUp == null)
                {
                    func1.Content = "Pick up parcel by drone";
                    func1.Click += PickedParcelUp;
                }
                else 
                {
                    func1.Content = "Deliver parcel by drone";
                    func1.Click += ParcelDelivered;
                }

            }
            displayDrone.Children.Add(func1);

            update.Click += Update_Click;

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            /*IBL.BO.DroneForList drone;
            drone.Id = (int)droneId.Text;*/
            this.Dispatcher.Invoke((Action)(() =>
            {

                this.Close();
            }));
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int DroneId;
            string Model;
            int MaxWieght;
            int StationId;

            int.TryParse(droneId.Text, out DroneId);
            Model = model.Text;
            int.TryParse(maxWeight.Text, out MaxWieght);
            int.TryParse(stationId.Text, out StationId);

            try
            {
                myBl.AddDrone(DroneId, Model, MaxWieght, StationId);
                MessageBox.Show("drone added!!!!");
            }
            catch (BL.BO.IdTakenException err)
            {
                MessageBox.Show("cannot to add because id is taken");
                this.Close();
            }
            catch (BL.BO.IdNotExistException err)
            {
                MessageBox.Show("cannot to add because id is not exist");
            }
            
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (model.Text != localDrone.Model)
            {
                myBl.DroneUpdate(localDrone.Id, model.Text);
                MessageBox.Show("drone updated!!!!");
            }
            else
            {
                MessageBox.Show("No chnage to update");
            }
        }
        private void DisChargeDrone(object sender, RoutedEventArgs e)
        {
            double time = 0.01;
            myBl.DisChargeDrone(localDrone.Id, time);
            MessageBox.Show("drone disCharging!!!!");
            localDrone = myBl.GetDroneForList(localDrone.Id);
            new DisplayDrone(myBl, localDrone).Show();
            this.Close();
        }

        private void ChargeDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.ChargeDrone(localDrone.Id);
            }
            catch(BL.BO.DroneIsntVacant ERR)
            {
                MessageBox.Show(ERR.ToString());
                return;
            }
            MessageBox.Show("drone charging!!!!");
            localDrone = myBl.GetDroneForList(localDrone.Id);
            new DisplayDrone(myBl, localDrone).Show();
            this.Close();
        }
        private void Attribution(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.Attribution(localDrone.Id);
            }
            catch(BL.BO.NoParcelMatch ERR)
            {
                MessageBox.Show(ERR.ToString());
                return;
            }
            localDrone = myBl.GetDroneForList(localDrone.Id);
            MessageBox.Show("drone Attribution!!!!");
            new DisplayDrone(myBl, localDrone).Show();
            this.Close();
        }
        private void PickedParcelUp(object sender, RoutedEventArgs e)
        {
            myBl.PickedParcelUp(localDrone.ParcelId);
            localDrone = myBl.GetDroneForList(localDrone.Id);
            MessageBox.Show("drone picked parcel!!!!");
            new DisplayDrone(myBl, localDrone).Show();
            this.Close();
        }
        private void ParcelDelivered(object sender, RoutedEventArgs e)
        {
            myBl.ParcelDelivered(localDrone.ParcelId);
            localDrone = myBl.GetDroneForList(localDrone.Id);
            MessageBox.Show("parcel delivered!!!!");
            new DisplayDrone(myBl, localDrone).Show();
            this.Close();
        }

    }
}
