using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        Usershelper helper = new Usershelper();

        // POST: api/Users
        [HttpPost]
        public IActionResult Register(Users u)
        {
            Response response = helper.UserRegistration(u);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Users/5
        [HttpPut("{userId}")]
        public IActionResult Update(int userId, Users u)
        {
            u.UserId = userId;
            Response response = helper.UpdateUser(u);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllUsers(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Users/5
        [HttpGet("{userId}")]
        public IActionResult GetById(int userId)
        {
            Response response = helper.GetAllUsers(userId);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Users/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            Response response = helper.DeleteUser(userId);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}