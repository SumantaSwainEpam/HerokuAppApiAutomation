using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Helpers;
using HerokuAppApiAutomation.Models;
using Newtonsoft.Json;
using System.Net;

namespace HerokuAppApiAutomation.Tests
{
    [TestFixture]
    public class UpdateBookingTest : BaseTest
    {
        private BookingClient _bookingClient;
        private int _bookingId;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _bookingClient = CreateClient<BookingClient>();
        }

        [SetUp]
        public void SetUp()
        {
            var created = _bookingClient.CreateBooking(BookingRequestBuilder.Build());
            _bookingId = created.BookingId;
        }

        [TearDown]
        public void TearDown()
        {
            if (_bookingId > 0)
            {
                _bookingClient.DeleteBooking(_bookingId);
                _bookingId = 0;
            }
        }

        [Test]
        [Category("UpdateBooking")]
        [Description("Full update of a booking should persist all changed fields.")]
        public void UpdateBooking_ShouldUpdateAllFields()
        {
            var updatedRequest = BookingRequestBuilder.Build(
                firstname: "Bravis",
                lastname: "Victory",
                totalprice: 999,
                depositpaid: false,
                additionalneeds: "Breakfast");

            var updateResult = _bookingClient.UpdateBooking(_bookingId, updatedRequest);

            Assert.That(updateResult.Firstname, Is.EqualTo(updatedRequest.Firstname));
            Assert.That(updateResult.Lastname, Is.EqualTo(updatedRequest.Lastname));
            Assert.That(updateResult.Totalprice, Is.EqualTo(updatedRequest.Totalprice));
            Assert.That(updateResult.Depositpaid, Is.EqualTo(updatedRequest.Depositpaid));
            Assert.That(updateResult.Bookingdates.Checkin, Is.EqualTo(updatedRequest.Bookingdates.Checkin));
            Assert.That(updateResult.Bookingdates.Checkout, Is.EqualTo(updatedRequest.Bookingdates.Checkout));
            Assert.That(updateResult.Additionalneeds, Is.EqualTo(updatedRequest.Additionalneeds));

            TestContext.WriteLine("Updated Booking:");
            TestContext.WriteLine(JsonConvert.SerializeObject(updateResult, Formatting.Indented));
        }

        [Test]
        [Category("UpdateBooking")]
        [Description("Update without auth token should return 403 Forbidden.")]
        public void UpdateBooking_WithoutAuth_ShouldReturn403()
        {
            var updatedRequest = BookingRequestBuilder.Build(firstname: "Unauthorized");
            var response = _bookingClient.ExecuteUpdateBooking(_bookingId, updatedRequest, withAuth: false);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden),
                "Update without auth should return 403 Forbidden.");
        }
    }
}
