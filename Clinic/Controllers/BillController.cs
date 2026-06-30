// Controllers/BillController.cs
using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        Billhelper helper = new Billhelper();

        [HttpPost("Register")]
        public IActionResult Register(Bill b)
        {
            Response response = helper.BillRegistration(b);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, Bill b)
        {
            b.BillId = id;
            Response response = helper.UpdateBill(b);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllBills(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllBills(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeleteBill(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}