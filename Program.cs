using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace contests_finder
{

    [DataContract]
    public class CFResponse<T>
    {
        [DataMember(Name="status")]
        public string Status { get; set; }

        [DataMember(Name="result")]
        public List<T> Contests { get; set; }
    }

    class Program
    {
        private static TimeSpan _timeout = System.Threading.Timeout.InfiniteTimeSpan;
        private static string _baseUri = "https://codeforces.com/api/";
        public static readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(_baseUri), Timeout = _timeout};
        private List < string > GetUsers()
        { 
            Console.Write("How many users are going to do the constest? ");
            int amount = Convert.ToInt32(Console.ReadLine());
            List <string> users = new List<string>();
            for (int i=0; i<amount; i++){
                users.Add(Console.ReadLine());
            }
            return users;
        }

        private async Task < CFResponse<ContestDefinition> > GetContests(bool gym){

            Console.WriteLine("pei");
            string path = $"contest.list?gym={gym}";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, path);

            Console.WriteLine("pow");
            var response = await _httpClient.SendAsync(requestMessage);
            Console.WriteLine("pa");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject< CFResponse<ContestDefinition> >(content);
        }
        
        static async Task Main(string[] args)
        {
            Program p = new Program();
            
            var users = p.GetUsers();
            
            var response = await p.GetContests(false);
            var contests = response.Contests;

        }
    }
}
