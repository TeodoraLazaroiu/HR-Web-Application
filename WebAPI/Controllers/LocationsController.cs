using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public LocationsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            var locations = (await unitOfWork.Locations.GetAll()).Select(a => new LocationDTO(a)).ToList();
            return locations;
        }

        // GET: api/Locations/id
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            var location = await unitOfWork.Locations.GetById(id);

            if (location == null)
            {
                return NotFound("Location with this id doesn't exist");
            }

            return new LocationDTO(location);
        }

        // PUT: api/Locations/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationDTO location)
        {
            var locationInDb = await unitOfWork.Locations.GetById(id);

            if (locationInDb == null)
            {
                return NotFound("Location with this id doesn't exist");
            }

            locationInDb.City = location.City;
            locationInDb.Country = location.Country;
            locationInDb.PostalCode = location.PostalCode;
            locationInDb.Street = location.Street;
            locationInDb.Number = location.Number;

            await unitOfWork.Locations.Update(locationInDb);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO location)
        {
            var locationToAdd = new Location();
            locationToAdd.City = location.City;
            locationToAdd.Country = location.Country;
            locationToAdd.PostalCode = location.PostalCode;
            locationToAdd.Street = location.Street;
            locationToAdd.Number = location.Number;

            await unitOfWork.Locations.Create(locationToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Locations/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var locationInDb = await unitOfWork.Locations.GetById(id);

            if (locationInDb == null)
            {
                return NotFound("Location with this id doesn't exist");
            }

            await unitOfWork.Locations.Delete(locationInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
