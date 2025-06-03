using KwestKarz.Entities;
using KwestKarz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwestKarz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Owner")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetById(Guid id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> Create(Vehicle vehicle)
        {
            var created = await _vehicleService.CreateAsync(vehicle);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vehicle>> Update(Guid id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest("ID mismatch");

            var existing = await _vehicleService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _vehicleService.UpdateAsync(vehicle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _vehicleService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _vehicleService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImportVehicles([FromServices] IVehicleImportService importService, [FromForm] VehicleImportRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = request.File.OpenReadStream();
            var count = await importService.ImportVehiclesAsync(stream);

            return Ok(new { message = $"Import successful. {count} vehicles added." });
        }

    }
}
