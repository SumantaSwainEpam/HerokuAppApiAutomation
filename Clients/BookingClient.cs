using HerokuAppApiAutomation.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace HerokuAppApiAutomation.Clients
{
    public class BookingClient : BaseClient
    {
        public BookingResponse CreateBooking(BookingRequest bookingRequest)
        {
            var request=CreateRequest("booking",Method.Post);

            request.AddJsonBody(bookingRequest);

            var response=_client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Booking creation failed.");
            Assert.That(response.Content, Is.Not.Null, "Response content is null.");

            var bookingResponse=JsonConvert.DeserializeObject<BookingResponse>(response.Content);   

            if(bookingResponse==null || bookingResponse.BookingId <= 0)
            {
                throw new InvalidOperationException("Booking ID is not found in response or is invalid.");
            }

            Assert.That(bookingResponse.BookingId, Is.GreaterThan(0), "Booking ID should be greater than 0.");

            return bookingResponse;

        }

        public BookingRequest GetBookingById(int bookingId)
        {
            var request = CreateRequest($"booking/{bookingId}", Method.Get);

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to retrieve booking by ID.");
            Assert.That(response.Content, Is.Not.Null, "Response content is null.");

            var booking = JsonConvert.DeserializeObject<BookingRequest>(response.Content);

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found in response.");
            }

            return booking;
        }



    }
}
