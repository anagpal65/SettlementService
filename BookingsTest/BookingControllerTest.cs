using Moq;
using SettlementService.Models;
using SettlementService.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace BookingTest
{
    [TestClass]
    public class BookingControllerTest
    {
        [TestMethod]
        public void AddBooking_WhenBookingIsValid_ReturnsOk()
        {
            // Arrange
            var bookingService = new Mock<IBookingService>();
            bookingService.Setup(x => x.AddBooking(It.IsAny<AddBooking>())).Returns(new Result<SuccessfulBooking>(new SuccessfulBooking(), HttpStatusCode.OK));
            var controller = new BookingController(bookingService.Object);
            var booking = new AddBooking() { BookingTime = "10:00", Name = "John Doe" };

            // Act
            var result = controller.Post(booking);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<SuccessfulBooking>));
        }
    }
}