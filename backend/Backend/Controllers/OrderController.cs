using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

    
            private readonly MyDbContext _myDbContext;

            public OrderController(MyDbContext myDbContext)
            {
                _myDbContext = myDbContext;
            }

            // Get all products
            [HttpGet]
            public IActionResult GetAllOrders()
            {
                var Orders = _myDbContext.Orders.ToList();
                return Ok(Orders);
            }

            // Get product by ID
            [HttpGet("by ID Orders/{id}")]

            public IActionResult GetOrdersById(int id)
            {
                var Orders = _myDbContext.Orders.FirstOrDefault(a => a.OrderId == id);
                if (Orders == null)
                {
                    return NotFound();
                }
                return Ok(Orders);
            }

            // Get product by name
            [HttpGet("byname Orders/{OrderDate}")]
            public IActionResult GetOrdersByName(DateTime OrderDate)
            {
                var Orders = _myDbContext.Orders.FirstOrDefault(a => a.OrderDate == OrderDate);
                if (Orders == null)
                {
                    return NotFound();
                }
                return Ok(Orders);
            }

           
            [HttpDelete]
            public IActionResult Delete(int id)
            {
                var Orders = _myDbContext.Orders.Find(id);
            if (Orders == null)
                {
                    return NotFound();
                }
                _myDbContext.Orders.Remove(Orders);
                _myDbContext.SaveChanges();
                return Ok();
            }
        }
    }

