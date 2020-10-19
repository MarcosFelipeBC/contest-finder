using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContestFinder.Models;
using ContestFinder.Communication;
using ContestFinder.Services;


namespace ContestFinder
{
    public class App
    {    
        private List<ContestDefinition> _contests;
        private List<String> _users;

        public App()
        {
            var responseContests = CodeforcesClient.GetContestsList(false);
            _users = IO.GetUsers();

            var responseCheckUsers = CheckUsers(_users);
            responseCheckUsers.GetAwaiter().GetResult();
            var able = responseCheckUsers.Result;

            if(!able)
            {
                throw new Exception();
            }

            var response = responseContests.GetAwaiter().GetResult();

            if(response.Status == "FAILED")
            {
                throw new Exception();
            }

            _contests = response.Result;
            if(IO.OlderFirst())
            {
                _contests.Reverse();
            }
        }

        public async Task<bool> CheckUsers(List<string> users)
        {
            bool able = true;
            foreach (var username in users)
            {
                var responseUser = await CodeforcesClient.GetUser(username);
                if(responseUser.Status == "FAILED"){
                    Console.WriteLine($"User {username} does not exist");
                    able = false;
                }
            }
            return able;
        }

        public async Task<int> NextContest(string filter, int idx)
        {
            for (; idx < _contests.Count; idx++)
            {
                var contest = _contests[idx];
                if(contest.Phase == "FINISHED" && contest.Name.Contains(filter))
                {
                    bool able = true;
                    foreach (var user in _users)
                    {
                        var responseStatus = await CodeforcesClient.GetStatus(contest.Id, user);

                        if(responseStatus.Result.Count != 0){
                            able = false;
                            break;
                        }
                    }
                    if(able) return idx;
                }
            }
            return -1;
        }

        public void FindContest()
        {
            var filter = IO.GetFilter();
            var idx = 0;
            var nextContestIndex = NextContest(filter, idx);
            do
            {
                var contestIdx = nextContestIndex.GetAwaiter().GetResult();
                if(contestIdx == -1) {
                    IO.NotFound();
                    Environment.Exit(0);
                }
                idx = contestIdx+1;

                IO.DisplayContest(_contests[contestIdx]);

                nextContestIndex = NextContest(filter, idx);
            } while(IO.AnotherContest());
        }
    }
}
