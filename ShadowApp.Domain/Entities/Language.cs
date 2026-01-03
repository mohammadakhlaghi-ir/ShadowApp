using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class Language
    {
        [Key]
        public uint ID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ulong Creator { get; set; } = 0;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public ulong Modifier { get; set; } = 0;
        public string Crc { get; set; } = "";

        #region navigation
        public Setting Setting { get; set; } = null!;
        #endregion
    }
}
