using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridgeAPI.Models
{
    public class Product
    {
        [Required, Key]
        public int productID { get; set; }
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
        public Nullable<double> productPrice { get; set; }
        [Required]
        public Nullable<double> productQuantity { get; set; }
        public string productImage { get; set; }
        public bool inStock { get; set; }
        public bool isDeleted { get; set; }
    }
}
