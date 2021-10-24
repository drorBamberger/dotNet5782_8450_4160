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
                customers[i] = new Customer(rnd.Next(), customerNames[i], "05" + (rnd.Next(10000000, 99999999)).ToString(), rnd.NextDouble() * 180 * Math.Pow(-1, rnd.Next(0, 1)), rnd.NextDouble() * 90 * Math.Pow(-1, rnd.Next(0, 1));
            }

            //drones initialization
            for (int i = 0; i < 5; i++)
            {
                drones[5] = new Drone(rnd.Next(), "version" + i.ToString(), (WeightCategories)rnd.Next(0, 3), (DroneStatuses)rnd.Next(0, 3), rnd.NextDouble() * 100);
            }

            //pacels initialization
            for (int i = 0; i < 10; i++)
            {
                parcels[i] = new Parcel(rnd.Next(), rnd.Next(), customers[i].Id, rnd.Next(0, 2), new DateTime(2021, 5, i + 1, i * 2, i * 6, i + 8), new DateTime(2021, 5, i + 1, i * 2 + 3, i * 6, i + 8), new DateTime(2021, 5, i + 1, i * 2 + 1, i * 6, i + 8), new DateTime(2021, 5, i + 1, i * 2, i * 6, i + 20), rnd.Next(0, 2), rnd.Next());
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

        //displays
        public Station displayStation(int id)
        {
            for (int i = 0; i < DataSource.Config.freeStation; i++)
            {
                if(DataSource.stations[i].Id == id)
                {

                }
            }
        }

    }
}