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
    public class UpdateBookingTest:BaseTest
    {
        private BookingClient bookingClient => CreateClient<BookingClient>();

        [Test]
        [Order(4)]
        [Category("UpdateBooking")]
        [Description("Update a booking by ID and verify the updated data.")]
        public void UpdateBookingById_ShouldUpdateCorrectDetails()
        {
           
            var bookingRequest = new BookingRequest
            {
                Firstname = "Sumanta",
                Lastname = "Swain",
                Totalprice = 521,
                Depositpaid = true,
                Bookingdates = new BookingDates
                {
                    Checkin = "2020-11-09",
                    Checkout = "2022-05-07"
                },
                Additionalneeds = "Dinner"
            };

           
            var createResponse = bookingClient.CreateBooking(bookingRequest);
            Assert.That(createResponse.BookingId, Is.GreaterThan(0), "Booking ID should be greater than 0");

            int bookingId = createResponse.BookingId;

           
            var updatedRequest = new BookingRequest
            {
                Firstname = "Bravis",
                Lastname = "victory",
                Totalprice = 999,
                Depositpaid = false,
                Bookingdates = new BookingDates
                {
                    Checkin = "2024-01-01",
                    Checkout = "2024-12-31"
                },
                Additionalneeds = "Breakfast"
            };

           
            var updateResult = bookingClient.UpdateBooking(bookingId, updatedRequest);

           
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


    }
}
