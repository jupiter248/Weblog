using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int tagId);
        Task<Tag> AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag newTag);
        Task DeleteTagAsync(Tag tag); 
    }
}