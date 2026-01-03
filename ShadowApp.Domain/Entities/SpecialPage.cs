using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class SpecialPage
    {
        [Key]
        public uint ID { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public ulong Creator { get; set; } = 0;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ulong Modifier { get; set; } = 0;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public Page Page { get; set; } = null!;
        #endregion
    }
}
