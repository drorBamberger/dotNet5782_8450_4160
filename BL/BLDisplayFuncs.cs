using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        //displays
        public Station DisplayStation(int id)
        {
            IDAL.DO.Station StructToClass = MyDal.DisplayStation(id);
            List<DroneInCharge> DronesInStation = new List<DroneInCharge> { };
            foreach (var item in Drones)
            {
                if (item.MyLocation.Longitude == StructToClass.Longitude && item.MyLocation.Latitude == StructToClass.Lattitude)
                {
                    DronesInStation.Add(new DroneInCharge(item.Id, item.Battery));
                }
            }
            return new Station(StructToClass.Id, StructToClass.Name, new Location(StructToClass.Longitude, StructToClass.Lattitude),
                StructToClass.ChargeSlots, DronesInStation);
        }

        public Drone DisplayDrone(int id)
        {
            IDAL.DO.Drone StructToClass = MyDal.DisplayDrone(id);
            double battery =0;
            DroneStatuses status = 0;
            ParcelOnDelivery parcel;
            Location loc = new Location(0, 0);
            foreach (var item in MyDal.ParcelList)
            {

            }
            foreach (var item in Drones)
            {
                if(item.Id == id)
                {
                    battery = item.Battery;
                    status = item.Status;
                    loc = item.MyLocation;
                    break;
                }
            }

            return new Drone(StructToClass.Id, StructToClass.Model, StructToClass.MaxWeight, battery, status,  , loc);

        }
    }
}
