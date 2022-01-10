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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Window
    {
        BLApi.IBL myBl;
        public CustomerPage()
        {
            InitializeComponent();
        }
        public CustomerPage(BLApi.IBL bl, int id)
        {
            InitializeComponent();
            myBl = bl;
        }
    }
}
