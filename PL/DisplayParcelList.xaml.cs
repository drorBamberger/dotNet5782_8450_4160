﻿using System;
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
    /// Interaction logic for DisplayParcelList.xaml
    /// </summary>
    public partial class DisplayParcelList : Window
    {
        private BLApi.IBL dataBase;
        public DisplayParcelList()
        {
            InitializeComponent();
        }
        public DisplayParcelList(BLApi.IBL bl)
        {
            InitializeComponent();
            dataBase = bl;
            ParcelListView.ItemsSource = bl.ParcelList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.ParcelStatuses));
            //WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParcelListView.ItemsSource = dataBase.ParcelList(x => x.Status == (BO.ParcelStatuses)StatusSelector.SelectedItem);
        }

        //private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DronesListView.ItemsSource = dataBase.DroneList(x => x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcel(dataBase).Show();
            //new DisplayParcelList(dataBase).Show();
            this.Close();
        }

        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void ParcelListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            new DisplayParcel(dataBase, (BO.ParcelForList)ParcelListView.SelectedItem).Show();
            this.Close();
        }
    }
}
