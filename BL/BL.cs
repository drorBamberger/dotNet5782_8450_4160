using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{

    public partial class BL : IBL.IBL
    {
        List<DroneForList> Drones;
        IDAL.IDal MyDal;
        public double Available;
        public double SmallPackege;
        public double MediumPackege;
        public double HeavyPackege;
        public double ChargePerHour;
        public BL()
        {
            MyDal = new DalObject.DalObject();
            GetElecticity();



        }

        //aads
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {

        }

        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationId)
        {

        }

        public void AddCustomer(int id, string name,  int phone, Location location)
        {

        }

        public void AddParcel(int senderId, int recieverId, WeightCategories maxWeight, Priorities priority)
        {

        }

        
    }

}
