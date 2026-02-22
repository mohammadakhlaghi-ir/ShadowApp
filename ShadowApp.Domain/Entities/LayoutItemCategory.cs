using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class LayoutItemCategory
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Crc { get; set; } = "";

        #region navigations
        public ICollection<LayoutItem> LayoutItems { get; set; } = [];
        #endregion
    }
}
