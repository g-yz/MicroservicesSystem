# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClienteAPI/ClienteAPI.csproj", "ClienteAPI/"]
COPY ["ClienteApp.Services/ClienteApp.Services.csproj", "ClienteApp.Services/"]
COPY ["ClienteApp.Data/ClienteApp.Data.csproj", "ClienteApp.Data/"]
COPY ["ClienteApp.Messaging/ClienteApp.Messaging.csproj", "ClienteApp.Messaging/"]
COPY ["SystemApp.Shared/SystemApp.Shared.csproj", "SystemApp.Shared/"]
RUN dotnet restore "./ClienteAPI/ClienteAPI.csproj"
COPY . .
WORKDIR "/src/ClienteAPI"
RUN dotnet build "./ClienteAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClienteAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClienteAPI.dll"]