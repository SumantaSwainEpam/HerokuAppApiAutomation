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
    public class CreateBookingTest:BaseTest
    {   

        private BookingClient bookingClient=>CreateClient<BookingClient>();
       

        [Test]
        [Order(2)]
        [Category("Booking")]
        [Description("Create a booking and verify that it returns a valid booking ID.")]
        public void CreateBookingShouldReturnValidBookingId()
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



            var response = bookingClient.CreateBooking(bookingRequest);

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

    }
}
