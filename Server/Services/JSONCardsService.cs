using Newtonsoft.Json.Linq;
using Cards.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace Server.Services
{
    public class JSONCardsService : ICardsService
    {
        string dataFile = "Data/cards.json";
        public async Task<Card> AddAsync(Card card)
        {
            string json = await File.ReadAllTextAsync(dataFile);
            JObject jObject = JObject.Parse(json);
            if (jObject == null)
                throw new Exception("File doesn`t exists");

            JArray cardsArrary = (JArray)jObject["cards"];

            
            card.Id = cardsArrary.Any() ? cardsArrary.Last()["id"].Value<int>() + 1 : 1;
            card.Path = "Data/cards/" + card.Id + ".jpg";

            using (FileStream fs = new FileStream(card.Path, FileMode.Create))
            {
                await fs.WriteAsync(card.File, 0, card.File.Length);
            }

            card.File = null;
            cardsArrary.Add(card.ToJSON());
            
            jObject["cards"] = cardsArrary;

            string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(dataFile, output);

            return card;
        }

        public async Task<Card> DeleteAsync(int id)
        {
            string json = await File.ReadAllTextAsync(dataFile);
            JObject jObject = JObject.Parse(json);

            if (jObject == null)
                throw new Exception("File doesn`t exists");

            JArray cardsArrary = (JArray)jObject["cards"];

            var cardToDelete = cardsArrary.FirstOrDefault(obj => obj["id"].Value<int>() == id);

            cardsArrary.Remove(cardToDelete);

            jObject["cards"] = cardsArrary;
            string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            File.WriteAllText(dataFile, output);

            Card card = new Card(cardToDelete.ToString());

            return card;
        }

        public async Task<Card> UpdateAsync(Card card)
        {
            string json = await File.ReadAllTextAsync(dataFile);
            JObject jObject = JObject.Parse(json);

            if (jObject == null)
                throw new Exception("File doesn`t exists");

            JArray cardsArrary = (JArray)jObject["cards"];

            JToken cardToEdit = cardsArrary.FirstOrDefault(obj => obj["id"].Value<int>() == card.Id);


            if (cardToEdit != null)
            {
                card.Path = "Data/cards/" + card.Id + ".jpg";

                using (FileStream fs = new FileStream(card.Path, FileMode.Create))
                {
                    await fs.WriteAsync(card.File, 0, card.File.Length);
                }

                card.File = null;
                cardToEdit.Replace(card.ToJSON());
                jObject["cards"] = cardsArrary;
                string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                File.WriteAllText(dataFile, output);
            }

            return card;
        }

        public async Task<IEnumerable<Card>> GetAsync()
        {
            List<Card> cards = new List<Card>();

            string json = await File.ReadAllTextAsync(dataFile);      
            JObject jObject = JObject.Parse(json);

            if (jObject == null)
                throw new Exception("File doesn`t exists");

            JArray cardsArrary = (JArray)jObject["cards"];
            foreach (var item in cardsArrary)
            {
                Card card = new Card(item.ToString());
                cards.Add(card);
            }

            return cards;

        }

        public async Task<Card> GetAsync(int id)
        {
            IEnumerable<Card> cards = new List<Card>();

            string json = await File.ReadAllTextAsync(dataFile);
            JObject jObject = JObject.Parse(json);

            if (jObject == null)
                throw new Exception("File doesn`t exists");

            JArray cardsArrary = (JArray)jObject["cards"];

            JToken jCard = cardsArrary.FirstOrDefault(obj => obj["id"].Value<int>() == id);

            if (jCard == null)
                throw new ArgumentNullException("Argument with this Id does not exists");

            Card card = new Card(jCard.ToString());

            return card;

        }
    }
}
