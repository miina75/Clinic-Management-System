using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        Doctorhelper helper = new Doctorhelper();

        // POST: api/Patient/Register
        [HttpPost("Register")]
        public IActionResult Register(Doctor d)
        {
            Response response = helper.DoctorRegistration(d);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Patient/Update/5
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, Doctor d)
        {
            d.DoctorId = id;
            Response response = helper.UpdateDoctor(d);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Patient/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllDoctors(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Patient/GetById/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllDoctors(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Patient/Delete/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeleteDoctor(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}