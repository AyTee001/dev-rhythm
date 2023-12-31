﻿using DevRhythm.App.DTOs;
using DevRhythm.Shared.Entities;
using DevRhythm.Shared.Settings;

namespace DevRhythm.App.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostShortDto>> GetPostPreviewsAsync(PageSettings? pageSettings, SortSettings? sortSettings, ICollection<long> tagIds);
        Task<PostFullDto> GetPostByIdAsync(long id);
    }
}
