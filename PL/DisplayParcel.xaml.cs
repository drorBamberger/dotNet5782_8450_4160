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
    /// Interaction logic for DisplayParcel.xaml
    /// </summary>
    public partial class DisplayParcel : Window
    {
        public DisplayParcel()
        {
            InitializeComponent();
        }
        public DisplayParcel(BLApi.IBL bl) //add Customer
        {
            InitializeComponent();
        }
        public DisplayParcel(BLApi.IBL bl, BO.CustomerForList customer) //display and edit Customer
        {
            InitializeComponent();
        }
    }
}
