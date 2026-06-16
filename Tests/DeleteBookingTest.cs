using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Helpers;
using System.Net;

namespace HerokuAppApiAutomation.Tests
{
    [TestFixture]
    public class DeleteBookingTest : BaseTest
    {
        private BookingClient _bookingClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _bookingClient = CreateClient<BookingClient>();
        }

        [Test]
        [Category("DeleteBooking")]
        [Description("Delete a booking by ID and verify it is no longer retrievable.")]
        public void DeleteBooking_ShouldRemoveBookingPermanently()
        {
            var created = _bookingClient.CreateBooking(BookingRequestBuilder.Build(
                firstname: "Smith",
                lastname: "Hock",
                totalprice: 321,
                additionalneeds: "Lunch"));

            int bookingId = created.BookingId;

            var deleteSuccess = _bookingClient.DeleteBooking(bookingId);
            Assert.That(deleteSuccess, Is.True, "Booking should be deleted successfully.");

            var deletedBooking = _bookingClient.GetBookingById(bookingId);
            Assert.That(deletedBooking, Is.Null, "Booking should not exist after deletion.");

            TestContext.WriteLine($"Booking with ID {bookingId} was successfully deleted.");
        }

        [Test]
        [Category("DeleteBooking")]
        [Description("Delete without auth token should return 403 Forbidden.")]
        public void DeleteBooking_WithoutAuth_ShouldReturn403()
        {
            var created = _bookingClient.CreateBooking(BookingRequestBuilder.Build());
            int bookingId = created.BookingId;

            try
            {
                var response = _bookingClient.ExecuteDeleteBooking(bookingId, withAuth: false);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden),
                    "Delete without auth should return 403 Forbidden.");
            }
            finally
            {
                _bookingClient.DeleteBooking(bookingId);
            }
        }
    }
}
