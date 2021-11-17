using IBL.BO;
using System.Collections.Generic;
namespace IBL
{
    public interface IBL
    {
        //adds
        public void AddStation(int id, string name, Location location, int chargeSlots);
        public void AddDrone(int id, string model, int maxWeight, int stationId);
        public void AddCustomer(int id, string name, string phone, Location location);
        public void AddParcel(int senderId, int recieverId, int maxWeight, int priority);

        //updates
        public void DroneUpdate(int id, string name);
        public void StationUpdate(int id, string name, int allChargeSlot);
        public void CustomerUpdate(int id, string name, string phone);

        //displays
        public string DisplayDrone(int id);
        public string DisplayStation(int id);
        public string DisplayCustomer(int id);
        public string DisplayParcel(int id);

        //display lists
        public IEnumerable<StationForList> StationList();
        public IEnumerable<DroneForList> DroneList();
        public IEnumerable<CustomerForList> CustomerList();
        public IEnumerable<ParcelForList> ParcelList();
        public IEnumerable<ParcelForList> ParcelListNotTaken();
        public IEnumerable<StationForList> FreeStations();

        //charge and disCharge:
        public void ChargeDrone(int id);
        public void DisChargeDrone(int id, double time);
        //helpFuncs

        //internal double DistanceTo(Location x1, Location x2, char unit = 'K');
        //internal void GetElecticity();
        //internal Station GetStation(int id);
        //internal Drone GetDrone(int id);
        //internal Customer GetCustomer(int id);
        //internal Parcel GetParcel(int id);
        //internal ParcelOnDelivery GetParcelOnDelivery(int droneId);
        //internal ParcelForCustomer GetParcelForCustomer(int id, CustomerInParcel otherSide);
        //internal DroneInParcel GetDroneInParcel(int parcelId);
        //internal ParcelStatuses GetParcelStatus(int id);
    }
}