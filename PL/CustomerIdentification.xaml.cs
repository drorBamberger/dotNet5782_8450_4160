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
    /// Interaction logic for CustomerIdentification.xaml
    /// </summary>
    public partial class CustomerIdentification : Window
    {
        BLApi.IBL myBl;
        public CustomerIdentification()
        {
            InitializeComponent();
        }
        public CustomerIdentification(BLApi.IBL bl)
        {
            InitializeComponent();
            myBl = bl;
        }
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myBl.GetCustomer(int.Parse(Id.Text)).Name == Name.Text)
                {
                    new CustomerInterface(myBl, myBl.CustomerList().Where(x=>x.Id.ToString() == Id.Text).First());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("invalid name or Id!");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("invalid name or Id!");
                return;
            }
        }
        private void Sigh_Up_Click(object sender, RoutedEventArgs e)
        {
            new DisplayCustomer(myBl).Show();
        }
    }
}
