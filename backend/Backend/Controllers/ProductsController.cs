using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public ProductsController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        // Get all products
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _myDbContext.Products.ToList();
            return Ok(products);
        }

        // Get product by ID
        [HttpGet("byIDProduct/{id}")]

        public IActionResult GetProductById(int id)
        {
            var product = _myDbContext.Products.FirstOrDefault(a => a.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Get product by name
        [HttpGet("bynameProduct")]
        public IActionResult GetProductByName()
        {
            var lastFiveProducts = _myDbContext.Products.OrderByDescending(a => a.ProductName).Take(5)                                  .ToList();                               

            if (lastFiveProducts == null)
            {
                return NotFound();
            }
            return Ok(lastFiveProducts);
        }

        [HttpGet("prodectbycategoryId/{category_id}")]
        public IActionResult GetProductbycategory(int category_id)
        {
            var Product = _myDbContext.Products.Where(x => x.CategoryId == category_id).ToList();
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Products = _myDbContext.Products.Find(id);
            if (Products == null)
            {
                return NotFound();
            }
            _myDbContext.Products.Remove(Products);
            _myDbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateProduct([FromForm] ProductDTO Product)
        {
            // التحقق من تنسيق الصورة
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(Product.ProductImage.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file type. Please upload a jpg, jpeg, or png image.");
            }

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var imgfile = Path.Combine(uploadFolder, Product.ProductImage.FileName);
            using (var stream = new FileStream(imgfile, FileMode.Create))
            {
                Product.ProductImage.CopyToAsync(stream);
            }

            var P = new Product
            {
                ProductName = Product.ProductName,
                Description = Product.Description,
                Price = Product.Price,
                CategoryId = Product.CategoryId,
                ProductImage = Product.ProductImage.FileName,
            };

            _myDbContext.Products.Add(P);
            _myDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromForm] ProductDTO Product)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            // التحقق من وجود ملف الصورة
            if (Product.ProductImage == null || Product.ProductImage.Length == 0)
            {
                return BadRequest("Please upload an image file.");
            }

            var extension = Path.GetExtension(Product.ProductImage.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file type. Please upload a jpg, jpeg, or png image.");
            }
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var imgfile = Path.Combine(uploadFolder, Product.ProductImage.FileName);
            using (var stream = new FileStream(imgfile, FileMode.Create))
            {
                Product.ProductImage.CopyToAsync(stream);
            }
            var find = _myDbContext.Products.FirstOrDefault(c => c.ProductId == id);


            find.ProductName = Product.ProductName;
            find.Description = Product.Description;
            find.Price = Product.Price;
            if (Product.ProductImage == null)
            {
                find.ProductImage = find.ProductImage;

            }
            find.ProductImage = Product.ProductImage.FileName;
            find.CategoryId= Product.CategoryId;
           

            _myDbContext.Products.Update(find);
            _myDbContext.SaveChanges();
            return Ok();
        }


    }
}
