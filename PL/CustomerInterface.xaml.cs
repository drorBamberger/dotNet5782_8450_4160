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
        public CustomerInterface(BLApi.IBL bl) //add Customer
        {
            InitializeComponent();
            newCustomer = new BO.Customer();
            newCustomer.CustomerLocation = new BO.Location();
            DataContext = this;
            myBl = bl;
            Add_Click.Visibility = Visibility.Hidden;
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
            this.Close();
        }
        private void FromCustomerViewSelected(object sender, MouseButtonEventArgs e)
        {
            new CustomerInterfaceParcel(myBl, myBl.ParcelList().Where(x => x.Id == ((BO.ParcelForCustomer)FromListView.SelectedItem).Id).First()).Show();
            this.Close();
        }
        private void Commit_All(object sender, RoutedEventArgs e)
        {

            try
            {
                myBl.AddCustomer(newCustomer.Id, newCustomer.Name, newCustomer.PhoneNum, newCustomer.CustomerLocation);
            }
            catch (BO.IdTakenException)
            {
                myBl.CustomerUpdate(newCustomer.Id, newCustomer.Name, newCustomer.PhoneNum);
            }
            MessageBox.Show("Commited.");
            Close_Window_Click(sender, e);
        }


        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new CustomerIdentification(myBl).Show();
            this.Close();
        }
        private void Add_Parcel_Click(object sender, RoutedEventArgs e)
        {
            new CustomerInterfaceParcel(myBl, newCustomer.Id).Show();
            this.Close();
        }

    }
}
