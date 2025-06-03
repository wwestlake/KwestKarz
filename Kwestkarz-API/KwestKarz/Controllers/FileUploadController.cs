using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KwestKarz.Services;
using System.Threading.Tasks;

namespace KwestKarz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileUploadController : ControllerBase
    {
        private readonly ITripEarningsService _tripEarningsService;

        public FileUploadController(ITripEarningsService tripEarningsService)
        {
            _tripEarningsService = tripEarningsService;
        }

        [HttpPost("trip-earnings-upload")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadTripEarnings([FromForm] TripEarningsUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = request.File.OpenReadStream();
            var insertedCount = _tripEarningsService.ImportTripEarnings(stream);

            return Ok(new { message = $"Upload successful. {insertedCount} new records added." });
        }
    }

    public class TripEarningsUploadRequest
    {
        public IFormFile File { get; set; }
    }

}

