using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class SpecialPageTranslation
    {
        [Key]
        public Guid ID { get; set; }
        public Guid SpecialPageID { get; set; }
        public Guid LanguageID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public SpecialPage SpecialPage { get; set; } = null!;
        public Language Language { get; set; } = null!;
        #endregion
    }
}
