using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblog.Application.CustomExceptions;
using Weblog.Application.Dtos.MediaDtos;
using Weblog.Application.Interfaces.Repositories;
using Weblog.Application.Interfaces.Services;

using Weblog.Domain.Models;

namespace Weblog.Infrastructure.Services
{
    public class MediumService : IMediumService
    {
        private readonly IMapper _mapper;
        private readonly IMediumRepository _mediumRepo;
        private readonly IWebHostEnvironment _webHost;

        public MediumService(IMapper mapper, IMediumRepository mediumRepo, IWebHostEnvironment webHost)
        {
            _mapper = mapper;
            _mediumRepo = mediumRepo;
            _webHost = webHost;
        }

        public async Task DeleteMediumAsync(int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException("Media not found");
            await _mediumRepo.DeleteMediumAsync(medium);
        }

        public async Task<List<MediumDto>> GetAllMediaAsync()
        {
            List<Medium> media = await _mediumRepo.GetAllMediaAsync();
            List<MediumDto> mediaDtos = _mapper.Map<List<MediumDto>>(media);
            return mediaDtos;
        }

        public async Task<MediumDto> GetMediumByIdAsync(int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException("Media not found");
            return _mapper.Map<MediumDto>(medium);
        }

        public async Task<MediumDto> StoreMediumAsync(UploadMediumDto uploadMediaDto)
        {
            // Article? article = null;
            // Event? eventModel = null;
            // Podcast? podcast = null;
            // Contributor? contributor = null;

            // Check if these exist



            IFormFile mediumFile = uploadMediaDto.UploadedFile;
            if (mediumFile == null || mediumFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            if (!Directory.Exists(Path.Combine(_webHost.WebRootPath, $"uploads/{uploadMediaDto.MediumType}")))
            {
                Directory.CreateDirectory(Path.Combine(_webHost.WebRootPath, $"uploads/{uploadMediaDto.MediumType}"));
            }

            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(mediumFile.FileName)}";
            var filePath = Path.Combine(_webHost.WebRootPath, $"uploads/{uploadMediaDto.MediumType}", fileName);
            

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await mediumFile.CopyToAsync(stream);
            }

            Medium medium = new Medium
            {
                Name = fileName,
                Path = filePath,
                IsPrimary = uploadMediaDto.IsPrimary,
                MediumType = uploadMediaDto.MediumType,
                MediumParentType = uploadMediaDto.MediumParentType,
            };

            await _mediumRepo.AddMediumAsync(medium);
            return _mapper.Map<MediumDto>(medium);
        }

        public async Task UpdateMediumAsync(UpdateMediumDto updateMediumDto, int mediaId)
        {
            Medium medium = await _mediumRepo.GetMediumByIdAsync(mediaId) ?? throw new NotFoundException("Media not found");
            medium.Name = updateMediumDto.Name;
            medium.Path = updateMediumDto.Path;
            medium.IsPrimary = updateMediumDto.IsPrimary;
            await _mediumRepo.UpdateMediumAsync(medium);
        }
    }
}