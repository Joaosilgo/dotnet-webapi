# 🌮 DOTNET API

___

![Info](https://images.unsplash.com/photo-1623282033815-40b05d96c903?ixid=MnwyMjIwNDh8MHwxfGNvbGxlY3Rpb258MXwzY1hKR09YdkpZOHx8fHx8Mnx8MTYyNDEyMzM5MQ&ixlib=rb-1.2.1)

````bash
dotnet add dotnet-webapi  package Microsoft.EntityFrameworkCore.InMemory --version 5.0.6
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Hangfire.Core --version 1.7.23
dotnet add package Hangfire.AspNetCore --version 1.7.23
dotnet add package Hangfire.MemoryStorage --version 1.7.0
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 5.0.7


````

___

## In-memory database provider for Entity Framework Core (to be used for testing purposes)

## Postman

## DbInitializer

## Swagger

___

## TO-DO

![To DO](https://images.unsplash.com/photo-1512758017271-d7b84c2113f1?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwyMjIwNDh8MHwxfGNvbGxlY3Rpb258MXwzY1hKR09YdkpZOHx8fHx8Mnx8MTYyNDEyNDIyOA&ixlib=rb-1.2.1&q=80&w=1080)

- [x] In-memory database provider for Entity Framework Core (to be used for testing purposes)
- [x] Setting up Swagger (.NET Core) using the Authorization headers (Bearer)
- [x] Autenticação e Autorização via Token (Bearer e JWT)
- [x] Setup Docker Container and Deploy in Heroku
- [x] IncludeXmlComments in Swagger
- [x] Hangfire Core to perform background processing/tasks and Jobs
- [x] Override IDashboardAuthorizationFilter to CustomAuthorizationFilter to Return Hangfire DashBoard in Production (It was Protected By default)
- [x] [EnableCors] To Solve the Proble with Cors Policy
- [x] RespectBrowserAcceptHeader
- [x] Handle JSON Patch requests
- [ ] Contact the media
  
  ___

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

___

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

## JSON Patch?

JSON Patch is a format for describing changes to a [JSON](http://json.com) document. It can be used to avoid sending a whole document when only a part has changed. When used in combination with the [HTTP PATCH method](http://tools.ietf.org/html/rfc5789), it allows partial updates for HTTP APIs in a standards compliant way.

The patch documents are themselves JSON documents.

## Simple example

### The original document

````json
    {
      "baz": "qux",
      "foo": "bar"
    }
````

### The patch

````json
    [
      { "op": "replace", "path": "/baz", "value": "boo" },
      { "op": "add", "path": "/hello", "value": ["world"] },
      { "op": "remove", "path": "/foo" }
    ]
````

### The result

````json
    {
      "baz": "boo",
      "hello": ["world"]
    }
````

### How it works

A JSON Patch document is just a JSON file containing an array of patch operations. The patch operations supported by JSON Patch are "add", "remove", "replace", "move", "copy" and "test". The operations are applied in order: if any of them fail then the whole patch operation should abort.
___

## Testing

## Log Problems

## 1.0   Configuring Authorization

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

## 2.0  JSON Patch and Swagger

"Swashbuckle.AspNetCore doesn't work propertly with this type JsonPatchDocument, which doesn’t represent the expected patch request doument."

You need to custome a document filter to modify the generated specification.

````C#
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace dotnet_webapi.Services
{
   public class JsonPatchDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var schemas = swaggerDoc.Components.Schemas.ToList();
        foreach (var item in schemas)
        {
            if (item.Key.StartsWith("Operation") || item.Key.StartsWith("JsonPatchDocument"))
                swaggerDoc.Components.Schemas.Remove(item.Key);
        }

        swaggerDoc.Components.Schemas.Add("Operation", new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                {"op", new OpenApiSchema{ Type = "string" } },
                {"value", new OpenApiSchema{ Type = "string"} },
                {"path", new OpenApiSchema{ Type = "string" } }
            }
        });

        swaggerDoc.Components.Schemas.Add("JsonPatchDocument", new OpenApiSchema
        {
            Type = "array",
            Items = new OpenApiSchema
            {
                Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Operation" }
            },
            Description = "Array of operations to perform"
        });

        foreach (var path in swaggerDoc.Paths.SelectMany(p => p.Value.Operations)
        .Where(p => p.Key == Microsoft.OpenApi.Models.OperationType.Patch))
        {
            foreach (var item in path.Value.RequestBody.Content.Where(c => c.Key != "application/json-patch+json"))
                path.Value.RequestBody.Content.Remove(item.Key);
            var response = path.Value.RequestBody.Content.Single(c => c.Key == "application/json-patch+json");
            response.Value.Schema = new OpenApiSchema
            {
                Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" }
            };
        }
    }
}
}
````
