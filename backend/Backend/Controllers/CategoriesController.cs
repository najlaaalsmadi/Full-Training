using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;

        public CategoriesController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        // Get all categories
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _myDbContext.Categories.ToList();
            return Ok(categories);
        }

        // Get category by ID
        [HttpGet("byIDCategory/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _myDbContext.Categories.FirstOrDefault(a => a.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // Get category by name
        [HttpGet("bynameCategory/{name}")]

        public IActionResult GetCategoryByName(string name)
        {
            var category = _myDbContext.Categories.FirstOrDefault(a => a.CategoryName == name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _myDbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _myDbContext.Categories.Remove(category);
            _myDbContext.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategories([FromForm] CategoriesDTO category)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            // التحقق من وجود ملف الصورة
            if (category.CategoryImage == null || category.CategoryImage.Length == 0)
            {
                return BadRequest("Please upload an image file.");
            }

            var extension = Path.GetExtension(category.CategoryImage.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file type. Please upload a jpg, jpeg, or png image.");
            }

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var imgfile = Path.Combine(uploadFolder, category.CategoryImage.FileName);

            // استخدام await مع عملية النسخ
            using (var stream = new FileStream(imgfile, FileMode.Create))
            {
                await category.CategoryImage.CopyToAsync(stream);
            }

            var c = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage.FileName,
            };

            _myDbContext.Categories.Add(c);
            await _myDbContext.SaveChangesAsync(); // استخدام await لحفظ البيانات

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategories(int id, [FromForm] CategoriesDTO category)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            // البحث عن الفئة في قاعدة البيانات باستخدام المعرف (id)
            var find = await _myDbContext.Categories.FindAsync(id);
            if (find == null)
            {
                return NotFound("Category not found.");
            }

            // التحقق من وجود ملف الصورة
            if (category.CategoryImage == null || category.CategoryImage.Length == 0)
            {
                return BadRequest("Please upload an image file.");
            }

            var extension = Path.GetExtension(category.CategoryImage.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file type. Please upload a jpg, jpeg, or png image.");
            }

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var imgfile = Path.Combine(uploadFolder, category.CategoryImage.FileName);

            // استخدام await مع عملية النسخ
            using (var stream = new FileStream(imgfile, FileMode.Create))
            {
                await category.CategoryImage.CopyToAsync(stream);
            }

            // تحديث بيانات الفئة
            find.CategoryImage = category.CategoryImage.FileName;

            _myDbContext.Categories.Update(find);

            // استخدام await لحفظ التغييرات في قاعدة البيانات
            await _myDbContext.SaveChangesAsync();

            return Ok();
        }




    }
}
