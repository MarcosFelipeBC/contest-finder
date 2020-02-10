using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ContestFinder.Models;
using ContestFinder.Communication;
using ContestFinder.Services;


namespace ContestFinder
{
    public class App
    {    
        private List<ContestDefinition> _contests;
        private int _idx;
        private List<String> _users;
        public App()
        {
            var responseContests = CodeforcesClient.GetContestsList(false);
            _users = IO.GetUsers();

            var response = responseContests.GetAwaiter().GetResult();

            if(response.Status == "FAILED")
            {
                return ;
            }

            _contests = response.Result;    
        }

        public async Task<ContestDefinition> NextContest(string filter)
        {
            for (; _idx < _contests.Count; _idx++)
            {
                var contest = _contests[_idx];
                if(contest.Phase == "FINISHED" && contest.Name.Contains(filter)){
                    bool able = true;
                    foreach (var user in _users)
                    {
                        var responseStatus = await CodeforcesClient.GetStatus(contest.Id, user);

                        if(responseStatus.Result.Count != 0){
                            able = false;
                            break;
                        }
                    }
                    if(able) return contest;
                }
            }
            return new ContestDefinition();
        }

        public void FindContest(string filter)
        {
            var nextContest = NextContest(filter);
            do
            {
                var contest = nextContest.GetAwaiter().GetResult();
                IO.DisplayContest(contest);
                _idx++;
                nextContest = NextContest(filter);
            }while(IO.AnotherContest());
        }
    }
}
