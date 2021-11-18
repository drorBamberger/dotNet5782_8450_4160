using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL.DO;




namespace DalObject
{

    class DataSource
    {
        public static List<Drone> Drones;
        public static List<Station> Stations;
        public static List<Customer> Customers;
        public static List<Parcel> Parcels;
        public static List<DroneCharge> Charging;
        public static int ParcelId = 0;
        internal class Config
        {
            public static double Available = 1.2;
            public static double SmallPackege = 3;
            public static double MediumPackege = 6;
            public static double HeavyPackege = 10;
            public static double ChargePerHour = 20;
        }
        public static void Initialize()
        {
            Random rnd = new Random();
            //stations initialization
            Stations.Add(new Station(rnd.Next(), "London", -0.118092, 51.509865, 10));
            Stations.Add(new Station(rnd.Next(), "Paris", 2.349014, 48.864716, 8));
            //customers initialization
            string[] customerNames = new string[] { "Dror", "Yair", "Ofir", "Gil", "Benaya", "Ohad", "Michael", "Achiya", "Drew", "Shilo" };
            for (int i = 0; i < 10; ++i)
            {
                Customers.Add(new Customer(rnd.Next(), customerNames[i], "05" + (rnd.Next(10000000, 99999999)).ToString()
                    , rnd.NextDouble() * 180 * Math.Pow(-1, rnd.Next(0, 1)), rnd.NextDouble() * 90 * Math.Pow(-1, rnd.Next(0, 2))));
            }

            //drones initialization
            for (int i = 0; i < 5; i++)
            {
                Drones.Add(new Drone(rnd.Next(), "version" + i.ToString(), (WeightCategories)rnd.Next(0, 3)));

            }

            //parcels initialization
            for (ParcelId = 1; ParcelId < 11; ParcelId++)
            {
                Parcels.Add(new Parcel(Customers[rnd.Next(0, 10)].Id, Customers[rnd.Next(0, 10)].Id,
                   (WeightCategories)rnd.Next(0, 3), (Priorities)rnd.Next(0, 3), Drones[rnd.Next(0, 5)].Id));
            }

        }

    }

    public class DalObject : IDAL.IDal
    {
        // cnstrct:
        public DalObject()
        {
            DataSource.Initialize();
        }

        //help funcs:
        private void IsIdTaken<T>(List<T> list, int id)
        {
            foreach (T item in list)
            {
                int itemId = (int)((typeof(T).GetProperty("Id").GetValue(item, null)));
                if (itemId == id)
                {
                    throw new IdTakenException(id);
                }
            }
        }
        private void IsIdExist<T>(List<T> list, int id)
        {
            foreach (var item in list)
            {
                int itemId = (int)((typeof(T).GetProperty("Id").GetValue(item, null)));
                if (itemId == id)
                {
                    return;
                }
            }
            throw new IdNotExistException(id);
        }
        //add option

        public void AddStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            IsIdTaken(DataSource.Stations, id);
            DataSource.Stations.Add(new Station(id, name, longitude, lattitude, chargeSlots));
        }

        public void AddDrone(int id, string model, int maxWeight)
        {
            IsIdTaken(DataSource.Drones, id);
            DataSource.Drones.Add(new Drone(id, model, (WeightCategories)maxWeight));
        }

        public void AddCustomer(int id, string name, string phone, double longitude, double lattitude)
        {
            IsIdTaken(DataSource.Customers, id);
            DataSource.Customers.Add(new Customer(id, name, phone, longitude, lattitude));
        }

        public void AddParcel(int senderId, int targetId, int weight, int priority, int droneId)
        {
            DataSource.Parcels.Add(new Parcel(senderId, targetId, (WeightCategories)weight
                , (Priorities)priority, droneId));
        }
        //update options

        public void Attribution(int droneId, int parcelId)
        {
            IsIdExist(DataSource.Drones, droneId);
            IsIdExist(DataSource.Parcels, parcelId);
            Parcel tmp = DataSource.Parcels.Find(x => x.Id == parcelId);
            tmp.DroneId = droneId;
            tmp.Scheduled = DateTime.Now;
            DataSource.Parcels[DataSource.Parcels.FindIndex(x => x.Id == parcelId)] = tmp;
        }

