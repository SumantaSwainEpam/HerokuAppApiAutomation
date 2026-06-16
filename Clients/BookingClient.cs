using HerokuAppApiAutomation.Models;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace HerokuAppApiAutomation.Clients
{
    public class BookingClient : BaseClient
    {
        public BookingResponse CreateBooking(BookingRequest bookingRequest)
        {
            var request = CreateRequest("booking", Method.Post);
            request.AddJsonBody(bookingRequest);
            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Booking creation failed.");
            Assert.That(response.Content, Is.Not.Null, "Response content is null.");

            var bookingResponse = JsonConvert.DeserializeObject<BookingResponse>(response.Content!);
            if (bookingResponse == null || bookingResponse.BookingId <= 0)
                throw new InvalidOperationException("Booking ID is not found in response or is invalid.");

            Assert.That(bookingResponse.BookingId, Is.GreaterThan(0), "Booking ID should be greater than 0.");
            return bookingResponse;
        }

        public BookingRequest? GetBookingById(int bookingId)
        {
            var request = CreateRequest($"booking/{bookingId}", Method.Get);
            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to retrieve booking by ID.");
            Assert.That(response.Content, Is.Not.Null, "Response content is null.");

            var booking = JsonConvert.DeserializeObject<BookingRequest>(response.Content!);
            if (booking == null)
                throw new InvalidOperationException("Booking not found in response.");

            return booking;
        }

        public List<int> GetAllBookingIds()
        {
            var request = CreateRequest("booking", Method.Get);
            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to get all bookings.");
            var ids = JsonConvert.DeserializeObject<List<BookingIdResponse>>(response.Content!);
            return ids?.Select(x => x.BookingId).ToList() ?? new List<int>();
        }

        public BookingRequest UpdateBooking(int bookingId, BookingRequest updatedData)
        {
            var response = ExecuteUpdateBooking(bookingId, updatedData, withAuth: true);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Booking update failed.");
            Assert.That(response.Content, Is.Not.Null, "Response content is null.");

            var updatedBooking = JsonConvert.DeserializeObject<BookingRequest>(response.Content!);
            if (updatedBooking == null)
                throw new InvalidOperationException("Updated booking response is null.");

            return updatedBooking;
        }

        public RestResponse ExecuteUpdateBooking(int bookingId, BookingRequest data, bool withAuth = true)
        {
            var request = CreateRequest($"booking/{bookingId}", Method.Put, withAuth);
            request.AddJsonBody(data);
            return _client.Execute(request);
        }

        public bool DeleteBooking(int bookingId)
        {
            var response = ExecuteDeleteBooking(bookingId, withAuth: true);
            bool isSuccess = response.StatusCode == HttpStatusCode.Created ||
                             response.StatusCode == HttpStatusCode.OK ||
                             response.StatusCode == HttpStatusCode.NoContent;
            Assert.That(isSuccess, Is.True, $"Booking deletion failed. StatusCode: {response.StatusCode}");
            return true;
        }

        public RestResponse ExecuteDeleteBooking(int bookingId, bool withAuth = true)
        {
            var request = CreateRequest($"booking/{bookingId}", Method.Delete, withAuth);
            return _client.Execute(request);
        }

        public BookingRequest PatchBooking(int bookingId, object partialUpdate)
        {
            var response = ExecutePatchBooking(bookingId, partialUpdate, withAuth: true);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Patch update failed.");
            Assert.That(response.Content, Is.Not.Null, "Response content is null.");

            var patchedBooking = JsonConvert.DeserializeObject<BookingRequest>(response.Content!);
            if (patchedBooking == null)
                throw new InvalidOperationException("Patched booking response is null.");

            return patchedBooking;
        }

        public RestResponse ExecutePatchBooking(int bookingId, object data, bool withAuth = true)
        {
            var request = CreateRequest($"booking/{bookingId}", Method.Patch, withAuth);
            request.AddJsonBody(data);
            return _client.Execute(request);
        }
    }
}
