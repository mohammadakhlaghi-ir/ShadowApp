using ShadowApp.Application.DTOs;

namespace ShadowApp.Application.Interfaces
{
    public interface ILogService
    {
        Task AddLog(AddLogDto logDto, string languageName, Dictionary<string, string>? parameters);
    }
}
