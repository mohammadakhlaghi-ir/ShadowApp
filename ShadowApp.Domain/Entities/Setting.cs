using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class Setting
    {
        [Key]
        public byte ID { get; set; }
        public Guid LanguageID { get; set; }
        public string AppName { get; set; } = "";
        public string? AppDescription { get; set; }
        public Guid FaviconID { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public Language Language { get; set; } = null!;
        public Favicon Favicon { get; set; } = null!;
        #endregion
    }
}
