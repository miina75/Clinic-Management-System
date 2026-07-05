
using Clinic.data;
using Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/Bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        Billhelper helper = new Billhelper();

        // POST: api/Bill/Register
        [HttpPost("")]
        public IActionResult Register(Bill b)
        {
            Response response = helper.BillRegistration(b);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // PUT: api/Bill/Update/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Bill b)
        {
            b.BillId = id;
            Response response = helper.UpdateBill(b);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }

        // GET: api/Bill/GetAll
        [HttpGet("")]
        public IActionResult GetAll()
        {
            Response response = helper.GetAllBills(0);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // GET: api/Bill/GetById/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Response response = helper.GetAllBills(id);
            if (response.Status) return Ok(response);
            else return NotFound(response);
        }

        // DELETE: api/Bill/Delete/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = helper.DeleteBill(id);
            if (response.Status) return Ok(response);
            else return BadRequest(response);
        }
    }
}