        public void PickedParcelUp(int parcelId)
        {
            IsIdExist(DataSource.Parcels, parcelId);
            Parcel tmp = DataSource.Parcels.Find(x => x.Id == parcelId);
            tmp.PickedUp = DateTime.Now;
            DataSource.Parcels[DataSource.Parcels.FindIndex(x => x.Id == parcelId)] = tmp;
        }

        public void ParcelDelivered(int parcelId)
        {
            IsIdExist(DataSource.Parcels, parcelId);
            Parcel tmp = DataSource.Parcels.Find(x => x.Id == parcelId);
            tmp.Delivered = DateTime.Now;
            DataSource.Parcels[DataSource.Parcels.FindIndex(x => x.Id == parcelId)] = tmp;
        }

        public void ChargeDrone(int droneId, int stationId)
        {
            IsIdExist(DataSource.Drones, droneId);
            IsIdExist(DataSource.Stations, stationId);
            Station tmp = DataSource.Stations.Find(x => x.Id == stationId);
            DataSource.Charging.Add(new DroneCharge(droneId, stationId));
            tmp.ChargeSlots--;
            DataSource.Stations[DataSource.Stations.FindIndex(x => x.Id == stationId)] = tmp;
        }

        public void DisChargeDrone(int droneId, int stationId)
        {
            IsIdExist(DataSource.Drones, droneId);
            IsIdExist(DataSource.Stations, stationId);
            Station station = DataSource.Stations.Find(x => x.Id == stationId);
            station.ChargeSlots++;
            DataSource.Stations[DataSource.Stations.FindIndex(x => x.Id == stationId)] = station;
            DataSource.Charging.RemoveAll(x => x.DroneId == droneId);
        }


        //displays
        public Station DisplayStation(int id)
        {
            IsIdExist(DataSource.Stations, id);
            return DataSource.Stations.Find(x => x.Id == id);
        }

        public Drone DisplayDrone(int id)
        {
            IsIdExist(DataSource.Stations, id);
            return DataSource.Drones.Find(x => x.Id == id);
        }

        public Customer DisplayCustomer(int id)
        {
            IsIdExist(DataSource.Stations, id);
            return DataSource.Customers.Find(x => x.Id == id);
        }

        public Parcel DisplayParcel(int id)
        {
            IsIdExist(DataSource.Stations, id);
            return DataSource.Parcels.Find(x => x.Id == id);
        }

        //displays Lists

        public IEnumerable<Station> StationList()
        {
            List<Station> stationList = new List<Station>();
            stationList = DataSource.Stations;
            return stationList;
        }

        public IEnumerable<Drone> DroneList()
        {
            List<Drone> droneList = new List<Drone>();
            droneList = DataSource.Drones;
            return droneList;
        }

        public IEnumerable<Customer> CustomerList()
        {
            List<Customer> customerList = new List<Customer>();
            customerList = DataSource.Customers;
            return customerList;
        }

        public IEnumerable<Parcel> ParcelList()
        {
            List<Parcel> parcelList = new List<Parcel>();
            parcelList = DataSource.Parcels;
            return parcelList;
        }

        public IEnumerable<Parcel> ParcelListNotTaken()
        {
            List<Parcel> parcelList = new List<Parcel>();
            foreach (var item in DataSource.Parcels)
            {
                if (item.DroneId == 0)
                {
                    parcelList.Add(item);
                }
            }
            return parcelList;
        }

        public IEnumerable<Station> FreeStations()
        {
            List<Station> stationList = new List<Station>();
            foreach (var item in DataSource.Stations)
            {
                if (item.ChargeSlots != 0)
                {
                    stationList.Add(item);
                }
            }
            return stationList;
        }
        public double[] AskForElectricity()
        {
            return new double[5] { DataSource.Config.Available, DataSource.Config.SmallPackege, DataSource.Config.MediumPackege,
                DataSource.Config.HeavyPackege, DataSource.Config.ChargePerHour };
        }

        //update funcs:

        public void UpDateDrone(Drone newDrone)
        {
            DataSource.Drones[DataSource.Drones.FindIndex(x => x.Id == newDrone.Id)] = newDrone;
        }
        public void UpDateCustomer(Customer newCustomer)
        {
            DataSource.Customers[DataSource.Customers.FindIndex(x => x.Id == newCustomer.Id)] = newCustomer;
        }
        public void UpDateStation(Station newStation)
        {
            DataSource.Stations[DataSource.Stations.FindIndex(x => x.Id == newStation.Id)] = newStation;
        }

    }
}