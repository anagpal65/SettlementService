using System;

namespace SettlementService.Models
{
    public class Booking
    {
        public GUID ID { get; set; }

        public string Name { get; set; }

        public Time Time { get; set; }
    }
}