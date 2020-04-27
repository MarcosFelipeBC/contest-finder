using System.Collections.Generic;
using System;
using ContestFinder.Models;
using System.Linq;

namespace ContestFinder.Communication
{
    public static class IO
    {
        public static List < string > GetUsers()
        {
            Console.WriteLine("Write the usernames space-separated");
            string usersString = Console.ReadLine();
            
            var users = usersString.Split(',');
            return users.Select(user => user.TrimEnd().TrimStart()).ToList();
        }

        public static bool AnotherContest()
        {
            Console.WriteLine("Do you want another contest? (Y/N)");
            char ans = Console.ReadLine().ToLower()[0];
            return (ans == 'y');
        }

        public static void DisplayContest(ContestDefinition contest)
        {
            Console.WriteLine($"Id = {contest.Id} || Name = {contest.Name}");
        }

        public static string GetFilter()
        {
            Console.WriteLine("What filter do you want apply to our contest list? (Educational, Div. 2, Global, etc)");
            string filter = Console.ReadLine();
            return filter;
        }

        public static bool OlderFirst()
        {
            Console.WriteLine("Do you want newer or older contests? (newer/older)");
            var ans = Console.ReadLine();
            return (ans.ToLower() == "older");
        }

        public static void NotFound()
        {
            Console.WriteLine("Contest not found");
        }
    }
}