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
            Console.WriteLine("Write the usernames comma-separated");
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

        public static void DisplayContest(ContestDefinition contest, bool gym)
        {
            Console.WriteLine($"\nId   = {contest.Id}");
            Console.WriteLine($"Name = {contest.Name}");

            if(gym)
                Console.WriteLine($"Link = https://codeforces.com/gym/{contest.Id}\n");
            else
                Console.WriteLine($"Link = https://codeforces.com/contest/{contest.Id}\n");
        }

        public static string GetFilter(bool gym)
        {
            if(gym)
                Console.WriteLine("What filter do you want apply to our contest list? (ICPC, Brazil, Asia, etc)");
            else
                Console.WriteLine("What filter do you want apply to our contest list? (Educational, Div. 2, Global, etc)");
            string filter = Console.ReadLine().ToLower();
            return filter;
        }

        public static bool GetGym()
        {
            Console.WriteLine("Do you want to find for Gym? (Y/N)");
            char ans = Console.ReadLine().ToLower()[0];
            return (ans == 'y');
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