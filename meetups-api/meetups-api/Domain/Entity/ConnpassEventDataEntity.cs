using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetupsApi.Domain.Entity
{
    public class ConnpassEventDataEntity
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string event_url { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}