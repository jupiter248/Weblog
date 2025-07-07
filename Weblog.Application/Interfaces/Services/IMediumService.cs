using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblog.Application.Dtos.MediaDtos;

namespace Weblog.Application.Interfaces.Services
{
    public interface IMediumService
    {
        Task<List<MediumDto>> GetAllMediaAsync();
        Task<MediumDto> GetMediumByIdAsync(int mediaId);
        Task<MediumDto> StoreMediumAsync(UploadMediumDto uploadMediaDto , string userId);
        Task UpdateMediumAsync(UpdateMediumDto editMediaDto, int mediaId);
        Task DeleteMediumAsync(int mediaId , string userId);
        
    }
}