using System;

namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome7346();
            Welcome7654();
            Console.ReadKey();
        }
        static partial void Welcome7654();
        private static void Welcome7346()
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine()!;
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}