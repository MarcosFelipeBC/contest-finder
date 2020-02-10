using System.Threading.Tasks;
using System;


namespace ContestFinder
{

    class Program
    {
        public static void Main(string[] args)
        {
            App app = new App();

            var filter = "";

            Console.WriteLine("What is the division of the contest you want to find? (1, 2 or 3)");
            filter = Console.ReadLine();
            filter = $"Div. {filter}";
            
            app.FindContest(filter);
        }
    }
}
