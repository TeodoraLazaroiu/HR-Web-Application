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

        // GET: api/Locations/city
        [HttpGet("{city}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(string city)
        {
            var location = await unitOfWork.Locations.GetLocationByCity(city);

            if (location == null)
            {
                return NotFound("Location with this city doesn't exist");
            }

            return new LocationDTO(location);
        }

        // PUT: api/Locations/city
        [HttpPut("{city}")]
        public async Task<IActionResult> PutLocation(string city, LocationDTO location)
        {
            var locationInDb = await unitOfWork.Locations.GetLocationByCity(city);

            if (locationInDb == null)
            {
                return NotFound("Location with this city doesn't exist");
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
            var locationInDb = await unitOfWork.Locations.GetLocationByCity(location.City);

            if (locationInDb != null)
            {
                return NotFound("Location with this city already exist");
            }

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

        // DELETE: api/Locations/city
        [HttpDelete("{city}")]
        public async Task<IActionResult> DeleteLocation(string city)
        {
            var locationInDb = await unitOfWork.Locations.GetLocationByCity(city);

            if (locationInDb == null)
            {
                return NotFound("Location with this city doesn't exist");
            }

            await unitOfWork.Locations.Delete(locationInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
