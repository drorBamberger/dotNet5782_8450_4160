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
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayDrone.xaml
    /// </summary>
    public partial class DisplayDrone : Window
    {
        BackgroundWorker BGW;
        public BO.Drone localDrone { get; set; }
        BLApi.IBL myBl;
        bool isExit = false;

        public DisplayDrone()
        {
            InitializeComponent();
        }
        public DisplayDrone(BLApi.IBL bl) //add drone
        {
            //MessageBox.Show(bl.StationList().First().ToString());   // for knowing station id....
            InitializeComponent();
            myBl = bl;
            update.Click += Add_Click;


            droneId.Visibility = Visibility.Visible;
            modelAdd.Visibility = Visibility.Visible;
            maxWeight.Visibility = Visibility.Visible;
            stationId.Visibility = Visibility.Visible;
            labelId.Visibility = Visibility.Visible;
            labelModel.Visibility = Visibility.Visible;
            labelWeight.Visibility = Visibility.Visible;
            labelStatId.Visibility = Visibility.Visible;

            idLabel.Visibility = Visibility.Hidden;
            func1.Visibility = Visibility.Hidden;
            modelUpdate.Visibility = Visibility.Hidden;
            Simulation.Visibility = Visibility.Hidden;
        }
        public DisplayDrone(BLApi.IBL bl, BO.DroneForList drone) //display and edit drone
        {
            InitializeComponent();
            DataContext = this;
            myBl = bl;
            localDrone = myBl.GetDrone(drone.Id);

            bool flag = localDrone.Status == BO.DroneStatuses.Shipping;


            if (drone.Status == BO.DroneStatuses.maintenance)
            {
                func1.Content = "Release drone from charger";
                func1.Click += DisChargeDrone;
                timeText.Visibility = Visibility.Visible;
                timeText.IsEnabled = true;
                timeLabel.Content = "enter time in charge:";
            }
            else if (drone.Status == BO.DroneStatuses.vacant)
            {
                func1.Content = "Send drone to charge";
                func1.Click += ChargeDrone;
                //batteryPic.Visibility = Visibility.Visible;
                func2.Visibility = Visibility.Visible;
                func2.Content = "Link parcel to drone";
                func2.Click += Attribution;
            }
            else //shipping
            {
                parcel.Visibility = Visibility.Visible;
                B4.Visibility = Visibility.Hidden;
                B6.Visibility = Visibility.Hidden;
                if (bl.GetParcel(localDrone.MyParcel.Id).PickedUp == null)
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

            update.Click += Update_Click;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            new DisplayDroneList(myBl).Show();
            this.Close();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int DroneId;
            string Model;
            int MaxWieght;
            int StationId;

            if (int.TryParse(droneId.Text, out DroneId))
            {
                if (int.TryParse(maxWeight.Text, out MaxWieght) && MaxWieght <= 2 && MaxWieght > -1)
                {
                    if (int.TryParse(stationId.Text, out StationId))
                    {
                        Model = modelUpdate.Text;
                        try
                        {
                            myBl.AddDrone(DroneId, Model, MaxWieght, StationId);
                            MessageBox.Show("drone added!!!!");
                            new DisplayDroneList(myBl).Show();
                            this.Close();
                        }
                        catch (BO.IdTakenException err)
                        {
                            MessageBox.Show("cannot to add because id is taken");
                        }
                        catch (BO.IdNotExistException err)
                        {
                            MessageBox.Show("cannot to add because id is not exist");
                        }
                    }
                    else
                    {
                        MessageBox.Show("please enter number for station id):");
                    }
                }
                else
                {
                    MessageBox.Show("please enter number between 0-2 for Maximum Wieght):");
                }
            }
            else
            {
                MessageBox.Show("please enter number for id):");
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (modelUpdate.Text != localDrone.Model)
            {
                myBl.DroneUpdate(localDrone.Id, modelUpdate.Text);
                MessageBox.Show("drone updated!!!!");
            }
            else
            {
                MessageBox.Show("No change to update");
            }
        }
        private void DisChargeDrone(object sender, RoutedEventArgs e)
        {
            double time = 10;
            try
            {
                if (!double.TryParse(timeText.Text, out time))
                {
                    MessageBoxResult a = MessageBox.Show("enter time in charge!!!");
                    return;
                }
                myBl.DisChargeDrone(localDrone.Id, time);
            }
            catch (BO.CantBeNegative ERR)
            {
                MessageBox.Show(ERR.ToString());
                return;
            }

            MessageBox.Show("drone disCharging!!!!");
            new DisplayDrone(myBl, myBl.GetDroneForList(localDrone.Id)).Show();
            this.Close();
        }
        private void ChargeDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.ChargeDrone(localDrone.Id);
            }
            catch (BO.DroneIsntVacant ERR)
            {
                MessageBox.Show(ERR.ToString());
                return;
            }
            MessageBox.Show("drone charging!!!!");
            new DisplayDrone(myBl, myBl.GetDroneForList(localDrone.Id)).Show();
            this.Close();
        }
        private void Attribution(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.Attribution(localDrone.Id);
                parcel.Visibility = Visibility.Visible;
                B4.Visibility = Visibility.Hidden;
                B6.Visibility = Visibility.Hidden;
            }
            catch (BO.NoParcelMatch ERR)
            {
                MessageBox.Show(ERR.ToString());
                return;
            }
            MessageBox.Show("drone Attribution!!!!");
            new DisplayDrone(myBl, myBl.GetDroneForList(localDrone.Id)).Show();
            this.Close();
        }
        private void PickedParcelUp(object sender, RoutedEventArgs e)
        {
            myBl.PickedParcelUp(localDrone.MyParcel.Id);
            MessageBox.Show("drone picked parcel!!!!");
            new DisplayDrone(myBl, myBl.GetDroneForList(localDrone.Id)).Show();
            this.Close();
        }
        private void ParcelDelivered(object sender, RoutedEventArgs e)
        {
            myBl.ParcelDelivered(localDrone.MyParcel.Id);
            MessageBox.Show("parcel delivered!!!!");
            new DisplayDrone(myBl, myBl.GetDroneForList(localDrone.Id)).Show();
            this.Close();
        }
        private void Simulation_Click(object sender, RoutedEventArgs e)
        {
            update.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Hidden;
            func1.Visibility = Visibility.Hidden;
            func2.Visibility = Visibility.Hidden;
            Simulation.Click -= Simulation_Click;
            Simulation.Click += Un_Simulation_Click;
            Simulation.Content = "Regular";
            
            BGW = new BackgroundWorker();
            BGW.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);

            BGW.RunWorkerAsync();
        }


        private void Un_Simulation_Click(object sender, RoutedEventArgs e)
        {
            update.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Visible;
            func1.Visibility = Visibility.Visible;
            func2.Visibility = Visibility.Visible;
            Simulation.Click += Simulation_Click;
            Simulation.Click -= Un_Simulation_Click;
            Simulation.Content = "Simulation";

        }
        bool stop()
        {
           return true;
        }
        void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action temp = ConsolePrint;
            myBl.PlaySimulator(localDrone.Id, temp, stop);
            return;
        }

        public static void ConsolePrint()
        {
            Console.WriteLine(10);
        }

    }
}
