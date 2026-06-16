using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Helpers;
using HerokuAppApiAutomation.Models;
using Newtonsoft.Json;

namespace HerokuAppApiAutomation.Tests
{
    [TestFixture]
    public class CreateBookingTest : BaseTest
    {
        private BookingClient _bookingClient;
        private int _bookingId;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _bookingClient = CreateClient<BookingClient>();
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
        [Category("Booking")]
        [Description("Create a booking and verify that the response contains a valid booking ID and all fields match.")]
        public void CreateBooking_ShouldReturnValidBookingId()
        {
            var bookingRequest = BookingRequestBuilder.Build(
                firstname: "Sumanta",
                lastname: "Swain",
                totalprice: 521,
                additionalneeds: "Dinner");

            var response = _bookingClient.CreateBooking(bookingRequest);
            _bookingId = response.BookingId;

            Assert.That(response.BookingId, Is.GreaterThan(0), "Booking ID should be greater than 0");
            Assert.That(response.Booking.Firstname, Is.EqualTo(bookingRequest.Firstname));
            Assert.That(response.Booking.Lastname, Is.EqualTo(bookingRequest.Lastname));
            Assert.That(response.Booking.Totalprice, Is.EqualTo(bookingRequest.Totalprice));
            Assert.That(response.Booking.Depositpaid, Is.EqualTo(bookingRequest.Depositpaid));
            Assert.That(response.Booking.Bookingdates.Checkin, Is.EqualTo(bookingRequest.Bookingdates.Checkin));
            Assert.That(response.Booking.Bookingdates.Checkout, Is.EqualTo(bookingRequest.Bookingdates.Checkout));
            Assert.That(response.Booking.Additionalneeds, Is.EqualTo(bookingRequest.Additionalneeds));

            var expectedJson = JsonConvert.SerializeObject(bookingRequest);
            var actualJson = JsonConvert.SerializeObject(response.Booking);
            Assert.That(actualJson, Is.EqualTo(expectedJson), "Booking details do not match request payload.");

            TestContext.WriteLine("Booking ID: " + response.BookingId);
            TestContext.WriteLine("Booking Response JSON: " + actualJson);
        }

        [Test]
        [Category("Booking")]
        [Description("Created booking should be retrievable by its ID with all fields intact.")]
        public void CreateBooking_ShouldBePersistableAndRetrievable()
        {
            var bookingRequest = BookingRequestBuilder.Build(firstname: "Alice", lastname: "Walker");
            var createResponse = _bookingClient.CreateBooking(bookingRequest);
            _bookingId = createResponse.BookingId;

            var retrieved = _bookingClient.GetBookingById(_bookingId);

            Assert.That(retrieved, Is.Not.Null, "Created booking should be retrievable.");
            Assert.That(retrieved!.Firstname, Is.EqualTo(bookingRequest.Firstname));
            Assert.That(retrieved.Lastname, Is.EqualTo(bookingRequest.Lastname));
            Assert.That(retrieved.Totalprice, Is.EqualTo(bookingRequest.Totalprice));
        }
    }
}
