namespace ShadowApp.Application.Interfaces
{
    public interface ITranslationService
    {
        string Translate(string key, string languageName, Dictionary<string, string>? parameters = null);
    }
}
