using Newtonsoft.Json;

namespace meetupsApi.JsonEntity
{
    public class ConnpassMeetupJson
    {
        public int results_returned { get; set; }
        [JsonProperty("events")] 
        public ConnpassEvent[] ConnpassEvents { get; set; }
        public int results_start { get; set; }
        public int results_available { get; set; }
    }
}