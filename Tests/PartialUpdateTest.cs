using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Helpers;
using HerokuAppApiAutomation.Models;
using Newtonsoft.Json;
using System.Net;

namespace HerokuAppApiAutomation.Tests
{
    [TestFixture]
    public class PartialUpdateTest : BaseTest
    {
        private BookingClient _bookingClient;
        private int _bookingId;
        private BookingRequest _originalRequest;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _bookingClient = CreateClient<BookingClient>();
        }

        [SetUp]
        public void SetUp()
        {
            _originalRequest = BookingRequestBuilder.Build(
                firstname: "John",
                lastname: "Lammer",
                totalprice: 123,
                additionalneeds: "Lunch");
            var created = _bookingClient.CreateBooking(_originalRequest);
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
        [Category("PatchBooking")]
        [Description("Partial update should change only specified fields and leave others unchanged.")]
        public void PatchBooking_ShouldUpdateOnlySpecifiedFields()
        {
            var partialUpdate = new { firstname = "John", lastname = "Smith" };

            var patchedResponse = _bookingClient.PatchBooking(_bookingId, partialUpdate);

            Assert.That(patchedResponse.Firstname, Is.EqualTo(partialUpdate.firstname));
            Assert.That(patchedResponse.Lastname, Is.EqualTo(partialUpdate.lastname));
            Assert.That(patchedResponse.Totalprice, Is.EqualTo(_originalRequest.Totalprice));
            Assert.That(patchedResponse.Depositpaid, Is.EqualTo(_originalRequest.Depositpaid));
            Assert.That(patchedResponse.Bookingdates.Checkin, Is.EqualTo(_originalRequest.Bookingdates.Checkin));
            Assert.That(patchedResponse.Bookingdates.Checkout, Is.EqualTo(_originalRequest.Bookingdates.Checkout));
            Assert.That(patchedResponse.Additionalneeds, Is.EqualTo(_originalRequest.Additionalneeds));

            TestContext.WriteLine("Partially Updated Booking:");
            TestContext.WriteLine(JsonConvert.SerializeObject(patchedResponse, Formatting.Indented));
        }

        [Test]
        [Category("PatchBooking")]
        [Description("Partial update without auth token should return 403 Forbidden.")]
        public void PatchBooking_WithoutAuth_ShouldReturn403()
        {
            var partialUpdate = new { firstname = "Unauthorized" };
            var response = _bookingClient.ExecutePatchBooking(_bookingId, partialUpdate, withAuth: false);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden),
                "Patch without auth should return 403 Forbidden.");
        }
    }
}
