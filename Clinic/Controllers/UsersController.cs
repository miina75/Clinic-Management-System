using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        Usershelper helper = new Usershelper();

        // POST: api/Patient/Register
        [HttpPost("Register")]
        public IActionResult Register(Users u)
        {
            Response response = helper.UserRegistration(u);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Patient/Update/5
        [HttpPut("Update/{UserId}")]
        public IActionResult Update(int UserId, Users u)
        {
            u.UserId = UserId; 
            Response response = helper.UpdateUser(u);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Patient/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllUsers(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Patient/GetById/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllUsers(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Patient/Delete/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeleteUser(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}