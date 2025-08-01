using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.ContributorDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;
using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class ContributorService : IContributorService
    {
        private readonly IContributorRepository _contributorRepo;
        private readonly IMapper _mapper;

        public ContributorService(IContributorRepository contributorRepo, IMapper mapper)
        {
            _contributorRepo = contributorRepo;
            _mapper = mapper;
        }

        public async Task<ContributorDto> AddContributorAsync(AddContributorDto addContributorDto)
        {
            Contributor newContributor = _mapper.Map<Contributor>(addContributorDto);
            newContributor.FullName = $"{newContributor.FirstName} {newContributor.FamilyName}";
            Contributor addedContributor = await _contributorRepo.AddContributorAsync(newContributor);
            return _mapper.Map<ContributorDto>(addedContributor);
        }

        public async Task DeleteContributorAsync(int contributorId)
        {
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException("Contributor cot found");
            await _contributorRepo.DeleteContributorAsync(contributor);
        }

        public async Task<List<ContributorDto>> GetAllContributorsAsync()
        {
            List<Contributor> contributors = await _contributorRepo.GetAllContributorsAsync();
            List<ContributorDto> contributorDtos = _mapper.Map<List<ContributorDto>>(contributors);
            return contributorDtos;
        }

        public async Task<ContributorDto> GetContributorByIdAsync(int contributorId)
        {
            Contributor contributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException("Contributor cot found");
            return _mapper.Map<ContributorDto>(contributor);
        }

        public async Task UpdateContributorAsync(UpdateContributorDto updateContributorDto, int contributorId)
        {
            Contributor currentContributor = await _contributorRepo.GetContributorByIdAsync(contributorId) ?? throw new NotFoundException("Contributor cot found");
            Contributor newContributor = _mapper.Map<Contributor>(updateContributorDto);
            await _contributorRepo.UpdateContributorAsync(currentContributor ,newContributor);
        }
    }
}