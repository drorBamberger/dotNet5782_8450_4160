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
        public void PlaySimulator(int droneId, Action updateDisplay, Func<bool> stopCheck)
        {
            Simulator simi = new Simulator(droneId, updateDisplay, stopCheck, this);
        }

    }
}
