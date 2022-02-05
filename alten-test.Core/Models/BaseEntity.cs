using System;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Models
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}