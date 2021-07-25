using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Models
{
    public class ProductAPI : EditImage
    {
        [Required]
        [StringLength(100)]
        public string productName { get; set; }
        [Required]
        [StringLength(500)]
        public string productDescription { get; set; }
        [Required]
        [StringLength(100)]
        public string productCategory { get; set; }
        [Required]
        public double productPrice { get; set; }
        [Required]
        public double productQuantity { get; set; }
    }
}
