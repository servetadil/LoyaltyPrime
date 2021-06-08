# LoyaltyPrime
LoyaltyPrime Member Management System

## Installation

Project and database have been developed with Entity Framework Code first approach, therefore, once you run the project, it will automatically create the Database & test api users.

### Install project via Docker : 
If you prefer to run project from Linux container on Docker, run this code on the root directory folder of docker-compose file : 

```
docker-compose up
```
Sql instance will be up from port number : 1433

Api port number : 5000  (localhost:5000/api)


### Install project via Visual Studio IIS Express : 
If you prefer to run the project via Visual Studio without any Docker containers : 

First, you should set your Sql connection string in
LoyaltyPrime.Web.Api project appsettings.json : 

```
  "ConnectionStrings": {
    "DefaultConnection": "Server=host.docker.internal,1433;Initial Catalog=LoyaltyPrimeDb;User ID=SA;Password=Pa$$word2019;"
  },
```

### Technologies
* .NET Core 3.1
* Entity Framework Core
* Mssql
* XUnit
* Docker

### Libraries

**EntityFrameworkCore**: It has been used as ORM framework.

Documentation: [https://docs.microsoft.com/en-us/ef/core/](https://docs.microsoft.com/en-us/ef/core/)

**MediatR**: It has been used for reduce dependencies between objects.

Documentation: [https://github.com/jbogard/MediatR/wiki](https://github.com/jbogard/MediatR/wiki)

**System.IdentityModel.Tokens.Jwt(JWT)**: It has been used for authentication.

Documentation: [https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt](https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt)

**FluentValidation:** It has been used for validation of commands and queries.

Documentation: [https://github.com/FluentValidation/FluentValidation](https://github.com/FluentValidation/FluentValidation)

**AutoMapper**: It has been used for object-to-object mapping from entity to Dto or ViewModels.

Documentation: [https://docs.automapper.org/en/stable/](https://docs.automapper.org/en/stable/)

**Newtonsoft:** It has been used for json convert from enum.

Documentation: [https://www.newtonsoft.com/json/help/html/Introduction.htm](https://www.newtonsoft.com/json/help/html/Introduction.htm)

**Swagger:** It has been used for api documentation and structure.

Documentation: [https://swagger.io/docs/](https://swagger.io/docs/)

**FluentValidation:** It has been used for improve testing readability.

Documentation: [https://fluentassertions.com/introduction](https://github.com/fluentassertions/fluentassertions)**

**xUnit:** It has been used for unit tests.

Documentation: [https://xunit.net/docs/getting-started/netcore/cmdline](https://xunit.net/docs/getting-started/netcore/cmdline)**

**EntityFrameworkCore.InMemory**: It has been used to create mock database.

Documentation: [https://docs.microsoft.com/en-us/ef/core/providers/in-memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory)

**Moq**: It has been used to create mock objects.

Documentation: [https://documentation.help/Moq/](https://documentation.help/Moq/)
