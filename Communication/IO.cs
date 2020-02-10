using System.Collections.Generic;
using System;
using ContestFinder.Models;

namespace ContestFinder.Communication
{
    public static class IO
    {
        public static List < string > GetUsers()
        { 
            Console.Write("How many users are going to do the constest? ");
            int amount = Convert.ToInt32(Console.ReadLine());
            List <string> users = new List<string>();
            for (int i=0; i<amount; i++){
                users.Add(Console.ReadLine());
            }
            Console.WriteLine("");
            return users;
        }

        public static bool AnotherContest()
        {
            Console.WriteLine("Do you want another contest? (Y/N)");
            char ans = Console.ReadLine()[0];
            return (ans == 'Y' || ans == 'y');

        }

        public static void DisplayContest(ContestDefinition contest)
        {
            Console.WriteLine($"Id = {contest.Id} || Name = {contest.Name}");
        }

        public static string GetFilter()
        {
            Console.WriteLine("What filter do you want apply to our contest list? (Educational, Div. 2, Global)");
            string filter = Console.ReadLine();
            return filter;
        }

    }
}