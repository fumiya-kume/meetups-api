using System;
using System.ComponentModel.DataAnnotations;

namespace meetupsApi.JsonEntity
{
    public class Event
    {
        [Key]
        public int event_id { get; set; }
        public string event_url { get; set; }
        public string event_type { get; set; }
        public string owner_nickname { get; set; }
        public Series series { get; set; }
        public DateTime updated_at { get; set; }
        public string lat { get; set; } = "";
        public DateTime started_at { get; set; }
        public string hash_tag { get; set; }
        public string title { get; set; }
        public string lon { get; set; } = "";
        public int waiting { get; set; }
        public int? limit { get; set; } = 0;
        public int owner_id { get; set; }
        public string owner_display_name { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string _catch { get; set; }
        public int accepted { get; set; }
        public DateTime ended_at { get; set; }
        public string place { get; set; }
    }


}
