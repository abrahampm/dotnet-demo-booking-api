using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Models
{
    public class Room : BaseEntity
    {
        [Required] 
        public int Number { get; set; }
        
        [Required]
        public RoomStatus Status { get; set; }
        
        [Required]
        public RoomType Type { get; set; }

        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; }
        
        public List<Reservation> Reservations { get; set; }

    }

    public enum RoomType {
        Single = 1,
        Double = 2,
        Triple = 3
    }

    public enum RoomStatus {
        Enabled = 0,
        Disabled = 1
    }
}