using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridgeAPI.Models
{
    public class ProductAPI : EditImage
    {        
        [StringLength(100)]
        public string productName { get; set; }        
        [StringLength(500)]
        public string productDescription { get; set; }        
        [StringLength(100)]
        public string productCategory { get; set; }        
        public Nullable<double> productPrice { get; set; }        
        public Nullable<double> productQuantity { get; set; }
        public bool inStock { get; set; }
    }
}
