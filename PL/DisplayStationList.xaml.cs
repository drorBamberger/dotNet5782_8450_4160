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
    public partial class DisplayStationList : Window
    {
        private BLApi.IBL dataBase;
        public DisplayStationList()
        {
            InitializeComponent();
        }
        public DisplayStationList(BLApi.IBL bl)
        {
            InitializeComponent();
            dataBase = bl;

            StationListView.ItemsSource = bl.StationList();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChargeSlots");
            view.GroupDescriptions.Add(groupDescription);

            ChargeSlots.Items.Add(new List<string>() { "all", "full", "have empty charge slots" });
        }

        private void ChargeSlots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((string)ChargeSlots.SelectedItem == "all")
            {
                StationListView.ItemsSource = dataBase.StationList(x => true);
            }
            else if((string)ChargeSlots.SelectedItem == "full")
            {
                StationListView.ItemsSource = dataBase.StationList(x => x.FreeChargeSlots == 0);
            }
            else
            {
                StationListView.ItemsSource = dataBase.StationList(x => x.FreeChargeSlots != 0);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DisplayStation(dataBase).Show();
            //new DisplayDroneList(dataBase).Show();
            this.Close();
        }

        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void StationListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (StationListView.SelectedItem is null)
                return;
            new DisplayStation(dataBase, (BO.StationForList)StationListView.SelectedItem).Show();
            this.Close();
        }
    }
}
