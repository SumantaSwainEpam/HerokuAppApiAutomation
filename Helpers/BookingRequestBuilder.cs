using HerokuAppApiAutomation.Models;

namespace HerokuAppApiAutomation.Helpers
{
    public static class BookingRequestBuilder
    {
        public static BookingRequest Build(
            string firstname = "Test",
            string lastname = "User",
            int totalprice = 150,
            bool depositpaid = true,
            string additionalneeds = "Breakfast",
            string? checkin = null,
            string? checkout = null)
        {
            var today = DateTime.UtcNow.Date;
            return new BookingRequest
            {
                Firstname = firstname,
                Lastname = lastname,
                Totalprice = totalprice,
                Depositpaid = depositpaid,
                Additionalneeds = additionalneeds,
                Bookingdates = new BookingDates
                {
                    Checkin = checkin ?? today.ToString("yyyy-MM-dd"),
                    Checkout = checkout ?? today.AddDays(5).ToString("yyyy-MM-dd")
                }
            };
        }
    }
}
