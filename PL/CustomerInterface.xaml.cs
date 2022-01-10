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

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerInterface.xaml
    /// </summary>
    public partial class CustomerInterface : Window
    {
        BLApi.IBL myBl;
        public BO.Customer newCustomer { get; set; }
        public CustomerInterface()
        {
            InitializeComponent();
        }

        public CustomerInterface(BLApi.IBL bl, BO.CustomerForList customer) //display and edit Customer
        {
            InitializeComponent();
            DataContext = this;
            myBl = bl;
            newCustomer = myBl.GetCustomer(customer.Id);
            IdTextBox.IsReadOnly = true;
            LonTextBox.IsReadOnly = true;
            LatTextBox.IsReadOnly = true;

        }
        private void ToCustomerViewSelected(object sender, MouseButtonEventArgs e)
        {
            new CustomerInterfaceParcel(myBl, myBl.ParcelList().Where(x => x.Id == ((BO.ParcelForCustomer)ToListView.SelectedItem).Id).First()).Show();
        }
        private void FromCustomerViewSelected(object sender, MouseButtonEventArgs e)
        {
            new CustomerInterfaceParcel(myBl, myBl.ParcelList().Where(x => x.Id == ((BO.ParcelForCustomer)FromListView.SelectedItem).Id).First()).Show();
        }
        private void Commit_All(object sender, RoutedEventArgs e)
        {
            
            myBl.CustomerUpdate(newCustomer.Id, newCustomer.Name, newCustomer.PhoneNum);
            
            MessageBox.Show("Commited.");
        }


        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void Add_Parcel_Click(object sender, RoutedEventArgs e)
        {
            new CustomerInterfaceParcel(myBl, newCustomer.Id).Show();
            this.Close();
        }

    }
}
