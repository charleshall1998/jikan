using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JikanAPI.Models
{
    public class OrderDetail
    {
        public Order Order { get; set; }
        [Key, Column(Order = 0)]
        public int? OrderId { get; set; }
        [JsonIgnore]
        public Watch Watch { get; set; }
        [Key, Column(Order = 1)]
        public int? WatchId { get; set; }

        [Required]
        public int Quantity { get; set; }
        
    }
}
