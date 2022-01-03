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
        static bool temp = false;
        CollectionView view;
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("FreeChargeSlots");
        public DisplayStationList()
        {
            InitializeComponent();
        }
        public DisplayStationList(BLApi.IBL bl)
        {
            InitializeComponent();
            dataBase = bl;
            StationListView.ItemsSource = bl.StationList();

            view = (CollectionView)CollectionViewSource.GetDefaultView(StationListView.ItemsSource);
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
