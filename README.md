# 🌮 DOTNET API

````bash
dotnet add dotnet-webapi  package Microsoft.EntityFrameworkCore.InMemory --version 5.0.6
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer


````

## In-memory database provider for Entity Framework Core (to be used for testing purposes)

## Postman

## DbInitializer

## Swagger

- [x] In-memory database provider for Entity Framework Core (to be used for testing purposes)
- [x] Setting up Swagger (.NET Core) using the Authorization headers (Bearer)
- [x] Autenticação e Autorização via Token (Bearer e JWT)
- [ ] Update the website
- [ ] Contact the media
  
## Docker

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
