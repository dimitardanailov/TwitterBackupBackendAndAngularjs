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
        public int FollowedUserID { get; set; }

        [Required]
        public int FollowerUserID { get; set; }

        [ForeignKey("FollowedUserID")]
        public virtual ApplicationUser FollowedUser { get; set; }

        [ForeignKey("FollowerUser")]
        public virtual ApplicationUser FollowerUser { get; set; }
    }
}
