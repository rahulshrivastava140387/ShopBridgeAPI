using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridgeAPI.Data;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Common;

namespace ShopBridgeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private List<Product> ProductList { get; set; }
        private readonly ProductAPIContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ProductAPIContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            SeedProduct();
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            if (_context.Product.ToList().Count > 0)
                return await _context.Product.ToListAsync();
            else
                return ProductList.AsEnumerable().ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            var productAPI = new ProductAPI()
            {
                productID = product.productID,
                productName = product.productName,
                productDescription = product.productDescription,
                productCategory = product.productCategory,
                productPrice = product.productPrice,
                productQuantity = product.productQuantity,
                existingImage = product.productImage,
                inStock = product.inStock
            };

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> PutProduct(int id, ProductAPI product)
        {
            var objProduct = await _context.Product.FindAsync(id);
            objProduct.productName = product.productName == null || product.productName == "" ? objProduct.productName : product.productName;
            objProduct.productDescription = product.productDescription == null || product.productDescription == "" ? objProduct.productDescription : product.productDescription;
            objProduct.productCategory = product.productCategory == null || product.productCategory == "" ? objProduct.productCategory : product.productCategory;
            objProduct.productPrice = product.productPrice == null ? objProduct.productPrice : product.productPrice;
            objProduct.productQuantity = product.productQuantity == null ? objProduct.productQuantity : product.productQuantity;

            _context.Entry(objProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, RequestSizeLimit(long.MaxValue)]        
        public async Task<ActionResult<Product>> PostProduct(ProductAPI product)
        {
            try
            {
                Product objProduct = new Product()
                {
                    productName = product.productName,
                    productDescription = product.productDescription,
                    productCategory = product.productCategory,
                    productPrice = product.productPrice,
                    productQuantity = product.productQuantity,
                    productImage = "",
                    inStock = true,
                    isDeleted = false
                };

                _context.Product.Add(objProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new IndexOutOfRangeException();
            }            

            return CreatedAtAction("GetProduct", product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.isDeleted = true;

            _context.Entry(product).State = EntityState.Modified;

            var currentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", product.productImage);

            if (await _context.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(currentImage))
                {
                    System.IO.File.Delete(currentImage);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Route("uploadimage/{productId}"), HttpPost, RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> UploadProductImage([FromRoute] int productId, [FromForm] UploadImage postForm)
        {
            var objProduct = await _context.Product.FindAsync(productId);

            Document document = new Document
            {
                Name = $"{Guid.NewGuid()}.{Path.GetExtension(postForm.uploadedImage.FileName)}",
                Content = postForm.uploadedImage,
                DirectoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads")
            };

            await DocumentManager.Save(document);

            objProduct.productImage = document.FileFullPath;

            _context.Entry(objProduct).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Content("Image uploaded successfully.");
        }

        [Route("addstock/{productId}/{quantity}"), HttpPatch]
        public async Task<IActionResult> AddProductStock([FromRoute] int productId, [FromRoute] double quantity)
        {
            var objProduct = await _context.Product.FindAsync(productId);
            Nullable<double> existingQuantity = objProduct.productQuantity;

            objProduct.productQuantity = existingQuantity + quantity;

            _context.Entry(objProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Content("Stock updated successfully.");
        }

        [Route("booking/{productId}/{quantity}"), HttpPatch]
        public async Task<IActionResult> BookProduct([FromRoute] int productId, [FromRoute] double quantity)
        {
            var objProduct = await _context.Product.FindAsync(productId);
            Nullable<double> existingQuantity = objProduct.productQuantity;

            if (existingQuantity > quantity)
            {
                objProduct.productQuantity = existingQuantity - quantity;

                _context.Entry(objProduct).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Content("Product booked successfully.");
            }
            else
            {
                return Content("Booking quantity is more than the available quantity.");
            }           
        }

        [Route("outofstock/{productId}"), HttpPatch]
        public async Task<IActionResult> OutOfStockProduct([FromRoute] int productId)
        {
            var objProduct = await _context.Product.FindAsync(productId);

            objProduct.inStock = false;

            _context.Entry(objProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        } 

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.productID == id);
        }

        private void SeedProduct()
        {
            ProductList = new List<Product>();

            ProductList.Add(new Product()
            {
                productID = 1,
                productName = "Samsung S6 edge",
                productDescription = "This is Samsung S6 edge smartphone.",
                productPrice = 25000,
                productCategory = "Mobile",
                productQuantity = 500,                
                productImage = "http://www.samsung.com/global/galaxy/galaxys6/galaxy-s6-edge/images/galaxy-s6-edge_gallery_front_black.png",
                inStock = true,
                isDeleted = false
            });

            ProductList.Add(new Product()
            {
                productID = 2,
                productName = "Samsung S6 edge plus",
                productDescription = "This is Samsung S6 edge plus smartphone.",
                productCategory = "Mobile",
                productPrice = 30000,
                productQuantity = 500,                
                productImage = "http://www.samsung.com/global/galaxy/galaxys6/galaxy-s6-edge/images/galaxy-s6-edge_gallery_front_black.png",
                inStock = true,
                isDeleted = false
            });

            ProductList.Add(new Product()
            {
                productID = 3,
                productName = "Apple iPad",
                productDescription = "This is a revolutionary product!",
                productCategory = "Tablets",
                productPrice = 130000,
                productQuantity = 500,                
                productImage = "https://www.bhphotovideo.com/images/images2500x2500/apple_ml0t2ll_a_256gb_ipad_pro_wi_fi_1241236.jpg",
                inStock = true,
                isDeleted = false
            });

            ProductList.Add(new Product()
            {
                productID = 4,
                productName = "Apple iPad Pro",
                productDescription = "This is just another revolutionary product!",
                productCategory = "Tablets",
                productPrice = 140000,
                productQuantity = 500,                
                productImage = "https://www.bhphotovideo.com/images/images2500x2500/apple_ml0t2ll_a_256gb_ipad_pro_wi_fi_1241236.jpg",
                inStock = true,
                isDeleted = false
            });

            ProductList.Add(new Product()
            {
                productID = 5,
                productName = "LG G-Pad",
                productDescription = "This is a tablet from LG",
                productCategory = "Tablets",
                productPrice = 120000,
                productQuantity = 500,                
                productImage = "http://www.lg.com/uk/images/tablets/v480/gallery/medium01-1.jpg",
                inStock = true,
                isDeleted = false
            });            
        }
    }
}
