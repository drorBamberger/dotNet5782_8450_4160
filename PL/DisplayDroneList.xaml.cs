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
    /// Interaction logic for DisplayDroneList.xaml
    /// </summary>
    public partial class DisplayDroneList : Window
    {
        private IBL.IBL dataBase;
        public DisplayDroneList()
        {
            InitializeComponent();
        }
        public DisplayDroneList(IBL.IBL bl)
        {
            InitializeComponent();
            dataBase = bl;
            DronesListView.ItemsSource = bl.DroneList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            StatusSelector.SelectedItem
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
