using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class HeaderSection
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public Guid HeaderID { get; set; }
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public Guid LayoutItemID { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public Header Header { get; set; } = null!;
        public LayoutItem LayoutItem { get; set; } = null!;
        #endregion
    }
}
