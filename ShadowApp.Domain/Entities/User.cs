using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
        public bool Enabled { get; set; } = true;
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public ICollection<UserTranslation> Translations { get; set; } = [];
        #endregion
    }
}