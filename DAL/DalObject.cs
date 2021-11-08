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
            //folowing the first - not full place in every array

            internal static int runningField = 1;
            //TODO CREATION OF SOMETHING THAT I DIDNT UNDERSTAND
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
                Parcels.Add(new Parcel(Config.runningField++, Customers[rnd.Next(0, 10)].Id, Customers[rnd.Next(0, 10)].Id, 
                   (WeightCategories)rnd.Next(0, 3),  (Priorities)rnd.Next(0, 3), Drones[rnd.Next(0, 5)].Id));
            }
            
        }

    }

    public class DalObject
    {
        // cnstrct:
        public DalObject()
        {
            DataSource.Initialize();
        }

        //add option

        public void addStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            DataSource.Stations.Add(new Station(id, name, longitude, lattitude, chargeSlots));
        }

        public void addDrone(int id, string model, int maxWeight)
        {
            DataSource.Drones[DataSource.Config.freeDrone] = new Drone(id, model, (WeightCategories)maxWeight);
        }

        public void addCustomer(int id, string name, string phone, double longitude, double lattitude)
        {
            DataSource.Customers[DataSource.Config.freeCustomer] = new Customer(id, name, phone, longitude, lattitude);
            DataSource.Config.freeCustomer++;
        }

        public void addParcel(int senderId, int targetId, int weight, int priority, int droneId)
        {
            DataSource.Parcels[DataSource.Config.freeParcel] = new Parcel(DataSource.Config.runningField++, senderId, targetId,(WeightCategories)weight, (Priorities)priority, droneId);
            DataSource.Config.freeParcel++;
        }
        //update options

        public void attribution(int droneId, int parcelId)
        {

            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if (DataSource.Parcels[i].Id == parcelId)
                {
                    DataSource.Parcels[i].DroneId = droneId;
                    DataSource.Parcels[i].Scheduled = DateTime.Now;
                }
            }
        }

        public void PickedParcelUp(int parcelId)
        {
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if (DataSource.Parcels[i].Id == parcelId)
                {
                    DataSource.Parcels[i].PickedUp = DateTime.Now;
                }
            }
        }

        public void ParcelDelivered(int parcelId)
        {
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if (DataSource.Parcels[i].Id == parcelId)
                {
                    DataSource.Parcels[i].Delivered = DateTime.Now;
                }
            }
        }

        public void ChargeDrone(int droneId, int stationId)
        {
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if (DataSource.Stations[i].Id == stationId)
                {
                    DataSource.Charging[DataSource.Config.freeCharging++].DroneId = droneId;
                    DataSource.Charging[DataSource.Config.freeCharging].StationId = stationId;
                    DataSource.Stations[i].ChargeSlots--;
                }
            }
        }

        public void DisChargeDrone(int droneId, int stationId)
        {
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if (DataSource.Stations[i].Id == stationId)
                {
                    DataSource.Config.freeCharging--;
                    DataSource.Stations[i].ChargeSlots++;
                }
            }
        }


        //displays
        public Station displayStation(int id)
        {
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if(DataSource.Stations[i].Id == id)
                {
                    return DataSource.Stations[i];
                }
            }
            return new Station(0, "NO STATION FOUND", 0, 0, 0);
        }

        public Drone displayDrone(int id)
        {
            for (int i = 0; i < DataSource.Config.freeDrone; i++)
            {
                if (DataSource.Drones[i].Id == id)
                {
                    return DataSource.Drones[i];
                }
            }
            return new Drone(0 ,"NO DRONE FOUND", (WeightCategories)0);
        }

        public Customer displayCustomer(int id)
        {
            for (int i = 0; i < DataSource.Config.freeCustomer; i++)
            {
                if (DataSource.Customers[i].Id == id)
                {
                    return DataSource.Customers[i];
                }
            }
            return new Customer(0, "no customer found", "0000000000", 0, 0);
        }

        public Parcel displayParcel(int id)
        {
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if (DataSource.Parcels[i].Id == id)
                {
                    return DataSource.Parcels[i];
                }
            }
            return new Parcel(0, 0, 0, (WeightCategories)0, (Priorities)0, 0);
        }

        //displays Lists

        public Station[] stationList()
        {
            Station[] StationList = new Station[DataSource.Config.freeStation];
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                StationList[i] = DataSource.Stations[i];
            }
            return StationList;
        }

        public Drone[] droneList()
        {
            Drone[] DroneList = new Drone[DataSource.Config.freeDrone];
            for (int i = 0; i < DataSource.Config.freeDrone; i++)
            {
                DroneList[i] = DataSource.Drones[i];
            }
            return DroneList;
        }

        public Customer[] customerList()
        {
            Customer[] CustomerList = new Customer[DataSource.Config.freeCustomer];
            for (int i = 0; i < DataSource.Config.freeCustomer; i++)
            {
                CustomerList[i] = DataSource.Customers[i];
            }
            return CustomerList;
        }

        public Parcel[] parcelList()
        {
            Parcel[] ParcelList = new Parcel[DataSource.Config.freeParcel];
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                ParcelList[i] = DataSource.Parcels[i];
            }
            return ParcelList;
        }

        public Parcel[] parcelListNotTaken()
        {
            int count = 0;
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if(DataSource.Parcels[i].DroneId == 0)
                {
                    ++count;
                }
            }
            Parcel[] ParcelList = new Parcel[count];
            count = 0;
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if (DataSource.Parcels[i].DroneId == 0)
                {
                    ParcelList[count++] = DataSource.Parcels[i];
                }
            }
            return ParcelList;
        }

        public Station[] freeStations()
        {
            int count = 0;
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if (DataSource.Stations[i].ChargeSlots != 0)
                {
                    ++count;
                }
            }
            Station[] StationList = new Station[count];
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if (DataSource.Stations[i].ChargeSlots != 0)
                {
                    StationList[i] = DataSource.Stations[i];
                }
            }
            return StationList;
        }

    }
}