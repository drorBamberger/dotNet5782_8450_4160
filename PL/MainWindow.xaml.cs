using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static BLApi.IBL dataBase = new BL.BL();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void GoToDrones(object sender, RoutedEventArgs e)
        {
            new DisplayDroneList(dataBase).Show();
            this.Close();
        }
        private void GoToStations(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(dataBase).Show();
            this.Close();
        }
        private void GoToCustomers(object sender, RoutedEventArgs e)
        {
            new DisplayCustomerList(dataBase).Show();
            this.Close();
        }
        private void GoToParcels(object sender, RoutedEventArgs e)
        {
            new DisplayParcelList(dataBase).Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            new CustomerIdentification(dataBase).Show();
            this.Close();
        }


    }
    
     
        
    
}
