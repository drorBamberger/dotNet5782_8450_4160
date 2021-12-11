/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DO;

namespace ConsoleUI
{
    class Program
    {
        static DalObject.DalObject dataBase = new DalObject.DalObject();
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

            Console.WriteLine("Enter chargeSlots:");
            chargeSlots = GetInt();

            dataBase.AddStation(stationId, name, longitude, lattitude, chargeSlots);
        }

        /// <summary>
        /// Add a Drone to data base
        /// </summary>
        static void AddDrone()
        {
            int DroneId;
            string model;
            int maxWeight;

            Console.WriteLine("Enter id:");
            DroneId = GetInt();

            Console.WriteLine("Enter model:");
            model = Console.ReadLine();

            Console.WriteLine("Enter maxWeight:");
            maxWeight = GetInt();

            dataBase.AddDrone(DroneId, model, maxWeight);
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

            dataBase.AddCustomer(id, name, phone, longitude, lattitude);
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
            int droneId;

            Console.WriteLine("Enter sender id:");
            senderId = GetInt();

            Console.WriteLine("Enter target id:");
            targetId = GetInt();

            Console.WriteLine("Enter weight:");
            weight = GetInt();

            Console.WriteLine("Enter priority:");
            priority = GetInt();

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            dataBase.AddParcel(senderId, targetId, weight, priority, droneId);
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
                        Attribution();
                        break;
                    case 2:
                        PickedParcelUp();
                        break;
                    case 3:
                        ParcelDelivered();
                        break;
                    case 4:
                        ChargeDrone();
                        break;
                    case 5:
                        DisChargeDrone();
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
            Console.WriteLine("1. Link parcel to drone");
            Console.WriteLine("2. Pick up parcel by drone");
            Console.WriteLine("3. Deliver parcel to customer");
            Console.WriteLine("4. Send drone to charge");
            Console.WriteLine("5. Release drone from charger");
            Console.WriteLine("Enter your choice:");

        }

        /// <summary>
        /// Link parcel to drone
        /// </summary>
        static void Attribution() 
        {
            int droneId;
            int parcelId;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            Console.WriteLine("Enter parcel id:");
            parcelId = GetInt();

            dataBase.Attribution(droneId, parcelId);
        }

        /// <summary>
        /// Pick up parcel by drone
        /// </summary>
        static void PickedParcelUp() 
        {
            int droneId;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            dataBase.PickedParcelUp(droneId);
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

        /// <summary>
        /// Send drone to charge
        /// </summary>
        static void ChargeDrone() 
        {
            int droneId;
            int stationId;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            Console.WriteLine("Enter station id:");
            stationId = GetInt();

            dataBase.ChargeDrone(droneId, stationId);
        }

        /// <summary>
        /// Release drone from charger
        /// </summary>
        static void DisChargeDrone() 
        {
            int droneId;
            int stationId;

            Console.WriteLine("Enter drone id:");
            droneId = GetInt();

            Console.WriteLine("Enter station id:");
            stationId = GetInt();

            dataBase.DisChargeDrone(droneId, stationId);
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
                        List<Station> stations = (List<Station>)dataBase.StationList();
                        foreach (var item in stations)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        List<Drone> drones = (List<Drone>)dataBase.DroneList();
                        foreach (var item in drones)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        List<Customer> customers = (List<Customer>)dataBase.CustomerList();
                        foreach (var item in customers)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine();
                        }
                        break;
                    case 4:
                        List<Parcel> parcels = (List<Parcel>)dataBase.ParcelList();
                        foreach (var item in parcels)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine();
                        }

                        break;
                    case 5:
                        parcels = (List<Parcel>)dataBase.ParcelListNotTaken();
                        foreach (var item in parcels)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine();
                        }
                        break;
                    case 6:
                        stations = (List<Station>)dataBase.FreeStations();
                        foreach (var item in stations)
                        {
                            Console.WriteLine(item);
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
*/