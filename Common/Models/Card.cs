using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cards.Models
{
    [DataContract]
    public class Card
    {
        public Card()
        {

        }
        public Card(string json)
        {
            JToken jObject = JObject.Parse(json);
            Id = (int)jObject["id"];
            Description = (string)jObject["description"];
            Path = (string)jObject["path"];

            try
            {
                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);

                byte[] imgByteArr = new byte[fs.Length];

                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                File = imgByteArr;
            }
            catch
            {
                
            }
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("path")]
        public string Path { get;set;}
        [JsonProperty("file",NullValueHandling = NullValueHandling.Ignore)]
        public byte[] File { get; set; }

        public JObject ToJSON()
        {
            return JObject.Parse(JsonConvert.SerializeObject(this));
        }
    }
}
