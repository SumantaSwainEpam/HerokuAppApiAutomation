using Newtonsoft.Json;

namespace HerokuAppApiAutomation.Models
{
    public class BookingResponse
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingRequest Booking { get; set; }
    }

    public class BookingIdResponse
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }
    }
}
