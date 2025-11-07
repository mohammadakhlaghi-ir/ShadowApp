using AutoMapper;
using ShadowApp.Application.DTOs;
using ShadowApp.Application.Interfaces;
using ShadowApp.Application.Utilities;
using ShadowApp.Domain.Entities;
using ShadowApp.Infrastructure.Persistence;

namespace ShadowApp.Infrastructure.Services
{
    public class LogService(AppDbContext context, IMapper mapper, ITranslationService translationService) : ILogService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ITranslationService _translationService = translationService;

        public async Task AddLog(AddLogDto logDto, string languageName = "fa", Dictionary<string, string>? parameters = null)
        {
            logDto.Title = _translationService.Translate(logDto.Title, languageName, parameters);
            logDto.Description = _translationService.Translate(logDto.Description, languageName, parameters);

            var log = _mapper.Map<Log>(logDto);
            log.Crc = CrcHelper.ComputeCrc($"{log.Title}|{log.Description}|{log.DateTime:O}");
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
