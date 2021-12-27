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
    public partial class DisplayCustomerList : Window
    {
        private BLApi.IBL dataBase;
        public DisplayCustomerList()
        {
            InitializeComponent();
        }
        public DisplayCustomerList(BLApi.IBL bl)
        {
            InitializeComponent();
            dataBase = bl;
            CustomerListView.ItemsSource = bl.CustomerList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DisplayCustomer(dataBase).Show();
            //new DisplayDroneList(dataBase).Show();
            this.Close();
        }

        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void CustomersListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            new DisplayCustomer(dataBase, (BO.DroneForList)CustomerListView.SelectedItem).Show();
            this.Close();
        }
    }
}
