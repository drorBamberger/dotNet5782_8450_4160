using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {

        }

        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationId)
        {

        }

        public void AddCustomer(int id, string name, int phone, Location location)
        {

        }

        public void AddParcel(int senderId, int recieverId, WeightCategories maxWeight, Priorities priority)
        {

        }
    }
}
