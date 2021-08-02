using JikanAPI.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JikanAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        
        public User Purchaser { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
