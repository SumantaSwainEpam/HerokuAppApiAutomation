using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Helpers;
using HerokuAppApiAutomation.Models;
using Newtonsoft.Json;

namespace HerokuAppApiAutomation.Tests
{
    [TestFixture]
    public class GetBookingTest : BaseTest
    {
        private BookingClient _bookingClient;
        private int _bookingId;
        private BookingRequest _createdRequest;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _bookingClient = CreateClient<BookingClient>();
            _createdRequest = BookingRequestBuilder.Build(firstname: "James", lastname: "Brown", totalprice: 250);
            var created = _bookingClient.CreateBooking(_createdRequest);
            _bookingId = created.BookingId;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (_bookingId > 0)
                _bookingClient.DeleteBooking(_bookingId);
        }

        [Test]
        [Category("Booking")]
        [Description("Get a booking by ID and verify all fields match the created data.")]
        public void GetBookingById_ShouldReturnCorrectDetails()
        {
            var booking = _bookingClient.GetBookingById(_bookingId);

            Assert.That(booking, Is.Not.Null, "Booking should not be null.");
            Assert.That(booking!.Firstname, Is.EqualTo(_createdRequest.Firstname));
            Assert.That(booking.Lastname, Is.EqualTo(_createdRequest.Lastname));
            Assert.That(booking.Totalprice, Is.EqualTo(_createdRequest.Totalprice));
            Assert.That(booking.Depositpaid, Is.EqualTo(_createdRequest.Depositpaid));
            Assert.That(booking.Bookingdates.Checkin, Is.EqualTo(_createdRequest.Bookingdates.Checkin));
            Assert.That(booking.Bookingdates.Checkout, Is.EqualTo(_createdRequest.Bookingdates.Checkout));
            Assert.That(booking.Additionalneeds, Is.EqualTo(_createdRequest.Additionalneeds));

            TestContext.WriteLine(JsonConvert.SerializeObject(booking, Formatting.Indented));
        }

        [Test]
        [Category("Booking")]
        [Description("Get all bookings should return a non-empty list containing the created booking.")]
        public void GetAllBookings_ShouldReturnListContainingCreatedBooking()
        {
            var ids = _bookingClient.GetAllBookingIds();

            Assert.That(ids, Is.Not.Null.And.Not.Empty, "Booking IDs list should not be empty.");
            Assert.That(ids, Contains.Item(_bookingId), "Created booking ID should be in the list.");
        }

        [Test]
        [Category("Booking")]
        [Description("Get a booking with a non-existent ID should return null (404).")]
        public void GetBookingById_WithNonExistentId_ShouldReturnNull()
        {
            var booking = _bookingClient.GetBookingById(int.MaxValue);
            Assert.That(booking, Is.Null, "Non-existent booking ID should return null.");
        }
    }
}
