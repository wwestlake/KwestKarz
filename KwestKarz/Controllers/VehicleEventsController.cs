using KwestKarz.Entities;
using KwestKarz.Entities.Maintenance;
using KwestKarz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KwestKarz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Owner,Maintenance")]
    public class VehicleEventsController : ControllerBase
    {
        private readonly IVehicleEventService _eventService;

        public VehicleEventsController(IVehicleEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<ActionResult<List<VehicleEvent>>> GetEventsForVehicle(Guid vehicleId)
        {
            var events = await _eventService.GetEventsForVehicleAsync(vehicleId);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleEvent>> GetEventById(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        [HttpPost("maintenance")]
        public async Task<IActionResult> AddMaintenance([FromBody] MaintenanceEntry entry)
        {
            await _eventService.AddEventAsync(entry);
            return Ok(entry);
        }

        [HttpPost("inspection")]
        public async Task<IActionResult> AddInspection([FromBody] InspectionEntry entry)
        {
            await _eventService.AddEventAsync(entry);
            return Ok(entry);
        }

        [HttpPost("incident")]
        public async Task<IActionResult> AddIncident([FromBody] IncidentReport entry)
        {
            await _eventService.AddEventAsync(entry);
            return Ok(entry);
        }

        [HttpPost("repair")]
        public async Task<IActionResult> AddRepair([FromBody] RepairEntry entry)
        {
            await _eventService.AddEventAsync(entry);
            return Ok(entry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
