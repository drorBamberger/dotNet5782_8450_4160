using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{

    class DataSource
    {
        static internal Drone[] drones = new Drone[10];
        static internal Station[] stations = new Station[5];
        static internal Customer[] customers = new Customer[100];
        static internal Parcel[] parcels = new Parcel[1000];

        internal class Config
        {
            //folowing the first - not full place in every array
            internal static int freeDrone = 0;
            internal static int freeStation = 0;
            internal static int freeCustomer = 0;
            internal static int freeParcel = 0;

            //TODO CREATION OF SOMETHING THAT I DIDNT UNDERSTAND
        }
        public static void Initialize()
        {
            Random rnd = new Random();
            //stations initialization
            stations[0] = new Station(rnd.Next(), "London", -0.118092, 51.509865, 10);
            stations[1] = new Station(rnd.Next(), "Paris", 2.349014, 48.864716, 8);
            //customers initialization
            string[] customerNames = new string[] { "Dror", "Yair", "Ofir", "Gil", "Benaya", "Ohad", "Michael", "Achiya", "Drew", "Shilo" };
            for (int i = 0; i < 10; ++i)
            {
                customers[i] = new Customer(rnd.Next(), customerNames[i], "05" + (rnd.Next(10000000, 99999999)).ToString(), rnd.NextDouble() * 180 * Math.Pow(-1, rnd.Next(0, 1)), rnd.NextDouble() * 90 * Math.Pow(-1, rnd.Next(0, 2)));
            }

            //drones initialization
            for (int i = 0; i < 5; i++)
            {
                drones[5] = new Drone(rnd.Next(), "version" + i.ToString(), (WeightCategories)rnd.Next(0, 3), (DroneStatuses)rnd.Next(0, 3), rnd.NextDouble() * 100);
            }

            //pacels initialization
            for (int i = 0; i < 10; i++)
            {
                parcels[i] = new Parcel(rnd.Next(), rnd.Next(), customers[i].Id, (WeightCategories)rnd.Next(0, 3), new DateTime(2021, 5, i + 1, i * 2, i * 6, i + 8), new DateTime(2021, 5, i + 1, i * 2 + 3, i * 6, i + 8), new DateTime(2021, 5, i + 1, i * 2 + 1, i * 6, i + 8), new DateTime(2021, 5, i + 1, i * 2, i * 6, i + 20), (Priorities)rnd.Next(0, 3), rnd.Next());
            }
            
        }

    }

    public class DalObject
    {
        DateTime noDate  = new DateTime(0, 0, 0, 0, 0, 0);
        // cnstrct:
        public DalObject()
        {
            DataSource.Initialize();
        }

        //add option

        public void addStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            DataSource.stations[DataSource.Config.freeStation] = new Station(id, name, longitude, lattitude, chargeSlots);
            DataSource.Config.freeStation++;
        }

        public void addDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
        {
            DataSource.drones[DataSource.Config.freeDrone] = new Drone(id, model, maxWeight, status, battery);
            DataSource.Config.freeDrone++;
        }

        //displays
        public Station displayStation(int id)
        {
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if(DataSource.stations[i].Id == id)
                {
                    return DataSource.stations[i];
                }
            }
            return new Station(0, "NO STATION FOUND", 0, 0, 0);
        }

        public Drone displayDrone(int id)
        {
            for (int i = 0; i < DataSource.Config.freeDrone; i++)
            {
                if (DataSource.drones[i].Id == id)
                {
                    return DataSource.drones[i];
                }
            }
            return new Drone(0 ,"NO DRONE FOUND", (WeightCategories)0, (DroneStatuses)0, 0);
        }

        public Customer displayCustomer(int id)
        {
            for (int i = 0; i < DataSource.Config.freeCustomer; i++)
            {
                if (DataSource.customers[i].Id == id)
                {
                    return DataSource.customers[i];
                }
            }
            return new Customer(0, "no customer found", "0000000000", 0, 0);
        }

        public Parcel displayParcel(int id)
        {
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if (DataSource.parcels[i].Id == id)
                {
                    return DataSource.parcels[i];
                }
            }
            return new Parcel(0, 0, 0, (WeightCategories)0, noDate, noDate, noDate, noDate, (Priorities)0, 0);
        }

        //displays Lists

        public Station[] stationList()
        {
            Station[] StationList = new Station[DataSource.Config.freeStation];
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                StationList[i] = DataSource.stations[i];
            }
            return StationList;
        }

        public Drone[] droneList()
        {
            Drone[] DroneList = new Drone[DataSource.Config.freeDrone];
            for (int i = 0; i < DataSource.Config.freeDrone; i++)
            {
                DroneList[i] = DataSource.drones[i];
            }
            return DroneList;
        }

        public Customer[] customerList()
        {
            Customer[] CustomerList = new Customer[DataSource.Config.freeCustomer];
            for (int i = 0; i < DataSource.Config.freeCustomer; i++)
            {
                CustomerList[i] = DataSource.customers[i];
            }
            return CustomerList;
        }

        public Parcel[] parcelList()
        {
            Parcel[] ParcelList = new Parcel[DataSource.Config.freeParcel];
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                ParcelList[i] = DataSource.parcels[i];
            }
            return ParcelList;
        }

        public Parcel[] parcelListNotTaken()
        {
            int count = 0;
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if(DataSource.parcels[i].DroneId == 0)
                {
                    ++count;
                }
            }
            Parcel[] ParcelList = new Parcel[count];
            for (int i = 0; i < DataSource.Config.freeParcel; i++)
            {
                if (DataSource.parcels[i].DroneId == 0)
                {
                    ParcelList[i] = DataSource.parcels[i];
                }
            }
            return ParcelList;
        }

        public Station[] freeStations()
        {
            int count = 0;
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if (DataSource.stations[i].ChargeSlots != 0)
                {
                    ++count;
                }
            }
            Station[] StationList = new Station[count];
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if (DataSource.stations[i].ChargeSlots != 0)
                {
                    StationList[i] = DataSource.stations[i];
                }
            }
            return StationList;
        }

    }
}