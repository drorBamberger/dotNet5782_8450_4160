using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{

    public partial class BL : IBL.IBL
    {
        List<DroneForList> Drones = new List<DroneForList>();
        static DalObject.DalObject MyDal;
        public double Available;
        public double SmallPackege;
        public double MediumPackege;
        public double HeavyPackege;
        public double ChargePerHour;
        public BL()
        {
            MyDal = new DalObject.DalObject();
            Random rnd = new Random();
            GetElecticity();
            var dalDrones = MyDal.DroneList();
            int parcelIndex = 0;
            IDAL.DO.Parcel myParcel;
            Location droneLocation = new Location(0, 0);
            Location senderLocation, targetLocation;
            foreach (var drone in dalDrones)
            {
                parcelIndex = ((List<IDAL.DO.Parcel>)MyDal.ParcelList()).FindIndex(x => x.DroneId == drone.Id && x.Delivered == DateTime.MinValue);
                double minimumBatteryNeeded = 0;

                if (parcelIndex != -1)
                {
                    myParcel = ((List<IDAL.DO.Parcel>)(MyDal.ParcelList()))[parcelIndex];
                    senderLocation = new Location(MyDal.DisplayCustomer(myParcel.SenderId).Longitude,
                            MyDal.DisplayCustomer(myParcel.SenderId).Lattitude);
                    targetLocation = new Location(MyDal.DisplayCustomer(myParcel.TargetId).Longitude,
                            MyDal.DisplayCustomer(myParcel.TargetId).Lattitude);
                    if (myParcel.PickedUp != DateTime.MinValue)
                    {
                        droneLocation = senderLocation;
                        IDAL.DO.Station closestStation = GetClosestStation(targetLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                        minimumBatteryNeeded = GetElectricityPerKM(DistanceTo(droneLocation, targetLocation), (WeightCategories)myParcel.Weight) +
                            DistanceTo(targetLocation, new Location(closestStation.Longitude, closestStation.Lattitude)) * Available;
                    }
                    else
                    {
                        IDAL.DO.Station closestStation = GetClosestStation(senderLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                        droneLocation = new Location(closestStation.Longitude, closestStation.Lattitude);
                        closestStation = GetClosestStation(targetLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                        minimumBatteryNeeded = DistanceTo(droneLocation, senderLocation) * Available +
                            GetElectricityPerKM(DistanceTo(senderLocation, targetLocation), (WeightCategories)myParcel.Weight) +
                            DistanceTo(targetLocation, new Location(closestStation.Longitude, closestStation.Lattitude)) * Available;
                    }

                    Drones.Add(new DroneForList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,
                        rnd.NextDouble() * (100 - minimumBatteryNeeded) + minimumBatteryNeeded//(nextDouble between 0 - 1)*(between 0 - 100-minimumBatteryNeeded)+(between minimumBatteryNeeded - 100)
                        , DroneStatuses.Shipping, myParcel.Id, droneLocation));



                }
                else
                {
                    DroneStatuses status = (DroneStatuses)rnd.Next(0, 2);
                    if (status == DroneStatuses.vacant)
                    {
                        var parcelList = ((List<IDAL.DO.Parcel>)MyDal.ParcelList());
                        parcelList = parcelList.FindAll(x => x.Delivered != DateTime.MinValue);
                        if (parcelList.Count() == 0)
                        {
                            Drones.Add(new DroneForList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,
                            100, DroneStatuses.vacant, 0,
                            new Location(MyDal.StationList().First().Longitude, MyDal.StationList().First().Lattitude)));
                        }
                        else
                        {
                            int customerId = parcelList[rnd.Next(0, parcelList.Count())].TargetId;
                            var myCustomer = MyDal.DisplayCustomer(customerId);
                            Location customerLocation = new Location(myCustomer.Longitude, myCustomer.Lattitude);
                            var closestStation = GetClosestStation(customerLocation, (List<IDAL.DO.Station>)MyDal.StationList());
                            double distance = DistanceTo(customerLocation,
                                new Location(closestStation.Longitude, closestStation.Lattitude));
                            minimumBatteryNeeded = distance * Available;
                            Drones.Add(new DroneForList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,
                                rnd.NextDouble() * (100 - minimumBatteryNeeded) + minimumBatteryNeeded, DroneStatuses.vacant, 0,
                                customerLocation));
                        }
                    }
                    else
                    {
                        List<IDAL.DO.Station> stationList = (List<IDAL.DO.Station>)(MyDal.StationList());
                        Drones.Add(new DroneForList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,
                            rnd.NextDouble() * 20, DroneStatuses.maintenance, 0,
                            new Location(stationList[rnd.Next(0, stationList.Count())].Longitude,
                            stationList[rnd.Next(0, stationList.Count())].Lattitude)));
                    }
                }
            }

        }
    }

}
