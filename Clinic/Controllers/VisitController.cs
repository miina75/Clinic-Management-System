// Controllers/VisitController.cs
using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        Visithelper helper = new Visithelper();

        [HttpPost("Register")]
        public IActionResult Register(Visit v)
        {
            Response response = helper.VisitRegistration(v);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, Visit v)
        {
            v.VisitId = id;
            Response response = helper.UpdateVisit(v);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllVisits(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllVisits(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeleteVisit(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}