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
            GetElecticity();
            var dalDrones = MyDal.DroneList();
            int parcelIndex = 0;
            IDAL.DO.Parcel myParcel;
            Location droneLocation, senderLocation;
            foreach(var drone in dalDrones)
            {
                parcelIndex = ((List<IDAL.DO.Parcel>)MyDal.ParcelList()).FindIndex(x => x.DroneId == drone.Id && x.Delivered == DateTime.MinValue);
                
                if (parcelIndex != -1)
                {
                    myParcel = ((List<IDAL.DO.Parcel>)(MyDal.ParcelList()))[parcelIndex];
                    senderLocation = new Location(MyDal.DisplayCustomer(myParcel.SenderId).Longitude,
                            MyDal.DisplayCustomer(myParcel.SenderId).Longitude);
                    if (myParcel.PickedUp == DateTime.MinValue)
                    {
                        droneLocation = senderLocation;
                    }
                    else
                    {
                        //finding the closest station
                        double smallDistance = int.MaxValue, tmpDis;
                        foreach(var station in MyDal.StationList())
                        {
                            Location stationLocation = new Location(station.Longitude, station.Lattitude);
                            tmpDis = DistanceTo(senderLocation, stationLocation);
                            if (tmpDis<smallDistance)
                            {
                                smallDistance = tmpDis;
                                droneLocation = stationLocation;
                            }
                        }
                    }
                    Drones.Add(new DroneForList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,
                        , DroneStatuses.Shipping, myParcel.Id, droneLocation));
                }
            }

        }

        
    }

}
