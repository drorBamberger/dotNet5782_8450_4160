﻿using System;
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
        BLApi.IBL myBl;
        public BO.Station newParcel { get; set; }
        public DisplayParcel()
        {
            InitializeComponent();
        }
        public DisplayParcel(BLApi.IBL bl) //add Parcel
        {
            InitializeComponent();
        }
        public DisplayParcel(BLApi.IBL bl, BO.ParcelForList customer) //display and edit Parcel
        {
            InitializeComponent();
        }
    }
}