using System;

namespace ConsoleUI
{
    class Program
    {
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
            string Choice;
            int res;
            PrintAdd();
            Choice = Console.ReadLine();
            res = Convert.ToInt32(Choice);
            Console.WriteLine("Your Choice:", Choice);
            do
            {
                ToContinue = false;
                switch (res)
                {
                    case 1:
                        
                        break;
                    case 2:
                        
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
            string Choice;
            int res;
            while (ToContinue)
            {
                PrintMain();
                Choice = Console.ReadLine();
                res = Convert.ToInt32(Choice);
                Console.WriteLine("Your Choice:", Choice);
                switch(res)
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
