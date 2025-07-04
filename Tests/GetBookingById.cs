﻿using HerokuAppApiAutomation.Clients;
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
        [Category("UpdateBooking")]
        [Description("Update a booking by ID and verify the returned data.")]
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
                Firstname = "Sam",
                Lastname = "Delton",
                Totalprice = 888,
                Depositpaid = false,
                Bookingdates = new BookingDates
                {
                    Checkin = "2024-01-01",
                    Checkout = "2024-12-31"
                },
                Additionalneeds = "Breakfast"
            };

          
            var updateResponse = bookingClient.UpdateBooking(bookingId, updatedRequest);

          
            var updatedBooking = bookingClient.GetBookingById(bookingId);

            Assert.That(updatedBooking.Firstname, Is.EqualTo(updatedRequest.Firstname));
            Assert.That(updatedBooking.Lastname, Is.EqualTo(updatedRequest.Lastname));
            Assert.That(updatedBooking.Totalprice, Is.EqualTo(updatedRequest.Totalprice));
            Assert.That(updatedBooking.Depositpaid, Is.EqualTo(updatedRequest.Depositpaid));
            Assert.That(updatedBooking.Bookingdates.Checkin, Is.EqualTo(updatedRequest.Bookingdates.Checkin));
            Assert.That(updatedBooking.Bookingdates.Checkout, Is.EqualTo(updatedRequest.Bookingdates.Checkout));
            Assert.That(updatedBooking.Additionalneeds, Is.EqualTo(updatedRequest.Additionalneeds));

            TestContext.WriteLine("Updated Booking:");
            TestContext.WriteLine(JsonConvert.SerializeObject(updatedBooking, Formatting.Indented));

        }

    }


}

