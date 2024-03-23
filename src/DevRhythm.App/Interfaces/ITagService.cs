using DevRhythm.App.DTOs;

namespace DevRhythm.App.Interfaces
{
    public interface ITagService
    {
        public Task<ICollection<TagFilterDto>> GetTagsForFilterAsync();
        public Task AddPostTagsAsync(IEnumerable<long> tagIds, long postId);
    }
}
