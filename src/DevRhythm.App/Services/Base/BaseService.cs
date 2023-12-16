using AutoMapper;
using DevRhythm.Infrastructure.Data;

namespace DevRhythm.App.Services.Base
{
    public abstract class BaseService(DevRhythmDbContext context, IMapper mapper)
    {
        private protected readonly DevRhythmDbContext _context = context;
        private protected readonly IMapper _mapper = mapper;
    }
}
