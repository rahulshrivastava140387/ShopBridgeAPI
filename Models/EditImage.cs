using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Models
{
    public class EditImage : UploadImage
    {
        public int productID { get; set; }
        public string existingImage { get; set; }
    }
}
