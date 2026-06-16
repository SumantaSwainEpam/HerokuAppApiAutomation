# Heroku App Api Automation

API test automation suite for the [Restful Booker](https://restful-booker.herokuapp.com) API, built with .NET 8, NUnit 3, and RestSharp.

## Prerequisites

- .NET 8 SDK

## Configuration

Create `Credentials/AppSettings.json` (not committed to source control):

```json
{
  "ApiTesting": {
    "Endpoint": "https://restful-booker.herokuapp.com"
  },
  "Credential": {
    "username": "admin",
    "password": "password123"
  }
}
```

## Running Tests

```bash
# Run all tests
dotnet test

# Run a specific category
dotnet test --filter "Category=Booking"
dotnet test --filter "Category=UpdateBooking"
dotnet test --filter "Category=DeleteBooking"
dotnet test --filter "Category=PatchBooking"
dotnet test --filter "Category=Token Generation"

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"
```

## Test Coverage

| Test Class | Category | Tests |
|---|---|---|
| `TokenGenerateTest` | Token Generation | Valid credentials, invalid credentials (Bad credentials) |
| `CreateBookingTest` | Booking | Create with field validation, create and retrieve |
| `GetBookingTest` | Booking | Get by ID, get all bookings, non-existent ID (404) |
| `UpdateBookingTest` | UpdateBooking | Full update, update without auth (403) |
| `DeleteBookingTest` | DeleteBooking | Delete and verify removed, delete without auth (403) |
| `PartialUpdateTest` | PatchBooking | PATCH specific fields only, patch without auth (403) |

## Project Structure

```
HerokuAppApiAutomation/
├── Clients/
│   ├── BaseClient.cs          # Base HTTP client with token caching
│   ├── BookingClient.cs       # Booking CRUD operations
│   └── TokenGenerate.cs       # Auth token generation
├── Models/
│   ├── AuthRequest.cs
│   ├── AuthResponse.cs
│   ├── BookingRequest.cs
│   └── BookingResponse.cs
├── Tests/
│   ├── BaseTest.cs
│   ├── TokenGenerateTest.cs
│   ├── CreateBookingTest.cs
│   ├── GetBookingById.cs      # GetBookingTest class
│   ├── UpdateBookingTest.cs
│   ├── DeleteBookingTest.cs
│   └── PartialUpdateTest.cs
├── Helpers/
│   ├── Config.cs              # AppSettings.json loader
│   └── BookingRequestBuilder.cs  # Test data factory with dynamic dates
└── Credentials/
    └── AppSettings.json       # Not committed — see Configuration above
```

## CI/CD

GitHub Actions workflow (`.github/workflows/HerokuAppApiAutomation.yml`) runs on every push to `master`. The `APPSETTINGS_JSON` secret must be configured in repository Settings → Secrets.

## Author

**Sumanta Swain**
- Email: sumanta_swain@epam.com
- GitHub: [@SumantaSwainEpam](https://github.com/SumantaSwainEpam)
