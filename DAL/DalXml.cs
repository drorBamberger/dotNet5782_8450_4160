using DalApi;
using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace DalXml
{
    internal sealed class DalXml : DalApi.IDal
    {
        private static DalXml instance = null;

        internal static DalXml Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DalXml();
                }
                return instance;
            }
        }

        public DalXml()
        {
            Initialize();
        }
        public static void Initialize()
        {
            Random rnd = new Random();

            //stations initialization

            /*XmlSerializer ser = new XmlSerializer(typeof(List<Station>));
            XmlReader reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> stationData;
            if (ser.CanDeserialize(reader))
            {
                stationData = (List<Station>)ser.Deserialize(reader);
            }
            else
            {
                stationData = new List<Station>();
            }
            reader.Close();*/
            List<Station> stationData = new List<Station>();

            stationData.Add(new Station(rnd.Next(), "London", -0.118092, 51.509865, 10));
            stationData.Add(new Station(rnd.Next(), "Brighton", -0.13947, 50.82838, 8));


            //customers initialization
            /*ser = new XmlSerializer(typeof(List<Customer>));
            reader = new XmlTextReader(@"Data\Customers.xml");
            List<Customer> customerData;
            if (ser.CanDeserialize(reader))
            {
                customerData = (List<Customer>)ser.Deserialize(reader);
            }
            else
            {
                customerData = new List<Customer>();
            }
            reader.Close();*/
            List<Customer> customerData = new List<Customer>();

            string[] customerNames = new string[] { "Dror", "Yair", "Ofir", "Gil", "Benaya", "Ohad", "Michael", "Achiya", "Drew", "Shilo" };
            for (int i = 0; i < 10; ++i)
            {
                customerData.Add(new Customer(rnd.Next(), customerNames[i], "05" + (rnd.Next(10000000, 99999999)).ToString()
                    , rnd.NextDouble() * 180 * Math.Pow(-1, rnd.Next(0, 1)), rnd.NextDouble() * 90 * Math.Pow(-1, rnd.Next(0, 2))));
            }


            //drones initialization
            /*ser = new XmlSerializer(typeof(List<Drone>));
            reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> droneData;
            if (ser.CanDeserialize(reader))
            {
                droneData = (List<Drone>)ser.Deserialize(reader);
            }
            else
            {
                droneData = new List<Drone>();
            }
            reader.Close();*/
            List<Drone> droneData = new List<Drone>();
            for (int i = 0; i < 5; i++)
            {
                droneData.Add(new Drone(rnd.Next(), "version" + i.ToString(), (WeightCategories)rnd.Next(0, 3)));

            }


            //parcels initialization
            /*ser = new XmlSerializer(typeof(List<Parcel>));
            reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> parcelData;
            if (ser.CanDeserialize(reader))
            {
                parcelData = (List<Parcel>)ser.Deserialize(reader);
            }
            else
            {
                parcelData = new List<Parcel>();
            }
            reader.Close();

            for (int i = 0; i < 10; i++)
            {
                int[] droneId = new int[] { droneData[rnd.Next(0, 5)].Id, 0 };
                parcelData.Add(new Parcel(customerData[rnd.Next(0, 10)].Id, customerData[rnd.Next(0, 10)].Id,
                   (WeightCategories)rnd.Next(0, 3), (Priorities)rnd.Next(0, 3), droneId[rnd.Next(0, 2)]));
            }*/
            List<Parcel> parcelData = parcelData = new List<Parcel>();

            TextWriter writer = new StreamWriter(@"Data\Stations.xml");
            try { ser.Serialize(writer, stationData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\Customers.xml");
            try { ser.Serialize(writer, customerData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\Drones.xml");
            try { ser.Serialize(writer, droneData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\Parcels.xml");
            try { ser.Serialize(writer, parcelData); }
            finally { writer.Close(); }


        }
        private void IsIdTaken<T>(List<T> list, int id)
        {
            foreach (T item in list)
            {
                int itemId = (int)((typeof(T).GetProperty("Id").GetValue(item, null)));
                if (itemId == id)
                {
                    throw new IdTakenException(id);
                }
            }
        }
        private void IsIdExist<T>(List<T> list, int id)
        {
            foreach (var item in list)
            {
                int itemId = (int)((typeof(T).GetProperty("Id").GetValue(item, null)));
                if (itemId == id)
                {
                    return;
                }
            }
            throw new IdNotExistException(id);
        }
        //add option

        public void AddStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Station>));
            XmlReader reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> data = (List<Station>)ser.Deserialize(reader);
            reader.Close();
            IsIdTaken(data, id);

            data.Add(new Station(id, name, longitude, lattitude, chargeSlots));

            TextWriter writer = new StreamWriter(@"Data\Stations.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }

        }
        public void AddDrone(int id, string model, int maxWeight)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> data = (List<Drone>)ser.Deserialize(reader);
            reader.Close();
            IsIdTaken(data, id);

            data.Add(new Drone(id, model, (WeightCategories)maxWeight));

            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
        public void AddCustomer(int id, string name, string phone, double longitude, double lattitude)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Customer>));
            XmlReader reader = new XmlTextReader(@"Data\Customers.xml");
            List<Customer> data = (List<Customer>)ser.Deserialize(reader);
            reader.Close();

            IsIdTaken(data, id);
            data.Add(new Customer(id, name, phone, longitude, lattitude));

            TextWriter writer = new StreamWriter(@"Data\Customers.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }

        }
        public void AddParcel(int senderId, int targetId, int weight, int priority, int droneId)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> data = (List<Parcel>)ser.Deserialize(reader);
            reader.Close();

            data.Add(new Parcel(senderId, targetId, (WeightCategories)weight
            , (Priorities)priority, droneId));

            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
        //update options
        public void Attribution(int droneId, int parcelId)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> droneData = (List<Drone>)ser.Deserialize(reader);
            reader.Close();

            ser = new XmlSerializer(typeof(List<Parcel>));
            reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> parcelData = (List<Parcel>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(droneData, droneId);
            IsIdExist(parcelData, parcelId);
            Parcel tmp = parcelData.Find(x => x.Id == parcelId);
            tmp.DroneId = droneId;
            tmp.Scheduled = DateTime.Now;
            parcelData[parcelData.FindIndex(x => x.Id == parcelId)] = tmp;

            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            try { ser.Serialize(writer, droneData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\Parcels.xml");
            try { ser.Serialize(writer, parcelData); }
            finally { writer.Close(); }
        }

        public void PickedParcelUp(int parcelId)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> data = (List<Parcel>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, parcelId);
            Parcel tmp = data.Find(x => x.Id == parcelId);
            tmp.PickedUp = DateTime.Now;
            data[data.FindIndex(x => x.Id == parcelId)] = tmp;

            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }            
        }

        public void ParcelDelivered(int parcelId)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> data = (List<Parcel>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, parcelId);
            Parcel tmp = data.Find(x => x.Id == parcelId);
            tmp.Delivered = DateTime.Now;
            data[data.FindIndex(x => x.Id == parcelId)] = tmp;

            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }

        public void ChargeDrone(int droneId, int stationId)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> droneData = (List<Drone>)ser.Deserialize(reader);
            reader.Close();

            ser = new XmlSerializer(typeof(List<Station>));
            reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> stationData = (List<Station>)ser.Deserialize(reader);
            reader.Close();

            ser = new XmlSerializer(typeof(List<Station>));
            reader = new XmlTextReader(@"Data\DroneCharge.xml");
            List<DroneCharge> DroneChargeData = (List<DroneCharge>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(droneData, droneId);
            IsIdExist(stationData, stationId);
            Station tmp = stationData.Find(x => x.Id == stationId);
            DroneChargeData.Add(new DroneCharge(droneId, stationId));
            tmp.ChargeSlots--;
            stationData[stationData.FindIndex(x => x.Id == stationId)] = tmp;

            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            try { ser.Serialize(writer, droneData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\Stations.xml");
            try { ser.Serialize(writer, stationData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\DroneCharge.xml");
            try { ser.Serialize(writer, DroneChargeData); }
            finally { writer.Close(); }
        }

        public void DisChargeDrone(int droneId, int stationId)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> droneData = (List<Drone>)ser.Deserialize(reader);
            reader.Close();

            ser = new XmlSerializer(typeof(List<Station>));
            reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> stationData = (List<Station>)ser.Deserialize(reader);
            reader.Close();

            ser = new XmlSerializer(typeof(List<Station>));
            reader = new XmlTextReader(@"Data\DroneCharge.xml");
            List<DroneCharge> DroneChargeData = (List<DroneCharge>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(droneData, droneId);
            IsIdExist(stationData, stationId);
            Station station = stationData.Find(x => x.Id == stationId);
            station.ChargeSlots++;
            stationData[stationData.FindIndex(x => x.Id == stationId)] = station;
            DroneChargeData.RemoveAll(x => x.DroneId == droneId);

            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            try { ser.Serialize(writer, droneData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\Stations.xml");
            try { ser.Serialize(writer, stationData); }
            finally { writer.Close(); }

            writer = new StreamWriter(@"Data\DroneCharge.xml");
            try { ser.Serialize(writer, DroneChargeData); }
            finally { writer.Close(); }
        }


        //displays
        public Station DisplayStation(int id)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Station>));
            XmlReader reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> data = (List<Station>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, id);
            return data.Find(x => x.Id == id);
        }

        public Drone DisplayDrone(int id)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> data = (List<Drone>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, id);
            return data.Find(x => x.Id == id);
        }

        public Customer DisplayCustomer(int id)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Customer>));
            XmlReader reader = new XmlTextReader(@"Data\Customers.xml");
            List<Customer> data = (List<Customer>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, id);
            return data.Find(x => x.Id == id);
        }

        public Parcel DisplayParcel(int id)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> data = (List<Parcel>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, id);
            return data.Find(x => x.Id == id);
        }

        //displays Lists

        public IEnumerable<Station> StationList(Predicate<Station> prediction)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Station>));
            XmlReader reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> data = (List<Station>)ser.Deserialize(reader);
            reader.Close();

            List<Station> stationList = data.FindAll(prediction);
            return stationList;
        }

        public IEnumerable<Drone> DroneList(Predicate<Drone> prediction)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> data = (List<Drone>)ser.Deserialize(reader);
            reader.Close();

            List<Drone> droneList = data.FindAll(prediction);
            return droneList;
        }

        public IEnumerable<Customer> CustomerList(Predicate<Customer> prediction)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Customer>));
            XmlReader reader = new XmlTextReader(@"Data\Customers.xml");
            List<Customer> data = (List<Customer>)ser.Deserialize(reader);
            reader.Close();

            List<Customer> customerList = data.FindAll(prediction);
            return customerList;
        }

        public IEnumerable<Parcel> ParcelList(Predicate<Parcel> prediction)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> data = (List<Parcel>)ser.Deserialize(reader);
            reader.Close();

            List<Parcel> parcelList = data.FindAll(prediction);
            return parcelList;
        }
        public double[] AskForElectricity()
        {
            XElement dataRoot = XElement.Load(@"Data\DALConfig.xml");
            double[] s = new double[5] { double.Parse(dataRoot.Element("Available").Value),
                                         double.Parse(dataRoot.Element("SmallPackege").Value),
                                         double.Parse(dataRoot.Element("MediumPackege").Value),
                                         double.Parse(dataRoot.Element("HeavyPackege").Value),
                                         double.Parse(dataRoot.Element("ChargePerHour").Value)};
            return s;
        }
        //update funcs:
        public void UpDateDrone(Drone newDrone)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> data = (List<Drone>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, newDrone.Id);
            data[data.FindIndex(x => x.Id == newDrone.Id)] = newDrone;

            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
        public void UpDateCustomer(Customer newCustomer)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Customer>));
            XmlReader reader = new XmlTextReader(@"Data\Customers.xml");
            List<Customer> data = (List<Customer>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, newCustomer.Id);
            data[data.FindIndex(x => x.Id == newCustomer.Id)] = newCustomer;

            TextWriter writer = new StreamWriter(@"Data\Customers.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
        public void UpDateStation(Station newStation)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Station>));
            XmlReader reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> data = (List<Station>)ser.Deserialize(reader);
            reader.Close();

            IsIdExist(data, newStation.Id);
            data[data.FindIndex(x => x.Id == newStation.Id)] = newStation;

            TextWriter writer = new StreamWriter(@"Data\Stations.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
        public void DeleteDrone(int id)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> data = (List<Drone>)ser.Deserialize(reader);
            reader.Close();

            data.RemoveAll(
                x => x.Id == id);

            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
        public void DeleteStation(int id)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Station>));
            XmlReader reader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> data = (List<Station>)ser.Deserialize(reader);
            reader.Close();

            data.RemoveAll(
                x => x.Id == id);

            TextWriter writer = new StreamWriter(@"Data\Stations.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
        public void DeleteParcel(int id)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> data = (List<Parcel>)ser.Deserialize(reader);
            reader.Close();

            data.RemoveAll(x => x.Id == id);

            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            try { ser.Serialize(writer, data); }
            finally { writer.Close(); }
        }
    }
}
