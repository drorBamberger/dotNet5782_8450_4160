﻿using System;
using IBL.BO;

namespace ConsoleUI_BL
{
    class Program
    { 
        static IBL.IBL dataBase;
        /// <summary>
        /// get int from the console
        /// </summary>
        /// <returns>the input in int format </returns>
        static int GetInt()
        {
            int res;
            string temp;
            temp = Console.ReadLine();
            int.TryParse(temp, out res);
            return res;
        }

        /// <summary>
        /// get double from the console
        /// </summary>
        /// <returns> the input in double format </returns>
        static double GetDouble()
        {
            double res;
            string temp;
            temp = Console.ReadLine();
            double.TryParse(temp, out res);
            return res;
        }

        /// <summary>
        /// print the main menu to the console
        /// </summary>
        static void PrintMain()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter your choice:");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Display");
            Console.WriteLine("4. Display lists");
            Console.WriteLine("5. Exit");
        }

        //~~~~~~~~~~~~~~~~ADD~~~~~~~~~~~~~~~~~~~ADD~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ADD~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /// <summary>
        /// Handles in case the user wants to add things to the database
        /// </summary>
        static void Add()
        {
            bool ToContinue;
            int choice;
            do
            {
                PrintAdd();
                choice = GetInt();
                ToContinue = false;
                switch (choice)
                {
                    case 1:
                        AddStation();
                        break;
                    case 2:
                        AddDrone();
                        break;
                    case 3:
                        AddCustomer();
                        break;
                    case 4:
                        AddParcel();
                        break;
                    default:
                        ToContinue = true;
                        Console.WriteLine("No valid, please enter and number between 1 and 4:");
                        break;
                }
            } while (ToContinue);
        }

        /// <summary>
        /// Print the Add menu to the console
        /// </summary>
        static void PrintAdd()
        {
            Console.WriteLine("What you want to add?");
            Console.WriteLine("1. Station");
            Console.WriteLine("2. Drone");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Parcel");
            Console.WriteLine("Enter your choice:");

        }

        /// <summary>
        /// Add a Station to data base
        /// </summary>
        static void AddStation()
        {
            int stationId;
            string name;
            double longitude;
            double lattitude;
            int chargeSlots;

            Console.WriteLine("Enter id:");
            stationId = GetInt();

            Console.WriteLine("Enter name:");
            name = Console.ReadLine();

            Console.WriteLine("Enter longitude:");
            longitude = GetDouble();

            Console.WriteLine("Enter lattitude:");
            lattitude = GetDouble();

            Location loc = new Location(longitude, lattitude);

            Console.WriteLine("Enter chargeSlots:");
            chargeSlots = GetInt();

            dataBase.AddStation(stationId, name, loc, chargeSlots);
        }

        /// <summary>
        /// Add a Drone to data base
        /// </summary>
        static void AddDrone()
        {
            int DroneId;
            string model;
            int maxWeight;
            int numStat;

            Console.WriteLine("Enter id:");
            DroneId = GetInt();

            Console.WriteLine("Enter model:");
            model = Console.ReadLine();

            Console.WriteLine("Enter maxWeight:");
            maxWeight = GetInt();

            Console.WriteLine("Enter station number for initial charging:");
            numStat = GetInt();

            dataBase.AddDrone(DroneId, model, maxWeight, numStat);
        }

        /// <summary>
        /// Add a Customer to data base
        /// </summary>
        static void AddCustomer()
        {
            int id;
            string name;
            string phone;
            double longitude;
            double lattitude;

            Console.WriteLine("Enter id:");
            id = GetInt();

            Console.WriteLine("Enter name:");
            name = Console.ReadLine();

            Console.WriteLine("Enter phone:");
            phone = Console.ReadLine();

            Console.WriteLine("Enter longitude:");
            longitude = GetDouble();

            Console.WriteLine("Enter lattitude:");
            lattitude = GetDouble();

            Location loc = new Location(longitude, lattitude);

            dataBase.AddCustomer(id, name, phone, loc);
        }

        /// <summary>
        /// Add a Parcel to data base
        /// </summary>
        static void AddParcel()
        {

            int senderId;
            int targetId;
            int weight;
            int priority;

            Console.WriteLine("Enter sender id:");
            senderId = GetInt();

            Console.WriteLine("Enter target id:");
            targetId = GetInt();

            Console.WriteLine("Enter weight:");
            weight = GetInt();

            Console.WriteLine("Enter priority:");
            priority = GetInt();

            dataBase.AddParcel(senderId, targetId, weight, priority);
        }

        //~~~~~~~~~~~~~~~~UPDATE~~~~~~~~~~~~~~~~~~~UPDATE~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~UPDATE~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /// <summary>
        /// Handles in case the user wants to update things to the database
        /// </summary>
        static void Update()
        {
            bool ToContinue;
            int choice;
            do
            {
                PrintUpdate();
                choice = GetInt();
                ToContinue = false;
                switch (choice)
                {
                    case 1:
                        DroneUpdate();
                        break;
                    case 2:
                        StationUpdate();
                        break;
                    case 3:
                        CustomerUpdate();
                        break;
                    case 4:
                        ChargeDrone();
                        break;
                    case 5:
                        DisChargeDrone();
                        break;
                    case 6:
                        Attribution();
                        break;
                    case 7:
                        PickedParcelUp();
                        break;
                    case 8:
                        ParcelDelivered();
                        break;
                    default:
                        ToContinue = true;
                        Console.WriteLine("No valid, please enter and number between 1 and 4:");
                        break;
                }
            } while (ToContinue);
        }

        /// <summary>
        /// Print the Update menu to the console
        /// </summary>
        static void PrintUpdate()
        {
            Console.WriteLine("What you want to update?");
            Console.WriteLine("1. Name of drone");
            Console.WriteLine("2. Station");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Send drone to charge");
            Console.WriteLine("5. Release drone from charger");
            Console.WriteLine("6. Link parcel to drone");
            Console.WriteLine("7. Pick up parcel by drone");
            Console.WriteLine("8. Deliver parcel by drone");
            Console.WriteLine("Enter your choice:");

        }

        /// <summary>
        /// update name of drone
        /// </summary>
        static void DroneUpdate()
        {
            int droneId;
            string droneName;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            Console.WriteLine("Enter the new name of the drone:");
            droneName = Console.ReadLine();

            dataBase.DroneUpdate(droneId, droneName);
        }

        /// <summary>
        /// update station details
        /// </summary>
        static void StationUpdate()
        {
            int stationId;
            string stationName;
            int chargeSlots;

            Console.WriteLine("Enter station id:");
            stationId = GetInt();

            Console.WriteLine("Enter the new name of the station:");
            stationName = Console.ReadLine();

            Console.WriteLine("Enter chargeSlots:");
            chargeSlots = GetInt();
            //TO DO: Empty input
            dataBase.StationUpdate(stationId, stationName, chargeSlots);
        }
        
        /// <summary>
        /// update customer details
        /// </summary>
        static void CustomerUpdate()
        {
            int Id;
            string customerName;
            int phoneNumber;

            Console.WriteLine("Enter customer id:");
            Id = GetInt();

            Console.WriteLine("Enter the new name of the station:");
            customerName = Console.ReadLine();

            Console.WriteLine("Enter phone number:");
            phoneNumber = GetInt();
            //TO DO: Empty input
            dataBase.CustomerUpdate(Id, customerName, phoneNumber);
        }

        /// <summary>
        /// Send drone to charge
        /// </summary>
        static void ChargeDrone()
        {
            int droneId;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            dataBase.ChargeDrone(droneId);
        }

        /// <summary>
        /// Release drone from charger
        /// </summary>
        static void DisChargeDrone()
        {
            int droneId;
            int time;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            Console.WriteLine("Enter time in charge:");
            time = GetInt();

            dataBase.DisChargeDrone(droneId, time);
        }

        /// <summary>
        /// Link parcel to drone
        /// </summary>
        static void Attribution()
        {
            int droneId;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            dataBase.Attribution(droneId);
        }

        /// <summary>
        /// Pick up parcel by drone
        /// </summary>
        static void PickedParcelUp()
        {
            int parcelId;

            Console.WriteLine("Enter parcel id:");
            parcelId = GetInt();

            dataBase.PickedParcelUp(parcelId);
        }

        /// <summary>
        /// Deliver parcel to customer
        /// </summary>
        static void ParcelDelivered()
        {
            int droneId;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            dataBase.ParcelDelivered(droneId);
        }

        //~~~~~~~~~~~~~~~~DISPLAY~~~~~~~~~~~~~~~~~~~DISPLAY~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~DISPLAY~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /// <summary>
        /// Handles in case the user wants to display things to the database
        /// </summary>
        static void Display()
        {
            bool ToContinue;
            int choice;
            do
            {
                PrintDisplay();
                choice = GetInt();
                ToContinue = false;
                switch (choice)
                {
                    case 1:
                        Console.WriteLine(DisplayStation());
                        break;
                    case 2:
                        Console.WriteLine(DisplayDrone());
                        break;
                    case 3:
                        Console.WriteLine(DisplayCustomer());
                        break;
                    case 4:
                        Console.WriteLine(DisplayParcel());
                        break;
                    default:
                        ToContinue = true;
                        Console.WriteLine("No valid, please enter and number between 1 and 4:");
                        break;
                }
            } while (ToContinue);
        }

        /// <summary>
        /// Print the Display menu to the console
        /// </summary>
        static void PrintDisplay()
        {
            Console.WriteLine("What you want to display?");
            Console.WriteLine("1. Station");
            Console.WriteLine("2. Drone");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Parcel");
            Console.WriteLine("Enter your choice:");

        }

        /// <summary>
        /// Print the requested station to the console
        /// </summary>
        static Station DisplayStation()
        {
            int id;

            Console.WriteLine("Enter id:");
            id = GetInt();

            return dataBase.DisplayStation(id);
        }

        /// <summary>
        /// Print the requested drone to the console
        /// </summary>
        static Drone DisplayDrone()
        {
            int id;

            Console.WriteLine("Enter id:");
            id = GetInt();

            return dataBase.DisplayDrone(id);
        }

        /// <summary>
        /// Print the requested customer to the console
        /// </summary>
        static Customer DisplayCustomer()
        {
            int id;

            Console.WriteLine("Enter id:");
            id = GetInt();

            return dataBase.DisplayCustomer(id);
        }

        /// <summary>
        /// Print the requested parcel to the console
        /// </summary>
        static Parcel DisplayParcel()
        {
            int id;

            Console.WriteLine("Enter id:");
            id = GetInt();

            return dataBase.DisplayParcel(id);
        }

        //~~~~~~~~~~~~~~~~DISPLAYLISTS~~~~~~~~~~~~~DISPLAYLISTS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~DISPLAYLISTS~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /// <summary>
        /// Handles in case the user wants to display lists things to the database
        /// </summary>
        static void DisplayLists()
        {
            bool ToContinue;
            int choice;
            do
            {
                PrintDisplayLists();
                choice = GetInt();
                ToContinue = false;
                switch (choice)
                {
                    case 1:
                        Station[] stations = dataBase.StationList();
                        for (int i = 0; i < stations.Length; ++i)
                        {
                            Console.WriteLine(stations[i]);
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        Drone[] drones = dataBase.DroneList();
                        for (int i = 0; i < drones.Length; ++i)
                        {
                            Console.WriteLine(drones[i]);
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        Customer[] customers = dataBase.CustomerList();
                        for (int i = 0; i < customers.Length; ++i)
                        {
                            Console.WriteLine(customers[i]);
                            Console.WriteLine();
                        }
                        break;
                    case 4:
                        Parcel[] parcels = dataBase.ParcelList();
                        for (int i = 0; i < parcels.Length; ++i)
                        {
                            Console.WriteLine(parcels[i]);
                            Console.WriteLine();
                        }

                        break;
                    case 5:
                        parcels = dataBase.ParcelListNotTaken();
                        for (int i = 0; i < parcels.Length; ++i)
                        {
                            Console.WriteLine(parcels[i]);
                            Console.WriteLine();
                        }
                        break;
                    case 6:
                        stations = dataBase.FreeStations();
                        for (int i = 0; i < stations.Length; ++i)
                        {
                            Console.WriteLine(stations[i]);
                            Console.WriteLine();
                        }
                        break;
                    default:
                        ToContinue = true;
                        Console.WriteLine("No valid, please enter and number between 1 and 4:");
                        break;
                }
            } while (ToContinue);
        }

        /// <summary>
        /// Print the DisplayLists menu to the console
        /// </summary>
        static void PrintDisplayLists()
        {
            Console.WriteLine("What you want to display?");
            Console.WriteLine("1. Stations");
            Console.WriteLine("2. Drones");
            Console.WriteLine("3. Customers");
            Console.WriteLine("4. Parcels");
            Console.WriteLine("5. Free Parcels");
            Console.WriteLine("6. Free stations");
            Console.WriteLine();
            Console.WriteLine("Enter your choice:");

        }

        static void Main(string[] args)
        {
            bool ToContinue = true;
            int choice;
            Console.WriteLine("Welcome to the drones delivery!");
            while (ToContinue)
            {
                PrintMain();
                choice = GetInt();
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Update();
                        break;
                    case 3:
                        Display();
                        break;
                    case 4:
                        DisplayLists();
                        break;
                    case 5:
                        ToContinue = false;
                        break;
                    default:
                        Console.WriteLine("No valid, please enter and number between 1 and 5:");
                        break;
                }
            }
        }
    }
}

