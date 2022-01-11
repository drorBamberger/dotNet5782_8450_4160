using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
using DalObject;

namespace BL
{
    
    public partial class BL : BLApi.IBL
    {
        private static BL instance = null;

        internal static BL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BL();
                }
                return instance;
            }
        }
        List<DroneForList> Drones = new List<DroneForList>();
        static DalApi.IDal MyDal;
        public double Available;
        public double SmallPackege;
        public double MediumPackege;
        public double HeavyPackege;
        public double ChargePerHour;

        public BL()
        {
            MyDal = DAL.DalFactory.GetDal("DalXml");
            Random rnd = new Random();
            GetElecticity();
            var dalDrones = MyDal.DroneList(x=>true);
            int parcelIndex = 0;
            DO.Parcel myParcel;
            Location droneLocation = new Location(0, 0);
            Location senderLocation, targetLocation;
            foreach (var drone in dalDrones)
            {
                parcelIndex = ((List<DO.Parcel>)MyDal.ParcelList(x=>true)).FindIndex(x => x.DroneId == drone.Id && x.Delivered == null);
                double minimumBatteryNeeded = 0;

                if (parcelIndex != -1)
                {
                    myParcel = ((List<DO.Parcel>)(MyDal.ParcelList(x=>true)))[parcelIndex];
                    senderLocation = new Location(MyDal.DisplayCustomer(myParcel.SenderId).Longitude,
                            MyDal.DisplayCustomer(myParcel.SenderId).Lattitude);
                    targetLocation = new Location(MyDal.DisplayCustomer(myParcel.TargetId).Longitude,
                            MyDal.DisplayCustomer(myParcel.TargetId).Lattitude);
                    if (myParcel.PickedUp != null)
                    {
                        droneLocation = senderLocation;
                        DO.Station closestStation = GetClosestStation(targetLocation, (List<DO.Station>)MyDal.StationList(x=>true));
                        minimumBatteryNeeded = GetElectricityPerKM(DistanceTo(droneLocation, targetLocation), (WeightCategories)myParcel.Weight) +
                            DistanceTo(targetLocation, new Location(closestStation.Longitude, closestStation.Lattitude)) * Available;
                    }
                    
                    else
                    {
                        DO.Station closestStation = GetClosestStation(senderLocation, (List<DO.Station>)MyDal.StationList(x=>true));
                        droneLocation = new Location(closestStation.Longitude, closestStation.Lattitude);
                        closestStation = GetClosestStation(targetLocation, (List<DO.Station>)MyDal.StationList(x => true));
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
                        var parcelList = ((List<DO.Parcel>)MyDal.ParcelList(x => true));
                        parcelList = parcelList.FindAll(x => x.Delivered != null);
                        if (parcelList.Count() == 0)
                        {
                            Drones.Add(new DroneForList(drone.Id, drone.Model, (WeightCategories)drone.MaxWeight,
                            100, DroneStatuses.vacant, 0,
                            new Location(MyDal.StationList(x => true).First().Longitude, MyDal.StationList(x => true).First().Lattitude)));
                        }
                        else
                        {
                            int customerId = parcelList[rnd.Next(0, parcelList.Count())].TargetId;
                            var myCustomer = MyDal.DisplayCustomer(customerId);
                            Location customerLocation = new Location(myCustomer.Longitude, myCustomer.Lattitude);
                            var closestStation = GetClosestStation(customerLocation, (List<DO.Station>)MyDal.StationList(x => true));
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
                        List<DO.Station> stationList = (List<DO.Station>)(MyDal.StationList(x => true));
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
