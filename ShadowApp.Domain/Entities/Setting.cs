using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class Setting
    {
        [Key]
        public byte ID { get; set; }
        public Guid LanguageID { get; set; }
        public string AppName { get; set; } = "";
        public Guid FaviconID { get; set; }
        public Guid LayoutID { get; set; }
        public Guid LogoID { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public Language Language { get; set; } = null!;
        public Favicon Favicon { get; set; } = null!;
        public Layout Layout { get; set; } = null!;
        public Logo Logo { get; set; } = null!;
        #endregion
    }
}
