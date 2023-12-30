using DevRhythm.Shared.DTOs;

namespace DevRhythm.App.Interfaces
{
    public interface IVoteService
    {
        Task UpdateVoteAsync(NewVoteDto newVoteDto);
    }
}
