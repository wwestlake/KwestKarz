using System;
using System.Threading.Tasks;
using KwestKarz.Entities;
using KwestKarz.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwestKarz.Controllers
{
    [ApiController]
    [Route("api/gpt/memos")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService _memoService;

        public MemoController(IMemoService memoService)
        {
            _memoService = memoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Memo memo)
        {
            var saved = await _memoService.CreateMemoAsync(memo);
            return Ok(new
            {
                saved.Id,
                Status = "saved",
                saved.CreatedAt
            });
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            var memos = await _memoService.GetUpcomingAsync();
            return Ok(memos);
        }
    }
}
