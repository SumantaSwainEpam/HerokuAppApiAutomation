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
    
    
    public  class GetBookingById:BaseTest
    {
        private BookingClient bookingClient => CreateClient<BookingClient>();
       


        [Test]
        [Order(3)]
        [Category("GetBooking Details")]
        [Description("Get a booking by ID and verify the returned data is correct.")]
        public void GetBookingById_ShouldReturnCorrectDetails()
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


            int bookingId = response.BookingId;

            var booking = bookingClient.GetBookingById(bookingId);

            Assert.That(booking.Firstname, Is.EqualTo(bookingRequest.Firstname));
            Assert.That(booking.Lastname, Is.EqualTo(bookingRequest.Lastname));
            Assert.That(booking.Totalprice, Is.EqualTo(bookingRequest.Totalprice));
            Assert.That(booking.Depositpaid, Is.EqualTo(bookingRequest.Depositpaid));
            Assert.That(booking.Bookingdates.Checkin, Is.EqualTo(bookingRequest.Bookingdates.Checkin));
            Assert.That(booking.Bookingdates.Checkout, Is.EqualTo(bookingRequest.Bookingdates.Checkout));
            Assert.That(booking.Additionalneeds, Is.EqualTo(bookingRequest.Additionalneeds));

            TestContext.WriteLine("Retrieved Booking:");
            TestContext.WriteLine(JsonConvert.SerializeObject(booking, Formatting.Indented));
        }


    }


}

