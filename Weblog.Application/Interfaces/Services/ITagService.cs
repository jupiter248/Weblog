using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.TagDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllTagsAsync();
        Task<TagDto> GetTagByIdAsync(int tagId);
        Task<TagDto> AddTagAsync(AddTagDto addTagDto);
        Task UpdateTagAsync(UpdateTagDto updateTagDto, int currentTagId);
        Task DeleteTagAsync(int tagId);
    }
}