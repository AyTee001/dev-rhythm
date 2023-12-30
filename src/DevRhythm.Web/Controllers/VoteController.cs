using DevRhythm.App.Interfaces;
using DevRhythm.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DevRhythm.Web.Controllers
{
    public class VoteController(IVoteService voteService) : Controller
    {
        private readonly IVoteService _voteService = voteService;

        [HttpPost]
        public async Task<IActionResult> UpdateVote([FromBody] NewVoteDto newVoteDto)
        {
            try
            {
                await _voteService.UpdateVoteAsync(newVoteDto);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
