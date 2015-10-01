using System;
using System.ComponentModel.DataAnnotations;

namespace TwitterWebApplicationEntities
{
    class BaseModel
    {
        public BaseModel()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
