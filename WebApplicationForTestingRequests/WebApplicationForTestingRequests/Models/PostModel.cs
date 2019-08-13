using Newtonsoft.Json;

namespace ConsoleAppTestingRequests
{
    public class PostModel
    {
        [JsonProperty("post")]
        public string Post { get; set; }
        [JsonProperty("NumberOfRequest")]
        public int NumberOfRequest { get; set; }
    }
}
