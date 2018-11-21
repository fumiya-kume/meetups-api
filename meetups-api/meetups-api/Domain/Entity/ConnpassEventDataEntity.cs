using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meetupsApi.Domain.Entity
{
    public class ConnpassEventDataEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string EventUrl { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}