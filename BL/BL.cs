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

        //display
        public Station DisplayStation(int id)
        {
            return new Station((MyDal.DisplayStation(id)).Id, MyDal.DisplayStation(id).Name, 
                new Location(MyDal.DisplayStation(id).Longitude, MyDal.DisplayStation(id).Lattitude), MyDal.DisplayStation(id).ChargeSlots, )
        }
    }

}
