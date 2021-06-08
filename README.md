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

Api port number : 5001  (localhost:5001/api)


### Install project via Visual Studio IIS Express : 
If you prefer to run the project via Visual Studio without any Docker containers : 

First, you should set your Sql connection string in
LoyaltyPrime.Web.Api project appsettings.json : 

```
  "ConnectionStrings": {
    "DefaultConnection": "Server=host.docker.internal,1433;Initial Catalog=LoyaltyPrimeDb;User ID=SA;Password=Pa$$word2019;"
  },
```
