using System;
using System.Collections.Generic;
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
            for (int i = 0; i < 10; i++)
            {
                Parcels.Add(new Parcel(i+1, Customers[rnd.Next(0, 10)].Id, Customers[rnd.Next(0, 10)].Id, 
                   (WeightCategories)rnd.Next(0, 3),  (Priorities)rnd.Next(0, 3), Drones[rnd.Next(0, 5)].Id));
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
            foreach(T item in list)
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

        public void addStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            IsIdTaken(DataSource.Stations, id);
            DataSource.Stations.Add(new Station(id, name, longitude, lattitude, chargeSlots));
        }

        public void addDrone(int id, string model, int maxWeight)
        {
            IsIdTaken(DataSource.Drones, id);
            DataSource.Drones.Add(new Drone(id, model, (WeightCategories)maxWeight));
        }

        public void addCustomer(int id, string name, string phone, double longitude, double lattitude)
        {
            IsIdTaken(DataSource.Customers, id);
            DataSource.Customers.Add(new Customer(id, name, phone, longitude, lattitude));
        }

        public void addParcel(int id, int senderId, int targetId, int weight, int priority, int droneId)
        {
            IsIdTaken(DataSource.Stations, id);
            DataSource.Parcels.Add(new Parcel(id, senderId, targetId, (WeightCategories)weight
                , (Priorities)priority, droneId));
        }
        //update options

        public void attribution(int droneId, int parcelId)
        {
            IsIdExist(DataSource.Drones, droneId);
            IsIdExist(DataSource.Parcels, parcelId);
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                if (DataSource.Parcels[i].Id == parcelId)
                {
                    Parcel tmp = DataSource.Parcels[i];
                    tmp.DroneId = droneId;
                    tmp.Scheduled = DateTime.Now;
                    DataSource.Parcels[i] = tmp;
                }
            }
        }

        public void PickedParcelUp(int parcelId)
        {
            IsIdExist(DataSource.Parcels, parcelId);
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                if (DataSource.Parcels[i].Id == parcelId)
                {
                    Parcel tmp = DataSource.Parcels[i];
                    tmp.PickedUp = DateTime.Now;
                    DataSource.Parcels[i] = tmp;
                }
            }
        }

        public void ParcelDelivered(int parcelId)
        {
            IsIdExist(DataSource.Parcels, parcelId);
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                if (DataSource.Parcels[i].Id == parcelId)
                {
                    Parcel tmp = DataSource.Parcels[i];
                    tmp.Delivered = DateTime.Now;
                    DataSource.Parcels[i] = tmp;
                }
            }
        }

        public void ChargeDrone(int droneId, int stationId)
        {
            IsIdExist(DataSource.Drones, droneId);
            IsIdExist(DataSource.Stations, stationId);
            for (int i = 0; i < DataSource.Stations.Count; i++)
            {
                if (DataSource.Stations[i].Id == stationId)
                {

                    DataSource.Charging.Add(new DroneCharge(droneId, stationId));
                    Station tmp = DataSource.Stations[i];
                    tmp.ChargeSlots--;
                    DataSource.Stations[i] = tmp;
                }
            }
        }

        public void DisChargeDrone(int droneId, int stationId)
        {
            IsIdExist(DataSource.Drones, droneId);
            IsIdExist(DataSource.Stations, stationId);
            for (int i = 0; i < DataSource.Stations.Count; i++)
            {
                if (DataSource.Stations[i].Id == stationId)
                {
                    Station tmp = DataSource.Stations[i];
                    tmp.ChargeSlots++;
                    DataSource.Stations[i] = tmp;
                }
            }

        }


        //displays
        public Station DisplayStation(int id)
        {
            IsIdExist(DataSource.Stations, id);
            for (int i = 0; i < DataSource.Stations.Count; i++)
            {
                if (DataSource.Stations[i].Id == id)
                {
                    return DataSource.Stations[i];
                }
            }
            return new Station(0, "NO STATION FOUND", 0, 0, 0);
        }

        public Drone DisplayDrone(int id)
        {
            IsIdExist(DataSource.Drones, id);
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (DataSource.Drones[i].Id == id)
                {
                    return DataSource.Drones[i];
                }
            }
            return new Drone(0, "NO DRONE FOUND", (WeightCategories)0);
        }

        public Customer DisplayCustomer(int id)
        {
            IsIdExist(DataSource.Customers, id);
            for (int i = 0; i < DataSource.Customers.Count; i++)
            {
                if (DataSource.Customers[i].Id == id)
                {
                    return DataSource.Customers[i];
                }
            }
            return new Customer(0, "no customer found", "0000000000", 0, 0);
        }

        public Parcel DisplayParcel(int id)
        {
            IsIdExist(DataSource.Parcels, id);
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                if (DataSource.Parcels[i].Id == id)
                {
                    return DataSource.Parcels[i];
                }
            }
            return new Parcel(0, 0, 0, (WeightCategories)0, (Priorities)0, 0);
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

        public IEnumerable<Customer> customerList()
        {
            List<Customer> customerList = new List<Customer>();
            customerList = DataSource.Customers;
            return customerList;
        }

        public IEnumerable<Parcel> parcelList()
        {
            List<Parcel> parcelList = new List<Parcel>();
            parcelList = DataSource.Parcels;
            return parcelList;
        }

        public IEnumerable<Parcel> parcelListNotTaken()
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

        public IEnumerable<Station> freeStations()
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

    }
}