using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterWebApplicationEntities
{
    public class Follower : BaseModel
    {
        [Key]
        public int FollowerID { get; set; }

        // Foreign Key
        [Required]
        [MaxLength(128, ErrorMessage = "Maximum length is {0} characters.")]
        public string FollowedUserID { get; set; }

        [Required]
        [MaxLength(128, ErrorMessage = "Maximum length is {0} characters.")]
        public string FollowerUserID { get; set; }

        [ForeignKey("FollowedUserID")]
        public virtual ApplicationUser FollowedUser { get; set; }

        [ForeignKey("FollowerUserID")]
        public virtual ApplicationUser FollowerUser { get; set; }
    }
}
