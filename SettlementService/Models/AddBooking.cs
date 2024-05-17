using System;
using System.ComponentModel.DataAnnotations;

namespace SettlementService.Models
{
    public class AddBooking
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string BookingTime { get; set; }
    }
}