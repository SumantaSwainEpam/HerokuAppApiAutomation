using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuAppApiAutomation.Tests
{
    public class PartialUpdateTest:BaseTest
    {
        private BookingClient bookingClient => CreateClient<BookingClient>();

        [Test]
        [Category("PatchBooking")]
        [Description("Partially update a booking and verify that only specific fields are changed.")]
        public void PatchBookingById_ShouldUpdatePartialData()
        {
            var bookingRequest = new BookingRequest
            {
                Firstname = "John",
                Lastname = "Lammer",
                Totalprice = 123,
                Depositpaid = true,
                Bookingdates = new BookingDates
                {
                    Checkin = "2023-01-01",
                    Checkout = "2023-01-10"
                },
                Additionalneeds = "Lunch"
            };

            
            var createResponse = bookingClient.CreateBooking(bookingRequest);
            int bookingId = createResponse.BookingId;

            
            var partialUpdate = new
            {
                firstname = "Jhon",
                lastname = "Smith"
            };

            
            var patchedResponse = bookingClient.PatchBooking(bookingId, partialUpdate);

            
            Assert.That(patchedResponse.Firstname, Is.EqualTo(partialUpdate.firstname));
            Assert.That(patchedResponse.Lastname, Is.EqualTo(partialUpdate.lastname));
            Assert.That(patchedResponse.Totalprice, Is.EqualTo(bookingRequest.Totalprice));
            Assert.That(patchedResponse.Depositpaid, Is.EqualTo(bookingRequest.Depositpaid));
            Assert.That(patchedResponse.Bookingdates.Checkin, Is.EqualTo(bookingRequest.Bookingdates.Checkin));
            Assert.That(patchedResponse.Bookingdates.Checkout, Is.EqualTo(bookingRequest.Bookingdates.Checkout));
            Assert.That(patchedResponse.Additionalneeds, Is.EqualTo(bookingRequest.Additionalneeds));

            TestContext.WriteLine("Partially Updated Booking:");
            TestContext.WriteLine(JsonConvert.SerializeObject(patchedResponse, Formatting.Indented));

        }


    }
}
