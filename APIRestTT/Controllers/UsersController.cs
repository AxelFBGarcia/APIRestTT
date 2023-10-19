using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIRestTT.Models;
using APIRestTT.Tools;
using Azure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;

namespace APIRestTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBTecnicTestContext _context;

        public UsersController(DBTecnicTestContext _context)
        {
            this._context = _context;
        }

        class createUser
        {

        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> userLogin([FromBody] User user)
        {
            try
            {
                String password = Password.hashPassword(user.PasswordHash);
                var isAct = _context.Users.Where(u => u.LoginName == user.LoginName).FirstOrDefault();
                var dbUser = _context.Users.Where(u => u.LoginName == user.LoginName && u.PasswordHash == password).Select(u => new
                {
                    u.UserID,
                    u.LoginName
                }).FirstOrDefault();
                if(isAct != null) {
                    if (dbUser != null && isAct.Active != 1)
                    {
                        isAct.Active = 1;
                        await _context.SaveChangesAsync();
                        return Ok(dbUser);

                    }
                    else if (isAct.Active == 1)
                    {
                        return BadRequest("User has alredy loged on");
                    }
                    return BadRequest("Username or password is incorrect");
                }

                return BadRequest("Username or password is incorrect");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> userReg([FromBody] User user)
        {
            try
            {
                var dbUser = _context.Users.Where(u => u.LoginName == user.LoginName).FirstOrDefault();
                if (dbUser != null)
                {
                    return BadRequest("Username alredy registered in database");
                }
                if (user.FirstName == null || user.LoginName == null || user.PasswordHash == null || user.FirstName == null || user.LastName == null)

                user.PasswordHash = Password.hashPassword(user.PasswordHash);
                user.Active = 0;
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok("User registered succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]
        [Route("changePassw")]
        public async Task<IActionResult> chngPass([FromBody] User user)
        {
            try
            {
                var dbUser = _context.Users.Where(u => u.LoginName == user.LoginName).FirstOrDefault();
                if (dbUser != null)
                {
                    dbUser.PasswordHash = Password.hashPassword(user.PasswordHash);
                    dbUser.Active = 1;
                    await _context.SaveChangesAsync();
                    return BadRequest("User password updated succesfully");
                }

                return Ok("Username isn't registered in database ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("usrLogOut")]
        public async Task<IActionResult> useLogOut([FromBody] User user)
        {
            try
            {
                var dbUser = _context.Users.Where(u => u.LoginName == user.LoginName).FirstOrDefault();
                dbUser.Active = 0;
                await _context.SaveChangesAsync();
                return BadRequest("User LogOut succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static string GetChanges(EntityEntry entity)
        {
            var changes = new StringBuilder();
            foreach (var property in entity.OriginalValues.Properties)
            {
                var originalValue = entity.OriginalValues[property];
                var currentValue = entity.CurrentValues[property];
                if (!Equals(originalValue, currentValue))
                {
                    changes.AppendLine($"{property.Name}: From '{originalValue}' to '{currentValue}'");
                }
            }
            return changes.ToString();
        }
    }
}