using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowApp.Domain.Entities
{
    public class UserTranslation
    {
        [Key]
        public ulong ID { get; set; }
        public ulong UserID { get; set; }
        public uint LanguageID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ulong Creator { get; set; } = 0;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public ulong Modifier { get; set; } = 0;
        public string Crc { get; set; } = "";

        #region navigation
        public User User { get; set; } = null!;
        public Language Language { get; set; } = null!;
        #endregion
    }
}
