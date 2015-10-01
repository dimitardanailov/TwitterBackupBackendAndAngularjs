using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TwitterWebApplication.Models;

namespace TwitterWebApplicationEntities
{
    class Message : BaseModel
    {
        [Key]
        public int MessageID { get; set; }

        // Foreign Key
        [Required]
        public int UserID { get; set; }

        [Required]
        [MinLength(10), MaxLength(140, ErrorMessage = "Text should be must be 60 characters or less")]
        public string Text { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}
