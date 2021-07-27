using Microsoft.AspNetCore.Http;

namespace ShopBridgeAPI.Models
{
    public class ProductImageModel
    {
        public IFormFile Content { get; set; }
    }
}