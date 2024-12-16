﻿namespace _01_OOP_050_Loop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 1, 4, 9, 16, 25 };
            Loop<int> l = new Loop<int>(nums);

            Console.WriteLine(l.Current); //vypíše 1

            l.Right();
            Console.WriteLine(l.Current); //vypíše 4

            l.Right();
            l.Right();
            Console.WriteLine(l.Current); //vypíše 16

            l.Right();
            l.Right();
            Console.WriteLine(l.Current); //vypíše 1


            l.Left();
            Console.WriteLine(l.Current); //vypíše 25
        }
    }
}
