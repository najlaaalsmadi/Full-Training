using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        // تسجيل مستخدم جديد
        [HttpPost("register")]
        public IActionResult Register([FromForm] UsersDTO model)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHasher.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                Username = model.UserName,
                Password = model.Password, 
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = model.Email
            };

             _myDbContext.Users.Add(user);
            _myDbContext.SaveChanges();

            return Ok(user);
        }

        // تسجيل الدخول
        [HttpPost("login")]
        public IActionResult Login([FromForm] UsersDTO model)
        {
            var user =  _myDbContext.Users.FirstOrDefault(x => x.Username == model.UserName);

            if (user == null || !PasswordHasher.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok("User logged in successfully");
        }

        // تحديث بيانات مستخدم
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromForm] UsersDTO model)
        {
            var user = _myDbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Username = model.UserName;
            user.Email = model.Email;

            // إذا تم تقديم كلمة مرور جديدة، قم بتحديثها
            if (!string.IsNullOrEmpty(model.Password))
            {
                byte[] passwordHash, passwordSalt;
                PasswordHasher.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _myDbContext.Users.Update(user);
            _myDbContext.SaveChangesAsync();

            return Ok(user);
        }

        // حذف مستخدم
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user =  _myDbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            _myDbContext.Users.Remove(user);
            _myDbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}
