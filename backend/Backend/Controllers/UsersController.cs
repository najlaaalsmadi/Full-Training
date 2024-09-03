using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;
        private readonly  TokenGenerator _tokenGenerator;

        public UsersController(MyDbContext myDbContext, TokenGenerator tokenGenerator)
        {
            _myDbContext = myDbContext;
            _tokenGenerator = tokenGenerator;
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
            var roles =_myDbContext.UserRoles.Where(x => x.UserId == user.UserId).Select(ur => ur.Role).ToList();
            var token = _tokenGenerator.GenerateToken(user.Username, roles);
             return Ok(new { token, UserId = user.UserId });

            ////return Ok("User logged in successfully");
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



        // حذف مستخدم
        [HttpGet("{name}")]
        public IActionResult serch(string name)
        {
            var  user  = _myDbContext.Users.Where(x => x.Username == name).ToList();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);

        }
    }
}

