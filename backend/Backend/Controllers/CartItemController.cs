using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;
        public CartItemController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult GetAlldata()
        {
            try
            {
                // استعلام البيانات من قاعدة البيانات
                var getData = _myDbContext.CartItems.Select(x =>
                new CartItemsResponseDTO
                {
                    CartId = x.CartId,
                    CartItemId = x.CartItemId,
                    Quantity = x.Quantity,
                    productRequestDTO = new ProductRequestDTO1
                    {
                        ProductName = x.Product.ProductName,
                        Description = x.Product.Description,
                        Price = x.Product.Price,
                        CategoryId = x.Product.CategoryId,
                    }
                }).ToList();

                if (getData == null || !getData.Any())
                {
                    // التعامل مع الحالة عندما لا تكون هناك بيانات
                    return NotFound(new { message = "No data found." });
                }

                // إرجاع البيانات بنجاح
                return Ok(getData);
            }
            catch (Exception ex)
            {
                // التعامل مع الأخطاء العامة
                return StatusCode(500, new { message = "An error occurred while retrieving data", error = ex.Message });
            }
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult Get(int id)
        {
            var getData = _myDbContext.CartItems.Select(x =>
            new CartItemsResponseDTO
            {
                CartId = x.CartId,
                CartItemId = x.CartItemId,
                Quantity=x.Quantity,
                productRequestDTO = new ProductRequestDTO1
                {
                    ProductName = x.Product.ProductName,
                    Description = x.Product.Description,
                    Price = x.Product.Price,
                    CategoryId = x.Product.CategoryId,

                }
            }
            ).ToList();

            return Ok(getData);
        }
        [HttpPost]
        public IActionResult addcart([FromBody]AddcardItemrequest cart)
        {
            var data = new CartItem
            {
                CartId = cart.CartId,
                Quantity = cart.Quantity,
                ProductId = cart.ProductId,
            };
            _myDbContext.CartItems.Add(data);
            _myDbContext.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var card = _myDbContext.CartItems.Find(id);
            if (card == null)
            {
                return NotFound();
            }
            _myDbContext.CartItems.Remove(card);
            _myDbContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CartItemsupdate cartUpdate)
        {
            // العثور على العنصر المطلوب تحديثه
            var find = _myDbContext.CartItems.FirstOrDefault(c => c.CartItemId == id);

            if (find == null)
            {
                return NotFound("Cart item not found.");
            }

            // تحديث الكمية إذا كانت موجودة
            if (cartUpdate.Quantity.HasValue)
            {
                find.Quantity = cartUpdate.Quantity.Value;
            }

            // تحديث العنصر في قاعدة البيانات
            _myDbContext.CartItems.Update(find);
            _myDbContext.SaveChanges();

            return Ok();
        }



    }
}
