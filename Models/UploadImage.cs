using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopBridgeAPI.Models
{
    public class UploadImage
    {
        public IFormFile uploadedImage { get; set; }
    }
}
