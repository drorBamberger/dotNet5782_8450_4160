using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4160();
            Welcome8450();
            Console.ReadKey();
        }
        static partial void Welcome8450();
        private static void Welcome4160()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}

