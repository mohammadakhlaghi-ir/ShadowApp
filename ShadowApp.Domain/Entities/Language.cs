using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class Language
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public Guid? Modifier { get; set; }
        public string Crc { get; set; } = "";

        #region navigations
        public Setting Setting { get; set; } = null!;
        #endregion
    }
}
