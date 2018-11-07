using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meetupsApi.JsonEntity
{
    public class Series
    {
        [Key]
        public int id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public ICollection<Event> Events { get; set; }
    }


}
