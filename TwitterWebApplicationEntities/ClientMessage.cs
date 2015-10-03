using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterWebApplicationEntities
{
    [Table("dbo.Messages")]
    public class ClientMessage : BaseModel
    {
        [Key]
        public int MessageID { get; set; }

        // Foreign Key
        // [Required]
        [MaxLength(128, ErrorMessage = "UserID should be must be {0} characters or less")]
        public string UserID { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Text should be at least {0} characters.")]
        [MaxLength(140, ErrorMessage = "Maximum length is {0} characters.")]
        public string Text { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}
