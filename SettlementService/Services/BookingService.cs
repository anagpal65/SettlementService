using SettlementService.Models;
using System.Globalization;
using System.Net;

public interface IBookingService
{
    Result<SuccessfulBooking> AddBooking(AddBooking booking);
}

public class BookingService : IBookingService
{
    private static List<Booking> _reservedBookings;


    public BookingService()
    {
        _reservedBookings = new List<Booking>();
    }

    public Result<SuccessfulBooking> AddBooking(AddBooking booking)
    {
        SuccessfulBooking successfulBooking;

        // can add booking
        var respose = IsValidBooking(booking);
        if (respose.httpStatusCode != HttpStatusCode.OK)
        {
            return new Result<SuccessfulBooking>(respose.ErrorMessage, respose.httpStatusCode);
        }

        var time = TimeOnly.ParseExact(booking.BookingTime, "HH:mm", CultureInfo.InvariantCulture);

        respose = CanAddBooking(time);
        if (respose.httpStatusCode != HttpStatusCode.OK)
        {
            return new Result<SuccessfulBooking>(respose.ErrorMessage, respose.httpStatusCode);
        }

        var new_booking = new Booking
        {
            bookingId = Guid.NewGuid(),
            name = booking.Name,
            bookingTime = time
        };

        _reservedBookings.Add(new_booking);

        successfulBooking = new SuccessfulBooking
        {
            bookingId = new_booking.bookingId
        };

        // order the bookings
        _reservedBookings = _reservedBookings.OrderBy(x => x.bookingTime).ToList();

        return new Result<SuccessfulBooking>(successfulBooking, HttpStatusCode.OK);
    }

    private Result CanAddBooking(TimeOnly time)
    {
        ///     Important : accepts up to 4 simultaneous settlements. 
        ///     The requirement seems unclear. Assuming that the service should accept up to 4 bookings at the same time.
        ///     As in if the time is passed to make a booking, with that hour, the service should accept up to 4 bookings.

        // get the number of bookings that are made within 59 minutes of the time
        var bookings_after = new List<TimeOnly>();
        var bookings_before = new List<TimeOnly>();
        var min_time = time.AddMinutes(-59);
        var max_time = time.AddMinutes(59);

        foreach (var booking in _reservedBookings)
        {
            if (booking.bookingTime >= time && booking.bookingTime <= max_time)
            {
                bookings_after.Add(booking.bookingTime);
            }
            if (booking.bookingTime <= time && booking.bookingTime >= min_time)
            {
                bookings_before.Add(booking.bookingTime);
            }
        }

        if (bookings_after.Count >= 4 || bookings_before.Count >= 4)
        {
            // return conflict
            return new Result("Cannot make booking at this time. Resulted in conflict", HttpStatusCode.Conflict);
        }

        return new Result(HttpStatusCode.OK);
    }

    private Result IsValidBooking(AddBooking booking)
    {
        // check if data is valid
        if (string.IsNullOrEmpty(booking.Name))
        {
            // return bad request
            return new Result("Name cannot be empty", HttpStatusCode.BadRequest);
        }

        // parse time
        if (!TimeOnly.TryParseExact(booking.BookingTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly time))
        {
            // return bad request
            return new Result("Invalid time format", HttpStatusCode.BadRequest);
        }

        // all bookings must complete by 5pm latest booking is 4:00pm
        if (time > new TimeOnly(16, 0))
        {
            // return bad request
            return new Result("Booking should be between 9am and 5pm ( last booking at 4pm )", HttpStatusCode.BadRequest);
        }

        // all bookings should be between 9am and 5pm
        if (time < new TimeOnly(9, 0))
        {
            // return bad request
            return new Result("Booking should be between 9am and 5pm ( last booking at 4pm )", HttpStatusCode.BadRequest);
        }

        return new Result(HttpStatusCode.OK);
    }
}