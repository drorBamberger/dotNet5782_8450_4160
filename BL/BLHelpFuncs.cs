using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        internal void GetElecticity()
        {
            double[] tmp = new double[5];
            tmp = MyDal.AskForElectricity();
            Available     = tmp[0];
            SmallPackege  = tmp[1];
            MediumPackege = tmp[2];
            HeavyPackege  = tmp[3];
            ChargePerHour = tmp[4];
        }

        internal double DistanceTo(Location x1, Location x2, char unit = 'K')
        {
            double rlat1 = Math.PI * x1.Latitude / 180;
            double rlat2 = Math.PI * x2.Latitude / 180;
            double theta = x1.Longitude - x2.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }
        //gets--------------------------------------------------------------
        internal Station GetStation(int id)
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
            return new Station(id, StructToClass.Name, new Location(StructToClass.Longitude, StructToClass.Lattitude),
                StructToClass.ChargeSlots, DronesInStation);
        }

        internal Drone GetDrone(int id)
        {
            IDAL.DO.Drone StructToClass = MyDal.DisplayDrone(id);
            double battery = 0;
            DroneStatuses status = 0;
            Location DroneLocation = new Location(0, 0);
            foreach (var item in Drones)
            {
                if (item.Id == id)
                {
                    battery = item.Battery;
                    status = item.Status;
                    DroneLocation = item.MyLocation;
                    break;
                }
            }
            
            return new Drone(id, StructToClass.Model, (WeightCategories)StructToClass.MaxWeight,
                battery, status, GetParcelOnDelivery(id), DroneLocation);
        }

        internal Customer GetCustomer(int id)
        {
            IDAL.DO.Customer StructToClass = MyDal.DisplayCustomer(id);
            List<ParcelForCustomer> fromCustomers = new List<ParcelForCustomer>();
            List<ParcelForCustomer> toCustomers = new List<ParcelForCustomer>();
            foreach(var item in MyDal.ParcelList())
            {
                if(item.SenderId == id)
                {
                    fromCustomers.Add(GetParcelForCustomer(item.SenderId, new CustomerInParcel(item.TargetId, MyDal.DisplayCustomer(item.TargetId).Name)));
                }
                if (item.TargetId == id)
                {
                    toCustomers.Add(GetParcelForCustomer(item.TargetId, new CustomerInParcel(item.SenderId, MyDal.DisplayCustomer(item.SenderId).Name)));
                }
            }

            return new Customer(id, StructToClass.Name, StructToClass.Phone, new Location(StructToClass.Longitude, StructToClass.Lattitude),
                fromCustomers, toCustomers);
                
        }

        internal Parcel GetParcel(int id)
        {
            IDAL.DO.Parcel StructToClass = MyDal.DisplayParcel(id);


            return new Parcel(id, new CustomerInParcel(StructToClass.SenderId, MyDal.DisplayCustomer(StructToClass.SenderId).Name),
                new CustomerInParcel(StructToClass.TargetId, MyDal.DisplayCustomer(StructToClass.TargetId).Name), (WeightCategories)StructToClass.Weight,
                (Priorities)StructToClass.Priority, GetDroneInParcel(id), StructToClass.Reuqested, StructToClass.Scheduled, StructToClass.PickedUp, StructToClass.Delivered);
        }
        internal ParcelOnDelivery GetParcelOnDelivery(int droneId)
        {
            IDAL.DO.Parcel parcel = MyDal.ParcelList().First(p => p.DroneId == droneId);
            IDAL.DO.Customer sender = MyDal.DisplayCustomer(parcel.SenderId);
            IDAL.DO.Customer target = MyDal.DisplayCustomer(parcel.TargetId);
            Location SenderLocation = new Location(sender.Longitude, sender.Lattitude);
            Location TargetLocation = new Location(target.Longitude, target.Lattitude);
            CustomerInParcel senderInParcel = new CustomerInParcel(sender.Id, sender.Name);
            CustomerInParcel targetInParcel = new CustomerInParcel(sender.Id, sender.Name);

            return new ParcelOnDelivery(parcel.Id, ParcelOnDeliveryStatuses.onTheWay,
                (WeightCategories)parcel.Weight, (Priorities)parcel.Priority, senderInParcel, targetInParcel, SenderLocation,
                TargetLocation, DistanceTo(SenderLocation, TargetLocation));
        }

        internal ParcelForCustomer GetParcelForCustomer(int id, CustomerInParcel otherSide)
        {
            IDAL.DO.Parcel StructToClass = MyDal.DisplayParcel(id);
            return new ParcelForCustomer(id, (WeightCategories)StructToClass.Weight, (Priorities)StructToClass.Priority,
                GetParcelStatus(id), otherSide);
        }
        internal DroneInParcel GetDroneInParcel(int parcelId)
        {
            DroneForList myDrone = Drones.Find(x => x.ParcelId == parcelId);
            return new DroneInParcel(myDrone.Id, myDrone.Battery, myDrone.MyLocation);
        }

        internal ParcelStatuses GetParcelStatus(int id)
        {
            IDAL.DO.Parcel StructToClass = MyDal.DisplayParcel(id);
            ParcelStatuses status = new ParcelStatuses();
            if (StructToClass.Delivered != DateTime.MinValue)
                status = ParcelStatuses.Delivered;
            else if (StructToClass.PickedUp != DateTime.MinValue)
                status = ParcelStatuses.PickedUp;
            else if (StructToClass.Scheduled != DateTime.MinValue)
                status = ParcelStatuses.Scheduled;
            else
                status = ParcelStatuses.Reuqested;

            return status;
        }

        internal IDAL.DO.Station GetClosestStation(Location locationA, List<IDAL.DO.Station> stations)
        {
            double smallDistance = double.MaxValue, tmpDis;
            IDAL.DO.Station comeBack = new IDAL.DO.Station();
            foreach (var station in stations)
            {
                Location stationLocation = new Location(station.Longitude, station.Lattitude);
                tmpDis = DistanceTo(locationA, stationLocation);
                if (tmpDis < smallDistance)
                {
                    smallDistance = tmpDis;
                    comeBack = station;
                }
            }
            return comeBack;
        }

        internal double GetElectricityPerKM(double distance, WeightCategories a = WeightCategories.Light)
        {
            if(a == WeightCategories.Light)
                return SmallPackege;
            if (a == WeightCategories.Medium)
                return MediumPackege;
            return HeavyPackege;

        }
        internal IDAL.DO.Station GetClosestStationWithChargeSlots(Location locationA, List<IDAL.DO.Station> stations)
        {
            var closestStation = GetClosestStation(locationA, stations);
            while(closestStation.ChargeSlots == 0)
            {
                stations.Remove(closestStation);
                closestStation = GetClosestStation(locationA, stations);
            }
            return closestStation;
        }
    }
}
