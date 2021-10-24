using System;

namespace ConsoleUI
{
    class Program
    {
        static DalObject.DalObject dataBase = new DalObject.DalObject();
        static void PrintMain()
        {
            Console.WriteLine("Welcome to the drones delivery!");
            Console.WriteLine("Please enter your choice:");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Display");
            Console.WriteLine("4. Display lists");
            Console.WriteLine("5. Exit");
        }
        static void PrintAdd()
        {
            Console.WriteLine("What you want to add:");
            Console.WriteLine("1. Station");
            Console.WriteLine("2. Drone");
            Console.WriteLine("3. Costumer");
            Console.WriteLine("4. Parcel");
        }
        static void AddStation()
        {
            string temp;
            int stationId;
            string name;
            double longitude;
            double lattitude;
            int chargeSlots;

            Console.WriteLine("Enter id:");
            temp = Console.ReadLine();
            int.TryParse(temp, out stationId);

            Console.WriteLine("Enter name:");
            name = Console.ReadLine();

            Console.WriteLine("Enter longitude:");
            temp = Console.ReadLine();
            double.TryParse(temp, out longitude);

            Console.WriteLine("Enter lattitude:");
            temp = Console.ReadLine();
            double.TryParse(temp, out lattitude);

            Console.WriteLine("Enter chargeSlots:");
            temp = Console.ReadLine();
            int.TryParse(temp, out chargeSlots);

            dataBase.addStation(stationId, name, longitude, lattitude, chargeSlots);
        }
        static void AddDrone()
        {
            string temp;
            int DroneId;
            string model;
            int maxWeight;
            int status;
            double battery;

            Console.WriteLine("Enter id:");
            temp = Console.ReadLine();
            int.TryParse(temp, out DroneId);

            Console.WriteLine("Enter model:");
            model = Console.ReadLine();

            Console.WriteLine("Enter maxWeight:");
            temp = Console.ReadLine();
            int.TryParse(temp, out maxWeight);

            Console.WriteLine("Enter status:");
            temp = Console.ReadLine();
            int.TryParse(temp, out status);

            Console.WriteLine("Enter battery:");
            temp = Console.ReadLine();
            double.TryParse(temp, out battery);

            dataBase.addDrone(DroneId, model, maxWeight, status, battery);
        }
        static void AddCustomer()
        {
            string temp;
            int id;
            string name;
            string phone;
            double longitude;
            double lattitude;

            Console.WriteLine("Enter id:");
            temp = Console.ReadLine();
            int.TryParse(temp, out id);

            Console.WriteLine("Enter name:");
            name = Console.ReadLine();

            Console.WriteLine("Enter phone:");
            phone = Console.ReadLine();

            Console.WriteLine("Enter longitude:");
            temp = Console.ReadLine();
            double.TryParse(temp, out longitude);

            Console.WriteLine("Enter lattitude:");
            temp = Console.ReadLine();
            double.TryParse(temp, out lattitude);

            dataBase.addCustomer(id, name, phone, longitude, lattitude);
        }

        static void AddParcel()
        {

            string temp;
            int senderId;
            int targetId;
            int weight;
            int priority;
            int droneId;

            Console.WriteLine("Enter sender id:");
            temp = Console.ReadLine();
            int.TryParse(temp, out senderId);

            Console.WriteLine("Enter target id:");
            temp = Console.ReadLine();
            int.TryParse(temp, out targetId);

            Console.WriteLine("Enter weight:");
            temp = Console.ReadLine();
            int.TryParse(temp, out weight);

            Console.WriteLine("Enter priority:");
            temp = Console.ReadLine();
            int.TryParse(temp, out priority);

            Console.WriteLine("Enter drone id:");
            temp = Console.ReadLine();
            int.TryParse(temp, out droneId);


            dataBase.addParcel(senderId, targetId, weight, priority, droneId);
        }

        static void Add()
        {
            bool ToContinue = false;
            string temp;
            int choice;
            PrintAdd();
            temp = Console.ReadLine();
            int.TryParse(temp, out choice);
            Console.WriteLine("Your Choice:", temp);
            do
            {
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


        static void Main(string[] args)
        {
            bool ToContinue = true;
            string temp;
            int choice;
            while (ToContinue)
            {
                PrintMain();
                temp = Console.ReadLine();
                int.TryParse(temp, out choice);

                Console.WriteLine("Your Choice:", temp);
                switch(choice)
                {
                    case 1:
                        //Add();
                        Console.WriteLine("Add");
                        break;
                    case 2:
                        //Update();
                        Console.WriteLine("Update");
                        break;
                    case 3:
                        //Display();
                        Console.WriteLine("Display");
                        break;
                    case 4:
                        //DisplayLists();
                        Console.WriteLine("DisplayLists");
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
