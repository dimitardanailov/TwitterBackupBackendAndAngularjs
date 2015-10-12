using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWebApplicationEntities
{
    public class MongoDbMessage : BaseModel, IMessageEntity
    {
        [BsonId]
        public BsonObjectId MessageID { get; set; }

        [Required]
        [BsonRepresentation(BsonType.String)]
        public string UserID { get; set; }

        [Required]
        [BsonRepresentation(BsonType.String)]
        [MinLength(10, ErrorMessage = "Text should be at least {0} characters.")]
        [MaxLength(140, ErrorMessage = "Maximum length is {0} characters.")]
        public string Text { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}
