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
            Label labely = new Label();
            labely.Content = "Hiiiiiiiiiiiiiii";
            labely.FontSize = 40; 
            displayDrone.Children.Add(labely);
        }

        public DisplayDrone(IBL.IBL bl, IBL.BO.DroneForList drone) //display and edit drone
        {
            InitializeComponent();
            dataBase = bl;
            IBL.BO.Drone myDrone = bl.GetDrone(drone.Id);
            Label id = new Label();
            id.Content = "Drone ID: " + myDrone.Id;
            d
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
