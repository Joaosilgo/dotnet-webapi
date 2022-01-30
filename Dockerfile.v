# syntax=docker/dockerfile:1
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
#WORKDIR /app

# Copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore

# Copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o out

# Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:5.0
#WORKDIR /app
#EXPOSE 80
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "dotnet-webapi.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
##EXPOSE 8080
##ENV ASPNETCORE_URLS=http://*:$PORT




# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["dotnet-webapi.csproj", "./"]
RUN dotnet restore "dotnet-webapi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnet-webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dotnet-webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dotnet-webapi.dll"]
# ENTRYPOINT ["dotnet", "dotnet-webapi.dll"]
# Padrão de container ASP.NET

# Opção utilizada pelo Heroku
 CMD ASPNETCORE_URLS=http://*:$PORT dotnet dotnet-webapi.dll
