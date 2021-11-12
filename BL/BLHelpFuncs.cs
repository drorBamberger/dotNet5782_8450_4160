using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public partial class BL
    {
        private void GetElecticity()
        {
            double[] tmp = new double[5];
            tmp = MyDal.AskForElectricity();
            Available     = tmp[0];
            SmallPackege  = tmp[1];
            MediumPackege = tmp[2];
            HeavyPackege  = tmp[3];
            ChargePerHour = tmp[4];
        }
    }
}
