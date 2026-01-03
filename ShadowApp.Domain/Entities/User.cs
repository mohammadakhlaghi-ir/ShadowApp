using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class User
    {
        [Key]
        public ulong ID { get; set; }
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
        public bool Enabled { get; set; } = true;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ulong Creator { get; set; } = 0;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public ulong Modifier { get; set; } = 0;
        public string Crc { get; set; } = "";

        #region navigations
        public ICollection<UserTranslation> Translations { get; set; } = [];
        #endregion
    }
}