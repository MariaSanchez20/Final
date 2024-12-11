using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenantAPI.Models;

namespace TenantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectricityConsumptionController : ControllerBase
    {
        private static readonly List<ElectricityConsumption> Consumptions = new List<ElectricityConsumption>();

        [HttpGet]
        public IActionResult GetConsumptions()
        {
            return Ok(Consumptions);
        }

        [HttpGet("{id}")]
        public IActionResult GetConsumptionById(int id)
        {
            var consumption = Consumptions.FirstOrDefault(c => c.Id == id);
            if (consumption == null)
                return NotFound();
            return Ok(consumption);
        }

        [HttpPost]
        public IActionResult CreateConsumption([FromBody] ElectricityConsumption consumption)
        {
            if (consumption.ApartmentId <= 0 || consumption.QuantityKw < 0)
                return BadRequest("Invalid ApartmentId or QuantityKw.");

            consumption.Id = Consumptions.Count > 0 ? Consumptions.Max(c => c.Id) + 1 : 1;
            Consumptions.Add(consumption);
            return CreatedAtAction(nameof(GetConsumptionById), new { id = consumption.Id }, consumption);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateConsumption(int id, [FromBody] ElectricityConsumption updatedConsumption)
        {
            var consumption = Consumptions.FirstOrDefault(c => c.Id == id);
            if (consumption == null)
                return NotFound();

            consumption.ApartmentId = updatedConsumption.ApartmentId;
            consumption.Date = updatedConsumption.Date;
            consumption.QuantityKw = updatedConsumption.QuantityKw;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConsumption(int id)
        {
            var consumption = Consumptions.FirstOrDefault(c => c.Id == id);
            if (consumption == null)
                return NotFound();

            Consumptions.Remove(consumption);
            return NoContent();
        }
    }
}
