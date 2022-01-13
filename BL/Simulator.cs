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
        public Simulator(int droneId, Action updateDisplay, Func<bool> stopCheck, BL myBl)
        {
            int delay = 1000;//msec
            double speed = 30000.5;//km per second
            BO.Drone MyDrone = myBl.GetDrone(droneId);
            double time;
            for (int i = 0; stopCheck();i++)
            {
                MyDrone = myBl.GetDrone(droneId);
                switch (MyDrone.Status)
                {
                    case DroneStatuses.vacant:
                        try
                        {
                            myBl.Attribution(droneId);
                            i = 0;
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
                        if (myBl.GetParcel(MyDrone.MyParcel.Id).PickedUp == null)
                        {
                            time = myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelSenderLocation(MyDrone.MyParcel.Id)) / speed - i;
                            if (time<1)
                            {
                                myBl.PickedParcelUp(MyDrone.MyParcel.Id);
                                i = 0;
                                Thread.Sleep((int)(myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelSenderLocation(MyDrone.MyParcel.Id))/speed));
                            }
                            else
                            {
                                Thread.Sleep(delay);
                                myBl.AddBattery(droneId, -myBl.GetElectricityPerKM((delay / 1000) * speed, MyDrone.MyParcel.Weight));
                                //TODO bonus location
                            }
                            
                        }
                        else
                        {
                            time = myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelTargetLocation(MyDrone.MyParcel.Id)) / speed - i;
                            if (time < 1)
                            {
                                myBl.ParcelDelivered(MyDrone.MyParcel.Id);
                                i = 0;
                                Thread.Sleep((int)(myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelTargetLocation(MyDrone.MyParcel.Id)) / speed));
                                MyDrone.MyParcel.TransferDistance = 0;
                            }
                            else
                            {
                                Thread.Sleep(delay);
                                myBl.AddBattery(droneId, -myBl.GetElectricityPerKM((delay / 1000) * speed, MyDrone.MyParcel.Weight));
                                MyDrone.MyParcel.TransferDistance -= (delay / 1000) * speed;
                            }
                            
                        }
                        break;
                    case DroneStatuses.maintenance:
                        Thread.Sleep(delay);
                        if (MyDrone.Battery == 100)
                        {
                            myBl.DisChargeDrone(droneId, 0.1);
                            i = 0;
                            break;
                        }
                        myBl.AddBattery(droneId, (delay / 1000)*myBl.ChargePerSecond);
                        break;
                }
            }
        }
    }
}
