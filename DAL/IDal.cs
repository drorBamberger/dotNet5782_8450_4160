using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DO;

namespace DalApi
{
    public interface IDal
    {
        /// <summary>
        /// add a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="lattitude"></param>
        public void AddCustomer(int id, string name, string phone, double longitude, double lattitude);
        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        public void AddDrone(int id, string model, int maxWeight);
        /// <summary>
        /// add a parcel
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <param name="droneId"></param>
        public void AddParcel(int senderId, int targetId, int weight, int priority, int droneId);
        /// <summary>
        /// add a station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="lattitude"></param>
        /// <param name="chargeSlots"></param>
        public void AddStation(int id, string name, double longitude, double lattitude, int chargeSlots);
        /// <summary>
        /// Link parcel to drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcelId"></param>
        public void Attribution(int droneId, int parcelId);
        /// <summary>
        /// charge drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void ChargeDrone(int droneId, int stationId);
        /// <summary>
        /// return list of customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> CustomerList(Predicate<Customer> prediction);
        /// <summary>
        /// discharge drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void DisChargeDrone(int droneId, int stationId);
        /// <summary>
        /// display customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer DisplayCustomer(int id);
        /// <summary>
        /// display drone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone DisplayDrone(int id);
        /// <summary>
        /// display parcel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parcel DisplayParcel(int id);
        /// <summary>
        /// display station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Station DisplayStation(int id);
        /// <summary>
        /// return list of drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> DroneList(Predicate<Drone> prediction);
        /// <summary>
        /// Deliver parcel to customer
        /// </summary>
        /// <param name="parcelId"></param>
        public void ParcelDelivered(int parcelId);
        /// <summary>
        /// return list of parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> ParcelList(Predicate<Parcel> prediction);
        /// <summary>
        /// Pick up parcel by drone
        /// </summary>
        /// <param name="parcelId"></param>
        public void PickedParcelUp(int parcelId);
        /// <summary>
        /// return list of stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> StationList(Predicate<Station> prediction);
        /// <summary>
        /// get the amount of electricity that the drone uses per KM for each weight and the charge per hour
        /// </summary>
        /// <returns></returns>
        public double[] AskForElectricity();

        /// <summary>
        /// update drone details
        /// </summary>
        /// <param name="newDrone"></param>
        public void UpDateDrone(Drone newDrone);
        /// <summary>
        /// update customer details
        /// </summary>
        /// <param name="newCustomer"></param>
        public void UpDateCustomer(Customer newCustomer);
        /// <summary>
        /// update station details
        /// </summary>
        /// <param name="newStation"></param>
        public void UpDateStation(Station newStation);
        /// <summary>
        /// delete drone
        /// </summary>
        /// <param name="id"></param>
        public void DeleteDrone(int id);
        /// <summary>
        /// delete station
        /// </summary>
        /// <param name="id"></param>
        public void DeleteStations(int id);
    }
}