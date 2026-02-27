using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class LayoutItem
    {
        [Key]
        public Guid ID { get; set; }
        public Guid LayoutItemCategoryID { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public ICollection<HeaderSection> HeaderSections { get; set; } = [];
        public ICollection<Menu> Menues { get; set; } = [];
        public LayoutItemCategory LayoutItemCategory { get; set; } = null!;
        #endregion
    }
}
