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
            double battery;
            foreach (var item in Drones)
            {
                if(item.Id == id)
                {

                }
            }

                return new Drone(StructToClass.Id, StructToClass.Model, StructToClass.MaxWeight, )

        }
    }
}
