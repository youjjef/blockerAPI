# blockerAPI
This project is an ASP.NET Core Web API that provides functionality to block and look up IP addresses. It uses an external IP geolocation service to retrieve information about a given IP address, such as the country code, country name, and organization. Additionally, it allows blocking of IP addresses.

## Features

- Lookup IP address information
- Retrieve country code, country name, and organization for an IP address
- Block and unblock IP addresses
- Swagger UI for API documentation and testing

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- An API key for the IP geolocation service (e.g., IPAPI)

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/youjjef/blockerAPI.git
   cd blockerAPI

2. Update the appsettings.json file with your API key:
   {
    "ApiSettings": {
        "IPGeolocationAPIKey": "your-actual-api-key"
    }
}
3. Restore the dependencies and build the project:
 dotnet restore
 dotnet build
 dotnet run

