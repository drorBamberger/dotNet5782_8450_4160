using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLApi
{
    public interface IBL
    {
        //adds
        /// <summary>
        /// add a stattion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public void AddStation(int id, string name, Location location, int chargeSlots);
        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationId"></param>
        public void AddDrone(int id, string model, int maxWeight, int stationId);
        /// <summary>
        /// add a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        public void AddCustomer(int id, string name, string phone, Location location);
        /// <summary>
        /// add a parcel
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="recieverId"></param>
        /// <param name="maxWeight"></param>
        /// <param name="priority"></param>
        public void AddParcel(int senderId, int recieverId, int maxWeight, int priority);

        //updates
        /// <summary>
        /// update a drone model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void DroneUpdate(int id, string name);
        /// <summary>
        /// update a station details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="allChargeSlot"></param>
        public void StationUpdate(int id, string name, int allChargeSlot);
        /// <summary>
        /// update a customer details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void CustomerUpdate(int id, string name, string phone);

        //displays
        /// <summary>
        /// display drone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DisplayDrone(int id);
        /// <summary>
        /// dispaly station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DisplayStation(int id);
        /// <summary>
        /// display customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DisplayCustomer(int id);
        /// <summary>
        /// display parcel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DisplayParcel(int id);

        //display lists
        /// <summary>
        /// display all the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationForList> StationList();
        /// <summary>
        /// display all the drones that...
        /// </summary>
        /// <param name="predictaion"></param>
        /// <returns></returns>
        public IEnumerable<StationForList> StationList(Predicate<StationForList> predictaion);
        /// <summary>
        /// display all the drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneForList> DroneList();
        /// <summary>
        /// display all the drones that...
        /// </summary>
        /// <param name="predictaion"></param>
        /// <returns></returns>
        public IEnumerable<DroneForList> DroneList(Predicate<DroneForList> predictaion);
        /// <summary>
        /// display all the customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerForList> CustomerList();
        /// <summary>
        /// display all the parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelForList> ParcelList();
        /// <summary>
        /// display all the free parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelForList> ParcelListNotTaken();
        /// <summary>
        /// display all the free stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationForList> FreeStations();

        //charge and disCharge:
        /// <summary>
        /// charge drone
        /// </summary>
        /// <param name="id"></param>
        public void ChargeDrone(int id);
        /// <summary>
        /// dis charge drones
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void DisChargeDrone(int id, double time);

        /// <summary>
        /// Link parcel to drone
        /// </summary>
        /// <param name="droneId"></param>
        public void Attribution(int droneId);
        /// <summary>
        /// Pick up parcel by drone
        /// </summary>
        /// <param name="droneId"></param>
        public void PickedParcelUp(int droneId);
        /// <summary>
        /// Deliver parcel by drone
        /// </summary>
        /// <param name="parcelId"></param>
        public void ParcelDelivered(int parcelId);
        //helpFuncs

        //internal double DistanceTo(Location x1, Location x2, char unit = 'K');
        //internal void GetElecticity();
        //internal Station GetStation(int id);
        public Drone GetDrone(int id);
        public DroneForList GetDroneForList(int id);
        //internal Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        //internal ParcelOnDelivery GetParcelOnDelivery(int droneId);
        //internal ParcelForCustomer GetParcelForCustomer(int id, CustomerInParcel otherSide);
        //internal DroneInParcel GetDroneInParcel(int parcelId);
        //internal ParcelStatuses GetParcelStatus(int id);
        //internal DO.Station GetClosestStation(Location locationA, List<DO.Station> stations);
        //internal DO.Parcel GetClosestParcel(Location locationA, List<DO.Parcel> parcels);
        //internal DO.Station GetClosestStationWithChargeSlots(Location locationA, List<DO.Station> stations);
        //internal double GetElectricityPerKM(double distance, WeightCategories a);
        //internal IEnumerable<StationForList> StationsToBl(IEnumerable<DO.Station> stations);
        //internal IEnumerable<ParcelForList> ParcelsToBl(IEnumerable<DO.Parcel> Parcels);
    }
}