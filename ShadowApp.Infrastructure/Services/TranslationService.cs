using ShadowApp.Application.Interfaces;
using System.Text.Json;

namespace ShadowApp.Infrastructure.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly string _basePath;

        public TranslationService()
        {
            _basePath = Path.Combine(AppContext.BaseDirectory, "Localization", "Logs");
        }

        public string Translate(string key, string languageName, Dictionary<string, string>? parameters = null)
        {
            var filePath = Path.Combine(_basePath, $"{languageName}.json");

            if (!File.Exists(filePath))
                filePath = Path.Combine(_basePath, "fa.json");

            var json = File.ReadAllText(filePath);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json)!;

            if (!dictionary.TryGetValue(key, out var value))
                return key;

            if (parameters != null)
                foreach (var param in parameters)
                    value = value.Replace($"{{{param.Key}}}", param.Value);

            return value;
        }
    }
}
