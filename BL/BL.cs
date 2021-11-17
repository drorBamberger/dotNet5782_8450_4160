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
            Random rnd = new Random();
            GetElecticity();
            var dalDrones = MyDal.DroneList();
            int parcelIndex = 0;
            IDAL.DO.Parcel myParcel;
            Location droneLocation = new Location(0,0);
            Location senderLocation, targetLocation;
            foreach(var drone in dalDrones)
            {
                parcelIndex = ((List<IDAL.DO.Parcel>)MyDal.ParcelList()).FindIndex(x => x.DroneId == drone.Id && x.Delivered == DateTime.MinValue);
                
                if (parcelIndex != -1)
                {
                    myParcel = ((List<IDAL.DO.Parcel>)(MyDal.ParcelList()))[parcelIndex];
                    senderLocation = new Location(MyDal.DisplayCustomer(myParcel.SenderId).Longitude,
                            MyDal.DisplayCustomer(myParcel.SenderId).Longitude);
                    targetLocation = new Location(MyDal.DisplayCustomer(myParcel.TargetId).Longitude,
                            MyDal.DisplayCustomer(myParcel.TargetId).Longitude);
                    double minimumBatteryNeeded = 0;
                    if (myParcel.PickedUp == DateTime.MinValue)
                    {
                        droneLocation = senderLocation;
                        IDAL.DO.Station closestStation = GetClosestStation(targetLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                        minimumBatteryNeeded = GetElectricityPerKM(DistanceTo(droneLocation, targetLocation), (WeightCategories)myParcel.Weight) +
                            DistanceTo(targetLocation, new Location(closestStation.Longitude, closestStation.Lattitude))*Available;
                    }
                    else
                    {
                        IDAL.DO.Station closestStation = GetClosestStation(senderLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                        droneLocation = new Location(closestStation.Longitude, closestStation.Lattitude);
                    }

                    Drones.Add(new DroneForList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,
                        rnd.NextDouble()*(100- minimumBatteryNeeded)+ minimumBatteryNeeded//(nextDouble between 0 - 1)*(between 0 - 100-minimumBatteryNeeded)*(between minimumBatteryNeeded - 100)
                        , DroneStatuses.Shipping, myParcel.Id, droneLocation));
                }
            }

        }

        
    }

}
