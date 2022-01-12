using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : BLApi.IBL
    {
        //displays
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DisplayStation(int id)
        {
            return GetStation(id).ToString();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DisplayDrone(int id)
        {
            return GetDrone(id).ToString();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DisplayCustomer(int id)
        {
            return GetCustomer(id).ToString();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string DisplayParcel(int id)
        {
            return GetParcel(id).ToString();
        }
    }
}
