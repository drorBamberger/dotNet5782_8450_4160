using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        //displays
        public string DisplayStation(int id)
        {
            return GetStation(id).ToString();
        }

        public string DisplayDrone(int id)
        {
            return GetDrone(id).ToString();
        }

        public string DisplayCustomer(int id)
        {
            return GetCustomer(id).ToString();
        }

        public string DisplayParcel(int id)
        {
            return GetParcel(id).ToString();
        }
    }
}
