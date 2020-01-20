using System.Threading.Tasks;
using System;


namespace contests_finder
{

    class Program
    {
        static async Task Main(string[] args)
        {
            App app = new App();

            await app.FindContest("");

        }
    }
}
