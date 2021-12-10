using System;
using System.Collections.Generic;
using System.Text;


namespace DO
{
    public struct Customer
    {
        public Customer(int id, string name, string phone, double longitude, double lattitude)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Longitude = longitude;
            Lattitude = lattitude;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }

        public override string ToString()
        {
            return ("ID:        " + Id + "\n" +
                    "Name:      " + Name + "\n" +
                    "Phone:     " + Phone + "\n" +
                    "Longitude: " + Longitude + "\n" +
                    "Lattitude: " + Lattitude + "\n");
        }
    }

}

