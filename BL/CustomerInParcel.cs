using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class CustomerInParcel
    {
        public CustomerInParcel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return ("ID:        " + Id + "\n" +
                    "Name:      " + Name + "\n");
        }
    }
}

