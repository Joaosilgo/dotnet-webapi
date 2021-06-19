# 🌮 DOTNET API

![Info](https://images.unsplash.com/photo-1623282033815-40b05d96c903?ixid=MnwyMjIwNDh8MHwxfGNvbGxlY3Rpb258MXwzY1hKR09YdkpZOHx8fHx8Mnx8MTYyNDEyMzM5MQ&ixlib=rb-1.2.1)

````bash
dotnet add dotnet-webapi  package Microsoft.EntityFrameworkCore.InMemory --version 5.0.6
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Hangfire.Core --version 1.7.23
dotnet add package Hangfire.AspNetCore --version 1.7.23
dotnet add package Hangfire.MemoryStorage --version 1.7.0


````

## In-memory database provider for Entity Framework Core (to be used for testing purposes)

## Postman

## DbInitializer

## Swagger

## TO-DO

- [x] In-memory database provider for Entity Framework Core (to be used for testing purposes)
- [x] Setting up Swagger (.NET Core) using the Authorization headers (Bearer)
- [x] Autenticação e Autorização via Token (Bearer e JWT)
- [x] Setup Docker Container and Deploy in Heroku
- [x] IncludeXmlComments in Swagger
- [x] Hangfire Core to perform background processing/tasks and Jobs
- [x] Override IDashboardAuthorizationFilter to CustomAuthorizationFilter to Return Hangfire DashBoard in Production (It was Protected By default)
- [x] [EnableCors] To Solve the Proble with Cors Policy
- [ ] Contact the media
  
## Docker

![Docker](https://images.unsplash.com/photo-1520218750893-2be45c7cbf63?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwyMjIwNDh8MHwxfGNvbGxlY3Rpb258MXwzY1hKR09YdkpZOHx8fHx8Mnx8MTYyNDEyMzc0NQ&ixlib=rb-1.2.1&q=80&w=400)

Create a Dockerfile or generate in CRL+P Add Docker Compose

````bash
code Dockerfile
docker build .

````

So that we can Deploy in Heroku via Docker
In Dockerfile change:

````Dockerfile
# ENTRYPOINT ["dotnet", "<app-name>.dll"]
# Opção utilizada pelo Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet <app-name>.dll
````

## Deploy Heroku

````bash

docker build .
heroku login
heroku apps:create dotnet-webapi
heroku container:login
heroku container:push web -a dotnet-webapi
heroku container:release web -a dotnet-webapi
heroku logs --tail
````

|     Schemas     |
| :-------------: |
|    Category     |
|    Business     |
| WeatherForecast |

|  Syntax   |                    URL                     |
| :-------: | :----------------------------------------: |
|  Swagger  | [Swagger](https://localhost:5001/swagger/) |
| Paragraph |                    Text                    |

````bash
echo "# dotnet-webapi" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/Joaosilgo/dotnet-webapi.git
git push -u origin main

````

## Log Problems

### Configuring Authorization

"Hangfire Dashboard exposes sensitive information about your background jobs, including method names and serialized arguments as well as gives you an opportunity to manage them by performing different actions – retry, delete, trigger, etc. So it is really important to restrict access to the Dashboard.

To make it secure by default, only local requests are allowed, however you can change this by passing your own implementations of the IDashboardAuthorizationFilter interface, whose Authorize method is used to allow or prohibit a request. The first step is to provide your own implementation."

To fix the Dashboard view in Production we have to Make  

````C#
using Hangfire.Dashboard;

namespace dotnet_webapi.Services
{
    public class CustomAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;//we can make a condition to just retrive if a certain User/Role is Logged 
        }
    }
}
````
