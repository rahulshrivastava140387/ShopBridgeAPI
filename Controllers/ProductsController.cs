using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridgeAPI.Data;
using ShopBridgeAPI.Models;
using System.Drawing.Imaging;

namespace ShopBridgeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductAPIContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ProductAPIContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
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
                existingImage = product.productImage
            };

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutProduct(int id, ProductAPI product)
        {
            if (id != product.productID)
            {
                return BadRequest();
            }

            var objProduct = await _context.Product.FindAsync(id);
            objProduct.productName = product.productName;
            objProduct.productDescription = product.productDescription;
            objProduct.productCategory = product.productCategory;
            objProduct.productPrice = product.productPrice;
            objProduct.productQuantity = product.productQuantity;

            if (product.uploadedImage != null)
            {
                if (product.existingImage != null)
                {
                    if (System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", product.existingImage)))
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", product.existingImage);
                        System.IO.File.Delete(filePath);
                    }
                }

                objProduct.productImage = ProcessUploadedFile(product);
            }

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
        [HttpPost]        
        public async Task<ActionResult<Product>> PostProduct(ProductAPI product)
        {
            try
            {
                string uniqueFileName = ProcessUploadedFile(product);

                Product objProduct = new Product()
                {
                    productName = product.productName,
                    productDescription = product.productDescription,
                    productCategory = product.productCategory,
                    productPrice = product.productPrice,
                    productQuantity = product.productQuantity,
                    productImage = uniqueFileName
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

            var currentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", product.productImage);

            _context.Product.Remove(product);

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

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.productID == id);
        }

        private string ProcessUploadedFile(ProductAPI model)
        {
            string uniqueFileName = null;

            if (model.uploadedImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.uploadedImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.uploadedImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
