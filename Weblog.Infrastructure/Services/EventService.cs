using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.EventDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Application.Queries;
using Weblog.Application.Queries.FilteringParams;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Extension;

namespace Weblog.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IContributorRepository _contributorRepo;
        public EventService(IContributorRepository contributorRepo, IEventRepository eventRepo, ITagRepository tagRepo, IMapper mapper, ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _eventRepo = eventRepo;
            _mapper = mapper;
            _tagRepo = tagRepo;
            _contributorRepo = contributorRepo;
        }

        public async Task AddContributorAsync(int eventId, int contributorId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(eventId) ?? throw new NotFoundException("Contributor not found");
            await _eventRepo.AddContributorAsync(eventModel,contributor);        }

        public async Task<EventDto> AddEventAsync(AddEventDto addEventDto)
        {
            Event newEvent = _mapper.Map<Event>(addEventDto);
            Category? category = await _categoryRepo.GetCategoryByIdAsync(addEventDto.CategoryId) ?? throw new NotFoundException("Category not found");
            newEvent.Category = category;

            newEvent.Slug = newEvent.Title.Slugify();

            if (newEvent.IsDisplayed)
            {
                newEvent.DisplayedAt = DateTimeOffset.Now;
            }

            Event addedEvent = await _eventRepo.AddEventAsync(newEvent);
            return _mapper.Map<EventDto>(addedEvent);

        }

        public async Task AddTagAsync(int eventId, int tagId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _eventRepo.AddTagToEvent(eventModel,tag);
        }

        public async Task DeleteContributorAsync(int eventId, int contributorId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(eventId) ?? throw new NotFoundException("Contributor not found");
            await _eventRepo.DeleteContributorAsync(eventModel,contributor);     
        }

        public async Task DeleteEventAsync(int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            await _eventRepo.DeleteEventAsync(eventModel);
        }

        public async Task DeleteTagAsync(int eventId, int tagId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException("Tag not found");
            await _eventRepo.DeleteTagFromEvent(eventModel ,tag);
        }

        public async Task<List<EventDto>> GetAllEventsAsync(PaginationParams paginationParams, EventFilteringParams eventFilteringParams)
        {
            List<Event> events = await _eventRepo.GetAllEventsAsync(eventFilteringParams,paginationParams);
            List<EventDto> eventDtos = _mapper.Map<List<EventDto>>(events);
            return eventDtos;
        }

        public async Task<EventDto> GetEventByIdAsync(int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            return _mapper.Map<EventDto>(eventModel);
        }

        public async Task UpdateEventAsync(UpdateEventDto updateEventDto, int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            Category category = await _categoryRepo.GetCategoryByIdAsync(updateEventDto.CategoryId) ?? throw new NotFoundException("Category not found");
            eventModel.Title = updateEventDto.Title;
            eventModel.Capacity = updateEventDto.Capacity;
            eventModel.CategoryId = updateEventDto.CategoryId;
            eventModel.Category = category;
            eventModel.Context = updateEventDto.Context;
            eventModel.Place = updateEventDto.Place;
            eventModel.IsDisplayed = updateEventDto.IsDisplayed;
            eventModel.IsFinished = updateEventDto.IsFinished;

            if (eventModel.IsDisplayed)
            {
                eventModel.DisplayedAt = DateTimeOffset.Now;
            }
            if (eventModel.IsFinished)
            {
                eventModel.FinishedAt = DateTimeOffset.Now;
            }
            eventModel.UpdatedAt = DateTimeOffset.Now;
            await _eventRepo.UpdateEventAsync(eventModel);
        }

        public async Task UpdateLikesAsync(int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            await _eventRepo.UpdateLikesAsync(eventModel);
        }

        public async Task UpdateViewersAsync(int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event not found");
            await _eventRepo.UpdateViewersAsync(eventModel);
        }
    }
}