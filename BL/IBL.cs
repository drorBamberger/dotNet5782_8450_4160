using IBL.BO;
using System.Collections.Generic;
namespace IBL
{
    public interface IBL
    {
        //adds
        public void AddStation(int id, string name, Location location, int chargeSlots);
        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationId);
        public void AddCustomer(int id, string name, int phone, Location location);
        public void AddParcel(int senderId, int recieverId, WeightCategories maxWeight, Priorities priority);

        //updates
        public void DroneUpdate(int id, string name);
        public void StationUpdate(int id, string name, int allChargeSlot);
        public void StationUpdate(int id, string name);
        public void StationUpdate(int id, int allChargeSlot);
        public void CustomerUpdate(int id, string name, int phone);
        public void CustomerUpdate(int id, string name);
        public void CustomerUpdate(int id, int phone);

        //displays
        public Drone DisplayDrone(int id);
        public Station DisplayStation(int id);
        public Station DisplayCustomer(int id);
        public Station DisplayParcel(int id);

        //display lists
        public IEnumerable<StationForList> StationList();
        public IEnumerable<DroneForList> DroneList();
        public IEnumerable<CustomerForList> CustomerList();
        public IEnumerable<ParcelForList> ParcelList();
        public IEnumerable<ParcelForList> ParcelListNotTaken();
        public IEnumerable<StationForList> FreeStations();



    }
}