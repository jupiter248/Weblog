using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.TagDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepo , IMapper mapper)
        {
            _tagRepo = tagRepo;
            _mapper = mapper;

        }
        public async Task<TagDto> AddTagAsync(AddTagDto addTagDto)
        {
            Tag newTag = _mapper.Map<Tag>(addTagDto);
            Tag addedTag = await _tagRepo.AddTagAsync(newTag);
            return _mapper.Map<TagDto>(addedTag);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            Tag? tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _tagRepo.DeleteTagAsync(tag);
        }

        public async Task<List<TagDto>> GetAllTagsAsync()
        {
            List<Tag> tags = await _tagRepo.GetAllTagsAsync();
            List<TagDto> tagDtos = _mapper.Map<List<TagDto>>(tags);
            return tagDtos;
        }

        public async Task<TagDto> GetTagByIdAsync(int tagId)
        {
            Tag? tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            return _mapper.Map<TagDto>(tag);

        }

        public async Task UpdateTagAsync(UpdateTagDto updateTagDto, int currentTagId)
        {
            Tag? currentTag = await _tagRepo.GetTagByIdAsync(currentTagId) ?? throw new NotFoundException("Tag not found");
            Tag? newTag = _mapper.Map<Tag>(updateTagDto);
            await _tagRepo.UpdateTagAsync(currentTag , newTag);
        }
    }
}