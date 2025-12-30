using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShadowApp.Application.DTOs;
using ShadowApp.Application.Interfaces;
using ShadowApp.Domain.Entities;
using ShadowApp.Infrastructure.Persistence;
using ShadowApp.Infrastructure.Services;

namespace ShadowApp.Test.Services
{
    public class LogServiceTests
    {
        private readonly Mock<ITranslationService> _translationMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AppDbContext _context;
        private readonly LogService _sut;

        public LogServiceTests()
        {
            _translationMock = new Mock<ITranslationService>();
            _mapperMock = new Mock<IMapper>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            _sut = new LogService(
                _context,
                _mapperMock.Object,
                _translationMock.Object
            );
        }

        [Fact]
        public async Task AddLog_Should_Add_Log_To_Database()
        {
            var dto = new AddLogDto
            {
                Title = "LOG_TITLE",
                Description = "LOG_DESC",
            };

            _translationMock
                .Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns<string, string, Dictionary<string, string>>(
                    (key, _, _) => key + "_FA"
                );

            _mapperMock
                .Setup(x => x.Map<Log>(It.IsAny<AddLogDto>()))
                .Returns<AddLogDto>(dto => new Log
                {
                    Title = dto.Title,
                    Description = dto.Description,
                });

            await _sut.AddLog(dto, "fa");

            var log = _context.Logs.Single();

            Assert.Equal("LOG_TITLE_FA", log.Title);
            Assert.Equal("LOG_DESC_FA", log.Description);
            Assert.False(string.IsNullOrEmpty(log.Crc));
        }
    }

}
