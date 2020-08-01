using Cards.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    class JSONCardsDataStore : IDataStore<Card>
    {
        private static readonly IDataStore<Card> instance = new JSONCardsDataStore();
        private static readonly HttpClient client = new HttpClient();

        public string URL = ConfigurationManager.AppSettings["CardsAPI"].ToString();

        Task<Card> AddAsync(Card card)
        {
            throw new NotImplementedException();
        }

        async Task<Card> IDataStore<Card>.AddAsync(Card card)
        {
            var json = JsonConvert.SerializeObject(card);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(URL + "/add", content);

            var answ = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Card>(answ);
        }

        async Task<Card> IDataStore<Card>.DeleteAsync(int id)
        {
            var response = await client.DeleteAsync(URL + "/delete/" + id.ToString());

            var answ = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Card>(answ);
        }

        async Task<IEnumerable<Card>> IDataStore<Card>.GetAsync()
        {
            var response = await client.GetAsync(URL + "get");
            var content = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<IEnumerable<Card>>(content);
        }

        static IDataStore<Card> GetInstance() => instance; 
        async Task<Card> IDataStore<Card>.GetAsync(int id)
        {
            var response = await client.GetAsync(URL + "get/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Card>(content);
        }

        async Task<Card> IDataStore<Card>.UpdateAsync(Card card)
        {
            var json = JsonConvert.SerializeObject(card);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(URL + "/update", content);

            var answ = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Card>(answ);
        }
    }
}
