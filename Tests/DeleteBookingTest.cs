using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuAppApiAutomation.Tests
{
    public class DeleteBookingTest:BaseTest
    {

        private BookingClient bookingClient => CreateClient<BookingClient>();

        [Test]
        [Category("DeleteBooking")]
        [Description("Delete a booking by ID and verify it is removed.")]
        public void DeleteBookingById_ShouldDeleteSuccessfully()
        {
            var bookingRequest = new BookingRequest
            {
                Firstname = "Smith",
                Lastname = "Hock",
                Totalprice = 321,
                Depositpaid = true,
                Bookingdates = new BookingDates
                {
                    Checkin = "2021-06-01",
                    Checkout = "2021-06-10"
                },
                Additionalneeds = "Lunch"
            };

            var createResponse = bookingClient.CreateBooking(bookingRequest);
            int bookingId = createResponse.BookingId;

            var deleteSuccess = bookingClient.DeleteBooking(bookingId);
            Assert.That(deleteSuccess, Is.True, "Booking should be deleted successfully.");

            var deletedBooking = bookingClient.GetBookingById(bookingId);
            Assert.That(deletedBooking, Is.Null, "Booking should not exist after deletion.");

            TestContext.WriteLine($"Booking with ID {bookingId} was successfully deleted.");

        }


    }
}
