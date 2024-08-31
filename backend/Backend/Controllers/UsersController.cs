using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
            private readonly MyDbContext _myDbContext;

            public UsersController(MyDbContext myDbContext)
            {
                _myDbContext = myDbContext;
            }

            // Get all products
            [HttpGet]
            public IActionResult GetAllUsers()
            {
                var Users = _myDbContext.Users.ToList();
                return Ok(Users);
            }

            // Get product by ID
            [HttpGet("by ID Users/{id}")]

            public IActionResult GetUserById(int id)
            {
                var Users = _myDbContext.Users.FirstOrDefault(a => a.UserId == id);
                if (Users == null)
                {
                    return NotFound();
                }
                return Ok(Users);
            }

            // Get product by name
            [HttpGet("byname Users/{name}")]
            public IActionResult GetUsersByName(string name)
            {
                var Users = _myDbContext.Users.FirstOrDefault(a => a.Username == name);
                if (Users == null)
                {
                    return NotFound();
                }
                return Ok(Users);
            }

           
            [HttpDelete]
            public IActionResult Delete(int id)
            {
                var Users = _myDbContext.Users.Find(id);
                if (Users == null)
                {
                    return NotFound();
                }
                _myDbContext.Users.Remove(Users);
                _myDbContext.SaveChanges();
                return Ok();
            }
    [HttpPost]
    public IActionResult AddUser([FromForm] UsersDTO users)
        {
            var u = new User
            {
                Username = users.Username,
                Password = users.Password,
                Email = users.Email
            };

            _myDbContext.Users.Add(u);
            _myDbContext.SaveChanges();


            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UPDateuser(int id, [FromForm] UsersDTO users) { 
        var find= _myDbContext.Users.FirstOrDefault(a=>a.UserId==id);
            find.Username=users.Username;
            find.Password=users.Password;
            find.Email=users.Email;

        return Ok();
        }

        }
    }



