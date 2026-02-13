using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class Header
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public ICollection<Layout> Layouts { get; set; } = [];
        #endregion
    }
}
