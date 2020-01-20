using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using contests_finder.Models;


namespace contests_finder
{
    public class App
    {
        private static TimeSpan _timeout = System.Threading.Timeout.InfiniteTimeSpan;
        private static string _baseUri = "https://codeforces.com/api/";
        public static readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(_baseUri), Timeout = TimeSpan.FromSeconds(10)};
        
        public List < string > GetUsers()
        { 
            Console.Write("How many users are going to do the constest? ");
            int amount = Convert.ToInt32(Console.ReadLine());
            List <string> users = new List<string>();
            for (int i=0; i<amount; i++){
                users.Add(Console.ReadLine());
            }
            return users;
        }

        public async Task < CFResponse< List<ContestDefinition>> > GetContests(bool gym)
        {
            string path = $"contest.list?gym={gym}";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
            var response = await _httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject< CFResponse< List<ContestDefinition> > >(content);
        }

        public async Task < CFResponse< List<object> > > GetStatus(int id, string user)
        {
            string path = $"contest.status?contestId={id}&handle={user}&count=1";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
            var response = await _httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject< CFResponse< List<object> > >(content);
        }

        public async Task FindContest(string filter)
        {
            var users = GetUsers();
            var responseContests = await GetContests(false);
            if(responseContests.Status == "FAILED") return ;
            var contests = responseContests.Result;
            
            foreach(var contest in contests)
            {
                if(contest.Phase == "FINISHED" && contest.Name.Contains(filter)){
                    bool able = true;
                    foreach (var user in users)
                    {
                        var responseStatus = await GetStatus(contest.Id, user);
                        if(responseStatus.Result.Count != 0){
                            able = false;
                            break;
                        }
                    }
                    if(able) Console.WriteLine($"Id = {contest.Id} || Name = {contest.Name}");
                }
            }
        }
    }
}
