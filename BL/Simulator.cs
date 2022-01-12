using System;
using System.Collections.Generic;
using System.Text;
using BO;
using System.Threading;
using static BL.BL;
using System.Linq;

namespace BL
{
    internal class Simulator
    {
        public Simulator(int droneId, Action updateDisplay, Func<bool> stopCheck, BLApi.IBL myBl)
        {
            int delay = 1000;//msec
            double speed = 30000.5;//km per hour
            BO.Drone MyDrone = myBl.GetDrone(droneId);
            while (stopCheck())
            {
                switch(MyDrone.Status)
                {
                    case DroneStatuses.vacant:
                        try
                        {
                            myBl.Attribution(droneId);
                        }
                        catch(BO.DroneHaveToLittleBattery)
                        {
                            try
                            {
                                myBl.ChargeDrone(droneId);
                            }
                            catch
                            {
                                //TODO
                                throw new BO.CantBeNegative(droneId);
                            }
                        }
                        catch (BO.NoParcelMatch)
                        {
                            Thread.Sleep(delay);
                        }
                        break;
                    case DroneStatuses.Shipping:
                        if(myBl.GetParcel(MyDrone.MyParcel.Id).PickedUp == null)
                        {
                            myBl.PickedParcelUp(MyDrone.MyParcel.Id);
                        }
                        else
                        {
                            myBl.ParcelDelivered(MyDrone.MyParcel.Id);
                        }
                        break;
                    case DroneStatuses.maintenance:
                        Thread.Sleep(delay);
                        myBl.DisChargeDrone(droneId, delay/1000);
                        myBl.
                        break;
                }
            }
        }
    }
}
