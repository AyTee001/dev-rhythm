using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Settings;

namespace DevRhythm.Web.DTOs
{
    public class PageGetDto
    {
        public int PageNumber { get; set; }
        public ICollection<long> TagIds { get; set; } = [];
    }
}
