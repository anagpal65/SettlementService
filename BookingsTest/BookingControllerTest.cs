using SettlementService.Models;
using System.Net;


namespace BookingTest
{
    [TestClass]
    public class BookingControllerTest
    {
        private IBookingService _bookingService;

        [TestInitialize]
        public void Setup()
        {
            _bookingService = new BookingService();
        }

        [TestMethod]
        public void AddBooking_WhenBookingIsValid_ReturnsOk()
        {
            // Arrange
            var booking = new AddBooking() { BookingTime = "10:00", Name = "John Doe" };

            // Act
            var result = _bookingService.AddBooking(booking);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.httpStatusCode);
        }

        [TestMethod]
        public void AddBooking_WhenBookingIsNotValid_ReturnsBadRequest()
        {
            // Arrange
            var booking = new AddBooking() { BookingTime = "10:00", Name = "" };

            // Act
            var result = _bookingService.AddBooking(booking);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.httpStatusCode);
        }

        [TestMethod]
        [DataRow("10:00:00")]
        [DataRow("25:00")]
        public void AddBooking_WhenBookingTimeIsNotValid_ReturnsBadRequest(string time)
        {
            // Arrange
            var booking = new AddBooking() { BookingTime = time, Name = "John Doe" };

            // Act
            var result = _bookingService.AddBooking(booking);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.httpStatusCode);
        }

        [TestMethod]
        [DataRow("08:00")]
        [DataRow("18:00")]
        [DataRow("16:01")]
        public void AddBooking_WhenBookingTimeIsNotBetween9and5_ReturnsBadRequest(string time)
        {
            // Arrange
            var booking = new AddBooking() { BookingTime = time, Name = "John Doe" };

            // Act
            var result = _bookingService.AddBooking(booking);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.httpStatusCode);
        }

        [TestMethod]
        public void AddBooking_WhenBookingTimeIsAlreadyBooked_ReturnsBadRequest()
        {
            // Arrange
            var booking = new AddBooking() { BookingTime = "10:00", Name = "John Doe" };
            _bookingService.AddBooking(booking);
            _bookingService.AddBooking(booking);
            _bookingService.AddBooking(booking);
            _bookingService.AddBooking(booking);

            // Act
            var result = _bookingService.AddBooking(booking);

            // Assert
            Assert.AreEqual(HttpStatusCode.Conflict, result.httpStatusCode);
        }

    }
}