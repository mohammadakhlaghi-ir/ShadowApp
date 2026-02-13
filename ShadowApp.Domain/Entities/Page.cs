using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class Page
    {
        [Key]
        public Guid ID { get; set; }
        public Guid SpecialPageID { get; set; }
        public Guid FaviconID { get; set; }
        public Guid LayoutID { get; set; }
        public bool HeaderVisible { get; set; } = true;
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public SpecialPage SpecialPage { get; set; } = null!;
        public Favicon Favicon { get; set; } = null!;
        public Layout Layout { get; set; } = null!;
        public ICollection<PageTranslation> Translations { get; set; } = [];
        #endregion
    }
}
