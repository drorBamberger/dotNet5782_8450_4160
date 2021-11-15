using IBL.BO;
using System.Collections.Generic;
namespace IBL
{
    public interface IBL
    {
        //adds
        public void AddStation(int id, string name, Location location, int chargeSlots);
        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationId);
        public void AddCustomer(int id, string name, string phone, Location location);
        public void AddParcel(int senderId, int recieverId, WeightCategories maxWeight, Priorities priority);

        //updates
        public void DroneUpdate(int id, string name);
        public void StationUpdateAll(int id, string name, int allChargeSlot);
        public void StationUpdateName(int id, string name);
        public void StationUpdateCharge(int id, int allChargeSlot);
        public void CustomerUpdateAll(int id, string name, string phone);
        public void CustomerUpdateName(int id, string name);
        public void CustomerUpdatePhone(int id, string phone);

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

        //helpFuncs

        internal double DistanceTo(Location x1, Location x2, char unit = 'K');
        internal void GetElecticity();
        internal Station GetStation(int id);
        internal Drone GetDrone(int id);
        internal Customer GetCustomer(int id);
        internal Parcel GetParcel(int id);
        internal ParcelOnDelivery GetParcelOnDelivery(int droneId);
        internal ParcelForCustomer GetParcelForCustomer(int id, CustomerInParcel otherSide);
        internal DroneInParcel GetDroneInParcel(int parcelId);

    }
}