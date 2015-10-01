using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TwitterWebApplication.Models;

namespace TwitterWebApplicationEntities
{
    class Follower: BaseModel
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

        [ForeignKey("FollowerUser")]
        public virtual ApplicationUser FollowerUser { get; set; }
    }
}
