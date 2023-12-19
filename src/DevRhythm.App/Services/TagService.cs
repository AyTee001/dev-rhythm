using AutoMapper;
using DevRhythm.App.DTOs;
using DevRhythm.App.Interfaces;
using DevRhythm.App.Services.Base;
using DevRhythm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevRhythm.App.Services
{
    public class TagService(DevRhythmDbContext context, IMapper mapper) : BaseService(context, mapper), ITagService
    {
        public async Task<ICollection<TagFilterDto>> GetTagsForFilterAsync()
        {
            return await _context.Tags.AsNoTracking()
                         .Select(tag => _mapper.Map<TagFilterDto>(tag))
                         .ToListAsync();
        }
    }
}
