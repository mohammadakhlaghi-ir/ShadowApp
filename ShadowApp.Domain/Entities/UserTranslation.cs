using System.ComponentModel.DataAnnotations;

namespace ShadowApp.Domain.Entities
{
    public class UserTranslation
    {
        [Key]
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid LanguageID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Description { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public User User { get; set; } = null!;
        public Language Language { get; set; } = null!;
        #endregion
    }
}
