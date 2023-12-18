﻿using DevRhythm.App.DTOs;

namespace DevRhythm.App.Interfaces
{
    public interface ITagService
    {
        public Task<ICollection<TagShortDto>> GetTagsAsync();
    }
}
