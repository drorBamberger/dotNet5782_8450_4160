using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class CustomerInParcel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return ("ID:        " + Id + "\n" +
                        "Name:      " + Name + "\n");
            }
        }
    }
}
