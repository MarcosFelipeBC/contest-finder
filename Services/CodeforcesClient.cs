using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContestFinder.Models;
using Newtonsoft.Json;

namespace ContestFinder.Services
{
    public static class CodeforcesClient
    {
        private static TimeSpan _timeout = System.Threading.Timeout.InfiniteTimeSpan;
        private static string _baseUri = "https://codeforces.com/api/";
        public static readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(_baseUri), Timeout = TimeSpan.FromSeconds(10)};

        public static async Task < CFResponse<List<ContestDefinition>> > GetContestsList(bool gym)
        {
            string path = $"contest.list?gym={gym}";
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, path))
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject< CFResponse< List<ContestDefinition> > >(content);
            }
        }

        public static async Task < CFResponse<List<object>> > GetStatus(int id, string username)
        {
            string path = $"contest.status?contestId={id}&handle={username}&count=1";
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, path))
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject< CFResponse< List<object> > >(content);
            }
        }

        public static async Task <CFResponse<object>> GetUser(string username)
        {
            string path = $"user.status?handle={username}&from=1&count=1";
            using(HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, path))
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CFResponse<object>>(content);
            }
        }
    }
}