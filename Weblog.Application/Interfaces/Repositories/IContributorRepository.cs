using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Domain.Models;

namespace Weblog.Application.Interfaces.Repositories
{
    public interface IContributorRepository
    {
        Task<List<Contributor>> GetAllContributorsAsync();
        Task<Contributor> AddContributorAsync(Contributor contributor);
        Task<Contributor?> GetContributorByIdAsync(int contributorId);
        Task UpdateContributorAsync(Contributor currentContributor, Contributor newContributor);
        Task DeleteContributorAsync(Contributor contributor);
        Task<List<Contributor>> SearchByNameAsync(string keyword);  
    }
}