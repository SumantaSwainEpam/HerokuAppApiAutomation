
## 🧪 HerokuAppAPIAutomation

### Overview
**HerokuAppAPIAutomation** is a fully implemented API testing framework built using **C# .NET**, designed to validate the RESTful endpoints of the Heroku App. It uses **NUnit** for test execution, **RestSharp** for HTTP client operations, and includes structured logging and reporting capabilities.

---

### 🏗️ Project Structure

```
HerokuAppAPIAutomation/
│
├── Tests/                  # NUnit test classes
├── Clients/                # RestSharp API clients
├── Models/                 # Request/Response models
├── Utilities/              # Logging, config, helpers
├── Reports/                # ReportPortal or custom reports
├── Config/                 # Environment configs
├── HerokuAppAPIAutomation.sln
└── README.md
```

---

### 🛠️ Technologies Used

- **C# .NET**
- **NUnit**
- **RestSharp**
- **Log4Net / Serilog**
- **ReportPortal (optional)**
- **Newtonsoft.Json**

---

### 🚀 How to Run Tests

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/HerokuAppAPIAutomation.git
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Run tests**
   ```bash
   dotnet test
   ```

---

### 📦 Features

- ✅ Modular API client structure using RestSharp
- ✅ Centralized logging with Log4Net or Serilog
- ✅ Optional integration with ReportPortal for real-time reporting
- ✅ Environment-based configuration
- ✅ JSON serialization/deserialization with Newtonsoft.Json
- ✅ Retry and timeout handling

---

### 📊 Reporting

- **ReportPortal** integration (if enabled) provides:
  - Real-time dashboards
  - Execution logs
  - Historical test trends
  - CI/CD compatibility

---

### 🧪 Sample Test Case

```csharp
[Test]
public void GetStatusCode_ShouldReturn200()
{
    var response = _statusClient.GetStatusCode(200);
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
}
```

---

### 🔐 Logging

- Logs are stored in the `Reports/Logs/` directory.
- Supports multiple log levels (INFO, DEBUG, ERROR).
- Configurable via `log4net.config` or `appsettings.json`.

---

### 👨‍💻 Author

**Sumanta Swain**  
Junior Software Test Automation Engineer  
