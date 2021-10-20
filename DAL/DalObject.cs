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
        static void Initialize()
        {
            Random rnd = new Random();
            //stations initialization
            stations[0] = new Station(rnd.Next(), "London", -0.118092, 51.509865, 10);
            stations[1] = new Station(rnd.Next(), "Paris", 2.349014, 48.864716, 8);
            //customers initialization
            string [] customerNames = new string[] {"Dror", "Yair", "Ofir", "Gil", "Benaya" , "Ohad", "Michael", "Achiya", "Drew", "Shilo"}
            for (int i = 0; i < 10;++i)
            {
                customers[i] = new Customer(rnd.Next(), customerNames[i], "05" + (rnd.Next(10000000, 99999999)).ToString(), )
            }


        }

    }

}
