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
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(myBl).Show();
            this.Close();

        }
        private void DroneListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            new DisplayDrone(myBl, (BO.DroneForList)DroneListView.SelectedItem).Show();
            this.Close();
        }
    }
}
