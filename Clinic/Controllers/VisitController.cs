using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/Visit")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        Visithelper helper = new Visithelper();

        // POST: api/Visit/Register
        [HttpPost("")]
        public IActionResult Register(Visit v)
        {
            Response response = helper.VisitRegistration(v);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Visit/Update/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Visit v)
        {
            v.VisitId = id;
            Response response = helper.UpdateVisit(v);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Visit/GetAll
        [HttpGet("")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllVisits(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Visit/GetById/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllVisits(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Visit/Delete/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeleteVisit(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}