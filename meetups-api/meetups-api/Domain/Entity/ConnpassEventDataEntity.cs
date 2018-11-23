using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace meetupsApi.Domain.Entity
{
    public class ConnpassEventDataEntity
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string catchMesagge { get; set; }
        public string description { get; set; }
        public string event_url { get; set; }
        public string hash_tag { get; set; }
        public DateTime started_at { get; set; }
        public DateTime ended_at { get; set; }
        public int limit { get; set; }
        public string event_type { get; set; }
        public string address { get; set; }
        public int owned_id { get; set; }
        public string owned_nickname { get; set; }
        public string owner_display_name { get; set; }
        public int accepted { get; set; }
        public int waiting { get; set; }
        public DateTime updated_at { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}