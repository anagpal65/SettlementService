using Microsoft.AspNetCore.Mvc;
using SettlementService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SettlementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService BookingService;

        public BookingController(IBookingService bookingService)
        {
            BookingService = bookingService;
        }

        // POST api/<BookingController>
        [HttpPost(Name = "AddBooking")]
        public ActionResult<SuccessfulBooking> Post([FromBody] AddBooking value)
        {
            var result = BookingService.AddBooking(value);

            if (result.httpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return StatusCode((int)result.httpStatusCode, result.ErrorMessage);
            }

            return Ok(result.Value);
        }
    }
}
