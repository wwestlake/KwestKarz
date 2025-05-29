using KwestKarz.Entities;
using KwestKarz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwestKarz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Owner")]
    public class GuestController : ControllerBase
    {
        private readonly GuestService _guestService;

        public GuestController(GuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetById(int id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            return guest == null ? NotFound() : Ok(guest);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guest>> CreateOrGet([FromBody] Guest guest)
        {
            var result = await _guestService.GetOrCreateGuestAsync(guest);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Guest updatedGuest)
        {
            if (id != updatedGuest.GuestId)
                return BadRequest("Guest ID mismatch.");

            await _guestService.UpdateGuestAsync(updatedGuest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _guestService.DeleteGuestAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/trips")]
        public async Task<IActionResult> AddTrip(int id, [FromBody] Trip trip)
        {
            await _guestService.AddTripToGuestAsync(id, trip);
            return Ok();
        }

        [HttpPost("{id}/contactlog")]
        public async Task<IActionResult> AddContactLog(int id, [FromBody] ContactLog log)
        {
            await _guestService.AddContactLogAsync(id, log);
            return Ok();
        }

        [HttpPost("{id}/charges")]
        public async Task<IActionResult> AddOutstandingCharge(int id, [FromBody] OutstandingCharge charge)
        {
            await _guestService.AddOutstandingChargeAsync(id, charge);
            return Ok();
        }
    }
}
