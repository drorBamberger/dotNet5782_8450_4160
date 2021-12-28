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
    /// Interaction logic for DisplayCustomer.xaml
    /// </summary>
    public partial class DisplayCustomer : Window
    {
        BLApi.IBL myBl;
        public BO.Customer newCustomer { get; set; }
        public DisplayCustomer()
        {
            InitializeComponent();
        }
        public DisplayCustomer(BLApi.IBL bl) //add Customer
        {
            InitializeComponent();
            newCustomer = new BO.Customer();
            newCustomer.CustomerLocation = new BO.Location();
            DataContext = this;
            myBl = bl;
        }
        public DisplayCustomer(BLApi.IBL bl, BO.CustomerForList customer) //display and edit Customer
        {
            InitializeComponent();
            DataContext = this;
            myBl = bl;
            newCustomer = myBl.GetCustomer(customer.Id);
            IdTextBox.IsReadOnly = true;
            LonTextBox.IsReadOnly = true;
            LatTextBox.IsReadOnly = true;
            
        }
        private void Commit_All(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.AddCustomer(newCustomer.Id, newCustomer.Name, newCustomer.PhoneNum, newCustomer.CustomerLocation);
            }
            catch(BO.IdTakenException)
            {
                myBl.CustomerUpdate(newCustomer.Id, newCustomer.Name, newCustomer.PhoneNum);
            }
            MessageBox.Show("Commited.");
        }
        private void ToCustomerViewSelected(object sender, MouseButtonEventArgs e)
        {
            new DisplayParcel(myBl, (BO.ParcelForList)ToListView.SelectedItem).Show();
            this.Close();
        }
        private void FromCustomerViewSelected(object sender, MouseButtonEventArgs e)
        {
            new DisplayParcel(myBl, (BO.ParcelForList)FromListView.SelectedItem).Show();
            this.Close();
        }
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new DisplayCustomerList(myBl).Show();
            this.Close();
        }


    }
}
