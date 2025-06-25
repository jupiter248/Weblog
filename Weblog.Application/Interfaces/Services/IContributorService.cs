using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.ContributorDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IContributorService
    {
        Task<List<ContributorDto>> GetAllContributorsAsync();
        Task<ContributorDto> GetContributorByIdAsync(int contributorId);
        Task<ContributorDto> AddContributorAsync(AddContributorDto addContributorDto);
        Task UpdateContributorAsync(UpdateContributorDto updateContributorDto , int contributorId);
        Task DeleteContributorAsync(int contributorId);

    }
}