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
            IDAL.DO.Station StructToClass =  MyDal.DisplayStation(id);
            List<DroneInCharge> DronesInStation = new List<DroneInCharge>;
            foreach(var item in Drones)
            {
                if(item.MyLocation.Longitude == StructToClass.Longitude && item.MyLocation.Latitude == StructToClass.Lattitude)
                {
                    DronesInStation.Add(new DroneInCharge(item.Id, item.Battery));
                }
            }
            return new Station(StructToClass.Id, StructToClass.Name, new Location(StructToClass.Longitude, StructToClass.Lattitude), 
                StructToClass.ChargeSlots, DronesInStation);
        }
    }

}
