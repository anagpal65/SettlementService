using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettlementService.Controllers;
using System;

namespace SettlementService.Tests
{
    [TestClass]
    public class BookingControllerTests
    {
        // The bookingTime property should be a 24-hour time (00:00 - 23:59)
        [TestMethod]
        public void BookingTimeFormatTest()
        {
            // Arrange
            var booking = new AddBooking
            {
                Name = "Test",
                BookingTime = "25:00"
            };

            // Act
            var result = BookingController.AddBooking(booking);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.httpStatusCode);
        }

    }
}
