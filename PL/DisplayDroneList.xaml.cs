using System;
using System.Media;
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
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }
        
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = dataBase.DroneList(x=> x.Status == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem); 
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = dataBase.DroneList(x => x.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DisplayDrone(dataBase).Show();
            //new DisplayDroneList(dataBase).Show();
            this.Close();
        }

        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void DronesListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            new DisplayDrone(dataBase, (IBL.BO.DroneForList)DronesListView.SelectedItem).Show();
            this.Close();
        }
    }
}
