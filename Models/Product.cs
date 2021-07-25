using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Models
{
    public class Product
    {
        [Required, Key]
        public int ID { get; set; }
        [MaxLength(50, ErrorMessage = "Product name should not exceed 50 characters.")]
        public string productName { get; set; }
        [MaxLength(500, ErrorMessage = "Product description should not exceed 500 characters.")]
        public string productDescription { get; set; }
        public string productCategory { get; set; }
        public double productPrice { get; set; }
    }
}
