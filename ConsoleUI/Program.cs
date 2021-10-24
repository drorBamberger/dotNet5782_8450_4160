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
                        int stationId;
                        string name;
                        double longitude;
                        double lattitude;
                        int chargeSlots;

                        Console.WriteLine("Enter id:");
                        temp = Console.ReadLine();
                        int.TryParse(temp,out id);

                        Console.WriteLine("Enter name:");
                        name = Console.ReadLine();

                        Console.WriteLine("Enter longitude:");
                        temp = Console.ReadLine();
                        longitude = Convert.ToDouble(temp);

                        Console.WriteLine("Enter lattitude:");
                        temp = Console.ReadLine();
                        lattitude = Convert.ToDouble(temp);

                        Console.WriteLine("Enter chargeSlots:");
                        temp = Console.ReadLine();
                        int.TryParse(temp, out chargeSlots);

                        dataBase.addStation(id, name, longitude, lattitude, chargeSlots);
                        break;
                    case 2:
                        int DroneId;
                        string model;
                        int maxWeight;
                        int status;
                        double battery;
                        dataBase.addDrone(DroneId, model, maxWeight, status, battery);
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        
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
