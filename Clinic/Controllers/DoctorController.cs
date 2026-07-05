using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        Doctorhelper helper = new Doctorhelper();

        // POST: api/Doctor/Register
        [HttpPost("")]
        public IActionResult Register(Doctor d)
        {
            Response response = helper.DoctorRegistration(d);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Doctor/Update/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Doctor d)
        {
            d.DoctorId = id;
            Response response = helper.UpdateDoctor(d);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Doctor/GetAll
        [HttpGet("")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllDoctors(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Doctor/GetById/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllDoctors(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Doctor/Delete/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeleteDoctor(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}