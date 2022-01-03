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
    /// Interaction logic for DisplayParcelList.xaml
    /// </summary>
    public partial class DisplayParcelList : Window
    {
        private BLApi.IBL dataBase;
        static bool temp1 = false;
        static bool temp2 = false;
        CollectionView view;
        PropertyGroupDescription groupDescriptionSender = new PropertyGroupDescription("SenderName");
        PropertyGroupDescription groupDescriptionTarget = new PropertyGroupDescription("TargetName");
        public DisplayParcelList()
        {
            InitializeComponent();
        }
        public DisplayParcelList(BLApi.IBL bl)
        {
            InitializeComponent();
            dataBase = bl;
            ParcelListView.ItemsSource = dataBase.ParcelList();

            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);

            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.ParcelStatuses));
            //WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            groupSender.Visibility = Visibility.Hidden;
            groupTarget.Visibility = Visibility.Hidden;
            if (StatusSelector.SelectedIndex != -1)
            {
                ParcelListView.ItemsSource = dataBase.ParcelList(x => x.Status == (BO.ParcelStatuses)StatusSelector.SelectedItem);
            }
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            groupSender.Visibility = Visibility.Hidden;
            groupTarget.Visibility = Visibility.Hidden;
            if (StatusSelector.SelectedIndex != -1)
            {
                ParcelListView.ItemsSource = dataBase.ParcelList(x => x.Weight == (BO.WeightCategories)WeightSelector.SelectedItem);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcel(dataBase).Show();
            this.Close();
        }
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void ParcelListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (ParcelListView.SelectedItem is null)
                return;
            new DisplayParcel(dataBase, (BO.ParcelForList)ParcelListView.SelectedItem).Show();
            this.Close();
        }
        private void DispalyAll(object sender, RoutedEventArgs e)
        {
            ParcelListView.ItemsSource = dataBase.ParcelList();
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            temp1 = false;
            temp2 = false;
            view.GroupDescriptions.Clear();
            StatusSelector.SelectedIndex = -1;
            WeightSelector.SelectedIndex = -1;
            groupSender.Content = "Grouping Sender";
            groupTarget.Content = "Grouping Target";
            groupSender.Visibility = Visibility.Visible;
            groupTarget.Visibility = Visibility.Visible;
        }
        private void GroupUnGroupSender(object sender, RoutedEventArgs e)
        {
            if (!temp1)
            {
                view.GroupDescriptions.Clear();
                groupTarget.Content = "Grouping Target";
                temp2 = false;
                view.GroupDescriptions.Add(groupDescriptionSender);
                groupSender.Content = "UnGrouping Sender";
            }
            else
            {
                view.GroupDescriptions.Clear();
                groupSender.Content = "Grouping Sender";
            }
            temp1 = !temp1;
        }
        private void GroupUnGroupTarget(object sender, RoutedEventArgs e)
        {
            if (!temp2)
            {
                view.GroupDescriptions.Clear();
                groupSender.Content = "Grouping Sender";
                temp1 = false;
                view.GroupDescriptions.Add(groupDescriptionTarget);
                groupTarget.Content = "UnGrouping Target";
            }
            else
            {
                view.GroupDescriptions.Clear();
                groupTarget.Content = "Grouping Target";
            }
            temp2 = !temp2;
        }
    }
}
