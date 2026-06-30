using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        Patienthelper helper = new Patienthelper();

        // POST: api/Patient/Register
        [HttpPost("Register")]
        public IActionResult Register(Patient p)
        {
            Response response = helper.PatientRegistration(p);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Patient/Update/5
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, Patient p)
        {
            p.PatientId = id;
            Response response = helper.UpdatePatient(p);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Patient/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllPatients(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Patient/GetById/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllPatients(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Patient/Delete/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeletePatient(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}