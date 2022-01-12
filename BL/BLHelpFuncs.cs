using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : BLApi.IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            DO.Station StructToClass;
            try
            {
                StructToClass = MyDal.DisplayStation(id);
            }
            catch(DO.IdNotExistException err)
            {
                throw new BO.IdNotExistException(err.Id);
            }
            List<DroneInCharge> DronesInStation = new List<DroneInCharge> { };
            foreach (var item in Drones)
            {
                if (StructToClass.Longitude == item.MyLocation.Longitude && StructToClass.Lattitude == item.MyLocation.Latitude
                    && item.Status == DroneStatuses.maintenance)
                {
                    DronesInStation.Add(new DroneInCharge(item.Id, item.Battery));
                }
            }
            return new Station(id, StructToClass.Name, new Location(StructToClass.Longitude, StructToClass.Lattitude),
                StructToClass.ChargeSlots, DronesInStation);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            DO.Drone StructToClass;
            try
            {
                StructToClass = MyDal.DisplayDrone(id);
            }
            catch (DO.IdNotExistException err)
            {
                throw new BO.IdNotExistException(err.Id);
            }
            var drone = Drones.Find(item => item.Id == id);
            return new Drone(id, StructToClass.Model, (WeightCategories)StructToClass.MaxWeight,
                drone.Battery, drone.Status, GetParcelOnDelivery(id), drone.MyLocation);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneForList GetDroneForList(int id)
        {
            Drone drone = GetDrone(id);
            return new DroneForList(id, drone.Model, drone.MaxWeight, drone.Battery, drone.Status, drone.MyParcel == null?0:drone.MyParcel.Id, drone.MyLocation);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            lock (MyDal)
            {
                DO.Customer StructToClass;
                try
                {
                    StructToClass = MyDal.DisplayCustomer(id);
                }
                catch (DO.IdNotExistException err)
                {
                    throw new BO.IdNotExistException(err.Id);
                }
                List<ParcelForCustomer> fromCustomers = new List<ParcelForCustomer>();
                List<ParcelForCustomer> toCustomers = new List<ParcelForCustomer>();
                foreach (var item in MyDal.ParcelList(x => true))
                {
                    if (item.SenderId == id)
                    {
                        fromCustomers.Add(GetParcelForCustomer(item.Id, new CustomerInParcel(item.TargetId, MyDal.DisplayCustomer(item.TargetId).Name)));
                    }
                    if (item.TargetId == id)
                    {
                        toCustomers.Add(GetParcelForCustomer(item.Id, new CustomerInParcel(item.SenderId, MyDal.DisplayCustomer(item.SenderId).Name)));
                    }
                }


                return new Customer(id, StructToClass.Name, StructToClass.Phone, new Location(StructToClass.Longitude, StructToClass.Lattitude),
                    fromCustomers, toCustomers);
            }
                
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            lock (MyDal)
            {
                DO.Parcel StructToClass;
                try
                {
                    StructToClass = MyDal.DisplayParcel(id);
                }
                catch (DO.IdNotExistException err)
                {
                    throw new BO.IdNotExistException(err.Id);
                }

                return new Parcel(id, new CustomerInParcel(StructToClass.SenderId, MyDal.DisplayCustomer(StructToClass.SenderId).Name),
                    new CustomerInParcel(StructToClass.TargetId, MyDal.DisplayCustomer(StructToClass.TargetId).Name), (WeightCategories)StructToClass.Weight,
                    (Priorities)StructToClass.Priority, GetDroneInParcel(id), StructToClass.Reuqested,
                    StructToClass.Scheduled, StructToClass.PickedUp, StructToClass.Delivered);
            }
        }
        internal ParcelOnDelivery GetParcelOnDelivery(int droneId)
        {
            if (!Drones.Exists(x => x.Id == droneId))
            {
                throw new BO.IdNotExistException(droneId);
            }
            if(Drones.Find(x=>x.Id == droneId).ParcelId == 0)
            {
                return null;
            }
            lock (MyDal)
            {
                DO.Parcel parcel = MyDal.DisplayParcel(Drones.Find(x => x.Id == droneId).ParcelId);
                DO.Customer sender = MyDal.DisplayCustomer(parcel.SenderId);
                DO.Customer target = MyDal.DisplayCustomer(parcel.TargetId);

                Location SenderLocation = GetParcelSenderLocation(parcel.Id);
                Location TargetLocation = GetParcelTargetLocation(parcel.Id);
                CustomerInParcel senderInParcel = new CustomerInParcel(sender.Id, sender.Name);
                CustomerInParcel targetInParcel = new CustomerInParcel(target.Id, target.Name);

                return new ParcelOnDelivery(parcel.Id, ParcelOnDeliveryStatuses.onTheWay,
                    (WeightCategories)parcel.Weight, (Priorities)parcel.Priority, senderInParcel, targetInParcel, SenderLocation,
                    TargetLocation, DistanceTo(SenderLocation, TargetLocation));
            }
        }

        internal ParcelForCustomer GetParcelForCustomer(int id, CustomerInParcel otherSide)
        {
            DO.Parcel StructToClass = MyDal.DisplayParcel(id);
            return new ParcelForCustomer(id, (WeightCategories)StructToClass.Weight, (Priorities)StructToClass.Priority,
                GetParcelStatus(id), otherSide);
        }
        internal DroneInParcel GetDroneInParcel(int parcelId)
        {
            if(Drones.FindIndex(x=>x.ParcelId == parcelId) == -1)
            {
                return default;
            }
            DroneForList myDrone = Drones.Find(x => x.ParcelId == parcelId);
            return new DroneInParcel(myDrone.Id, myDrone.Battery, myDrone.MyLocation);
        }

        internal ParcelStatuses GetParcelStatus(int id)
        {
            DO.Parcel StructToClass = MyDal.DisplayParcel(id);
            ParcelStatuses status = new ParcelStatuses();
            if (StructToClass.Delivered != null)
                status = ParcelStatuses.Delivered;
            else if (StructToClass.PickedUp != null)
                status = ParcelStatuses.PickedUp;
            else if (StructToClass.Scheduled != null)
                status = ParcelStatuses.Scheduled;
            else
                status = ParcelStatuses.Reuqested;

            return status;
        }

        internal DO.Station GetClosestStation(Location locationA, List<DO.Station> stations)
        {
            double smallDistance = double.MaxValue, tmpDis;
            DO.Station comeBack = new DO.Station();
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

        internal DO.Parcel GetClosestParcel(Location locationA, List<DO.Parcel> parcels)
        {
            lock (MyDal)
            {
                double smallDistance = double.MaxValue, tmpDis;
                DO.Parcel comeBack = new DO.Parcel();
                foreach (var parcel in parcels)
                {
                    Location parcelLocation = new Location(MyDal.DisplayCustomer(parcel.SenderId).Longitude,
                        MyDal.DisplayCustomer(parcel.SenderId).Longitude);
                    tmpDis = DistanceTo(locationA, parcelLocation);
                    if (tmpDis < smallDistance)
                    {
                        smallDistance = tmpDis;
                        comeBack = parcel;
                    }
                }
                return comeBack;
            }
        }


        internal double GetElectricityPerKM(double distance, WeightCategories a)
        {
            if(a == WeightCategories.Light)
                return SmallPackege*distance;
            if (a == WeightCategories.Medium)
                return MediumPackege*distance;
            return HeavyPackege*distance;

        }
        internal DO.Station GetClosestStationWithChargeSlots(Location locationA, List<DO.Station> stations)
        {
            var closestStation = GetClosestStation(locationA, stations);
            while(closestStation.ChargeSlots == 0)
            {
                stations.Remove(closestStation);
                closestStation = GetClosestStation(locationA, stations);
            }
            return closestStation;
        }
        internal Location GetParcelSenderLocation(int id)
        {
            lock (MyDal)
            {
                var parcel = MyDal.DisplayParcel(id);
                return new Location(MyDal.DisplayCustomer(parcel.SenderId).Longitude, MyDal.DisplayCustomer(parcel.SenderId).Lattitude);
            }
        }
        internal Location GetParcelTargetLocation(int id)
        {
            lock (MyDal)
            {
                var parcel = MyDal.DisplayParcel(id);
                return new Location(MyDal.DisplayCustomer(parcel.TargetId).Longitude, MyDal.DisplayCustomer(parcel.TargetId).Lattitude);
            }
        }

        internal IEnumerable<StationForList> StationsToBL(IEnumerable<DO.Station> stations)
        {
            IEnumerable<DO.Station> original = stations;
            List<StationForList> comeBack = new List<StationForList>();
            int counter = 0;
            foreach (var station in original)
            {
                counter = Drones.Count(x => x.MyLocation.Longitude == station.Longitude && x.MyLocation.Latitude ==  station.Lattitude &&
                x.Status == DroneStatuses.maintenance);
                comeBack.Add(new StationForList(station.Id, station.Name, station.ChargeSlots, counter));
            }
            return comeBack;
        }
        internal IEnumerable<ParcelForList> ParcelsToBL(IEnumerable<DO.Parcel> Parcels)
        {
            lock (MyDal)
            {
                List<ParcelForList> comeBack = new List<ParcelForList>();
                foreach (var parcel in Parcels)
                {
                    comeBack.Add(new ParcelForList(parcel.Id, MyDal.DisplayCustomer(parcel.SenderId).Name,
                        MyDal.DisplayCustomer(parcel.TargetId).Name, (WeightCategories)parcel.Weight,
                        (Priorities)parcel.Priority, GetParcelStatus(parcel.Id)));
                }
                return comeBack;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            if (GetParcel(id).Scheduled == null)
            {
                MyDal.DeleteParcel(id);
            }
            else throw new BO.ParcelPickedUpOrIsntScheduled(id);
        }
    }
}
