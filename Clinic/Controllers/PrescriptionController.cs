using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        Prescriptionhelper helper = new Prescriptionhelper();

        // POST: api/Patient/Register
        [HttpPost("Register")]
        public IActionResult Register(Prescription p)
        {
            Response response = helper.PrescriptionRegistration(p);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Patient/Update/5
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, Prescription p)
        {
            p.PrescriptionId = id;
            Response response = helper.UpdatePrescription(p);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Patient/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllPrescriptions(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Patient/GetById/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllPrescriptions(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Patient/Delete/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeletePrescription(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}