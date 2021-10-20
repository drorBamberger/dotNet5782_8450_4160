using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{

    class DataSource
    {
        internal Drone[] drones = new Drone[10];
        internal Station[] stations = new Station[5];
        internal Customer[] customers = new Customer[100];
        internal Parcel[] parcels = new Parcel[1000];

        internal class config
        {
            //folowing the first - not full place in every array
            internal static int freeDrone = 0;
            internal static int freeStation = 0;
            internal static int freeCustomer = 0;
            internal static int freeParcel = 0;


        }


    }

}
