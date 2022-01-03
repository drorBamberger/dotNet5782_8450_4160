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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayDroneList.xaml
    /// </summary>
    public partial class DisplayDroneList : Window
    {
        static bool temp = false;
        private BLApi.IBL dataBase;
        CollectionView view;
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
        public ObservableCollection<BO.DroneForList> drones
        {
            get;
            set;
        }
        public DisplayDroneList()
        {
            InitializeComponent();
        }
        public DisplayDroneList(BLApi.IBL bl)
        {
            InitializeComponent();
            dataBase = bl;
            DronesListView.ItemsSource = dataBase.DroneList();

            view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);

            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            group.Visibility = Visibility.Hidden;
            if (StatusSelector.SelectedIndex != -1)
            {
                DronesListView.ItemsSource = dataBase.DroneList(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem);
            }
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            group.Visibility = Visibility.Hidden;
            if (WeightSelector.SelectedIndex != -1)
            {
                DronesListView.ItemsSource = dataBase.DroneList(x => x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            new DisplayDrone(dataBase).Show();
            this.Close();
        }

        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void DronesListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (DronesListView.SelectedItem is null)
                return;
            new DisplayDrone(dataBase, (BO.DroneForList)DronesListView.SelectedItem).Show();
            this.Close();
        }

        private void DispalyAll(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = dataBase.DroneList();
            temp = false;
            view.GroupDescriptions.Clear();
            StatusSelector.SelectedIndex = -1;
            WeightSelector.SelectedIndex = -1;
            group.Content = "Grouping";
            group.Visibility = Visibility.Visible;
        }
        private void GroupUnGroup(object sender, RoutedEventArgs e)
        {
            if (!temp)
            {
                view.GroupDescriptions.Add(groupDescription);
                group.Content = "UnGrouping";
            }
            else
            {
                view.GroupDescriptions.Clear();
                group.Content = "Grouping";
            }
            temp = !temp;
        }
    }
}
