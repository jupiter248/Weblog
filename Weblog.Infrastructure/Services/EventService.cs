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
using Weblog.Domain.Enums;
using Weblog.Domain.Errors.Category;
using Weblog.Domain.Errors.Contributor;
using Weblog.Domain.Errors.Event;
using Weblog.Domain.Errors.Tag;
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

        private readonly ILikeContentRepository _likeContentRepo;
        public EventService( ILikeContentRepository likeContentRepo,IContributorRepository contributorRepo, IEventRepository eventRepo, ITagRepository tagRepo, IMapper mapper, ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _eventRepo = eventRepo;
            _mapper = mapper;
            _tagRepo = tagRepo;
            _contributorRepo = contributorRepo;
            _likeContentRepo = likeContentRepo;
        }

        public async Task AddContributorAsync(int eventId, int contributorId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            await _eventRepo.AddContributorAsync(eventModel, contributor); 
        }

        public async Task<EventDto> AddEventAsync(AddEventDto addEventDto)
        {
            Event newEvent = _mapper.Map<Event>(addEventDto);
            Category? category = await _categoryRepo.GetCategoryByIdAsync(addEventDto.CategoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            if (category.EntityType == CategoryType.Event)
            {
                newEvent.Category = category;
            }
            else
            {
                throw new ConflictException(CategoryErrorCodes.CategoryEntityTypeMatchFailed);
            }
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
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.TagNotFound);
            await _eventRepo.AddTagToEvent(eventModel,tag);
        }

        public async Task DeleteContributorAsync(int eventId, int contributorId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(eventId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            await _eventRepo.DeleteContributorAsync(eventModel,contributor);     
        }

        public async Task DeleteEventAsync(int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            await _eventRepo.DeleteEventAsync(eventModel);
        }

        public async Task DeleteTagAsync(int eventId, int tagId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            Tag tag = await _tagRepo.GetTagByIdAsync(tagId) ?? throw new NotFoundException(TagErrorCodes.TagNotFound);
            await _eventRepo.DeleteTagFromEvent(eventModel ,tag);
        }

        public async Task<List<EventSummaryDto>> GetAllEventsAsync(PaginationParams paginationParams, EventFilteringParams eventFilteringParams)
        {
            List<Event> events = await _eventRepo.GetAllEventsAsync(eventFilteringParams,paginationParams);
            List<EventSummaryDto> eventSummaryDtos = _mapper.Map<List<EventSummaryDto>>(events);
            foreach (var item in eventSummaryDtos)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Event);
            }

            if (eventFilteringParams.MostLikes == true)
            {
                eventSummaryDtos = eventSummaryDtos.OrderByDescending(l => l.LikeCount).ToList();
            }
            else if (eventFilteringParams.MostLikes == false)
            {
                eventSummaryDtos = eventSummaryDtos.OrderBy(l => l.LikeCount).ToList();
            }

            if (eventFilteringParams.MostViews == true)
            {
                eventSummaryDtos = eventSummaryDtos.OrderByDescending(l => l.ViewCount).ToList();
            }
            else if (eventFilteringParams.MostViews == false)
            {
                eventSummaryDtos = eventSummaryDtos.OrderBy(l => l.ViewCount).ToList();
            }
            return eventSummaryDtos;
        }

        public async Task<EventDto> GetEventByIdAsync(int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            EventDto eventDto = _mapper.Map<EventDto>(eventModel);
            eventDto.LikeCount = await _likeContentRepo.GetLikeCountAsync(eventDto.Id, LikeAndViewType.Event);
            return eventDto;
        }

        public async Task<List<EventSummaryDto>> GetSuggestionsAsync(PaginationParams paginationParams, int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            List<Event> events = await _eventRepo.GetSuggestionsAsync(paginationParams, eventModel);
            List<EventSummaryDto> eventSummaryDtos = _mapper.Map<List<EventSummaryDto>>(events);
            foreach (var item in eventSummaryDtos)
            {
                item.LikeCount = await _likeContentRepo.GetLikeCountAsync(item.Id, LikeAndViewType.Event);
            }
            return eventSummaryDtos; 
       }

        public async Task<int> IncrementEventViewAsync(int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            await _eventRepo.IncrementEventViewAsync(eventModel);
            return eventModel.ViewCount;
        }

        public async Task<EventDto> UpdateEventAsync(UpdateEventDto updateEventDto, int eventId)
        {
            Event eventModel = await _eventRepo.GetEventByIdAsync(eventId) ?? throw new NotFoundException(EventErrorCodes.EventNotFound);
            Category category = await _categoryRepo.GetCategoryByIdAsync(updateEventDto.CategoryId) ?? throw new NotFoundException(CategoryErrorCodes.CategoryNotFound);
            if (category.EntityType == CategoryType.Event)
            {
                eventModel.Category = category;
            }
            else
            {
                throw new ConflictException(CategoryErrorCodes.CategoryEntityTypeMatchFailed);
            }
            eventModel = _mapper.Map(updateEventDto, eventModel);
            eventModel.UpdatedAt = DateTimeOffset.Now;
            eventModel.Slug = updateEventDto.Title.Slugify();
            if (eventModel.IsDisplayed == true)
            {
                if (eventModel.DisplayedAt == DateTimeOffset.MinValue)
                {
                    eventModel.DisplayedAt = DateTimeOffset.Now;
                }
            }
            else
            {
                if (eventModel.DisplayedAt != DateTimeOffset.MinValue)
                {
                    eventModel.DisplayedAt = DateTimeOffset.MinValue;
                }
            }
            await _eventRepo.UpdateEventAsync(eventModel);
            return _mapper.Map<EventDto>(eventModel);
        }
    }
}