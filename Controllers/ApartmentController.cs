using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenantAPI.Models;

namespace TenantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private static readonly List<Apartment> Apartments = new List<Apartment>();

        [HttpGet]
        public IActionResult GetApartments()
        {
            return Ok(Apartments);
        }

        [HttpGet("{id}")]
        public IActionResult GetApartmentById(int id)
        {
            var apartment = Apartments.FirstOrDefault(a => a.Id == id);
            if (apartment == null)
                return NotFound();
            return Ok(apartment);
        }

        [HttpPost]
        public IActionResult CreateApartment([FromBody] Apartment apartment)
        {
            if (string.IsNullOrEmpty(apartment.OwnerName) || string.IsNullOrEmpty(apartment.PhoneNumber))
                return BadRequest("OwnerName and PhoneNumber are required.");

            apartment.Id = Apartments.Count > 0 ? Apartments.Max(a => a.Id) + 1 : 1;
            Apartments.Add(apartment);
            return CreatedAtAction(nameof(GetApartmentById), new { id = apartment.Id }, apartment);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateApartment(int id, [FromBody] Apartment updatedApartment)
        {
            var apartment = Apartments.FirstOrDefault(a => a.Id == id);
            if (apartment == null)
                return NotFound();

            apartment.OwnerName = updatedApartment.OwnerName;
            apartment.PhoneNumber = updatedApartment.PhoneNumber;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteApartment(int id)
        {
            var apartment = Apartments.FirstOrDefault(a => a.Id == id);
            if (apartment == null)
                return NotFound();

            Apartments.Remove(apartment);
            return NoContent();
        }
    }
}
