using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{

    public partial class BL
    {
        List<DroneForList> Drones;
        IDAL.IDal MyDal;
        public double Available;
        public double SmallPackege;
        public double MediumPackege;
        public double HeavyPackege;
        public double ChargePerHour ;
        public BL()
        {
            MyDal = new DalObject.DalObject();
            GetElecticity();
            


        }
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {

        }
    }

}
