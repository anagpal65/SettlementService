using System;

namespace SettlementService.Models
{
    public class Booking
    {
        public Guid bookingId { get; set; }

        public string name { get; set; }

        public TimeOnly bookingTime { get; set; }
    }
}