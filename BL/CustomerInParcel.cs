using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace BO
{
    public class CustomerInParcel
    {
        public CustomerInParcel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public CustomerInParcel()
        { }
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return ("ID:        " + Id + "\n" +
                    "Name:      " + Name + "\n");
        }
    }
}

