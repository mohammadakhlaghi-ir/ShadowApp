using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class Favicon
    {
        [Key]
        public uint ID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public bool Main { get; set; }
        public uint Creator { get; set; } = 0;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public uint Modifier { get; set; } = 0;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigation
        public Setting Setting { get; set; } = null!;
        #endregion
    }
}
