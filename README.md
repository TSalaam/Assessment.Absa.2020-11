# Assessment.Absa.2020-11

## Description

This solution was built using ASP.NET Core 3.1 and comprises the following components and features:

### Api

* RESTful
* Entity Framework Core with SQLite
* NLog provider for logging
* Swagger UI for visualizing and interacting with the API’s resources
* FluentValidation for strongly-typed validation rules
* Custom response objects (Namespace: PhoneBook.Api.Infrastructure.Results)

### PhoneBook.Web

* ASP.NET MVC Core
* NLog provider for logging
* Communication with Api implemented using HttpClientFactory, allowing Polly policies to be applied to outgoing calls
* Jittered Retry Policy conditionally applied, i.e. to outgoing HTTP GET calls

### Note:

- Log files are written to the following locations:
  - C:\Application.Logging\PhoneBook\Api
  - C:\Application.Logging\PhoneBook\Web
