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
            Label[] labels = new Label[4];
            TextBox model = new TextBox();
            for (int i = 0;i< 4;++i )
            {
                labels[i] = new Label();
            }
            model.Text = "\n\n\n"+myDrone.Model;
            Grid.SetColumn(model, 1);
            displayDrone.Children.Add(model);
            bl.DroneUpdate(drone.Id, model.Text);
            labels[0].Content = "Drone ID " + myDrone.Id;
            labels[0].FontSize = 30;
            labels[1].Content = (int)myDrone.Battery + "%";
            Grid.SetColumn(labels[1], 1);
            labels[2].Content = "\nmaximum weight: " + myDrone.MaxWeight;
            Grid.SetColumn(labels[2], 1);
            labels[3].Content = "\n\nstatus: " + myDrone.Status;


            Grid.SetColumn(labels[3], 1);

            foreach (var label in labels)
            {
                displayDrone.Children.Add(label);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
