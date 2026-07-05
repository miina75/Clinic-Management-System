using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/Prescription")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        Prescriptionhelper helper = new Prescriptionhelper();

        // POST: api/Prescription/Register
        [HttpPost("")]
        public IActionResult Register(Prescription p)
        {
            Response response = helper.PrescriptionRegistration(p);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Prescription/Update/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Prescription p)
        {
            p.PrescriptionId = id;
            Response response = helper.UpdatePrescription(p);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Prescription/GetAll
        [HttpGet("")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllPrescriptions(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Prescription/GetById/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllPrescriptions(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Prescription/Delete/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeletePrescription(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}