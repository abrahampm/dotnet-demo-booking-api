using System;
using System.ComponentModel.DataAnnotations;

using alten_test.Core.Models;


namespace alten_test.Core.Dto
{
    public class RoomDto
    {
        [Required]
        public int Id { get; set; }

        [Required] 
        public int Number { get; set; }
        
        [Required]
        [EnumDataType(typeof(RoomType))]
        public RoomType Type { get; set; }
        
        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; }
                
    }
}