# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeAPI.Seeder/EmployeeAPI.Seeder.csproj", "EmployeeAPI.Seeder/"]
COPY ["EmployeeAPI.Data/EmployeeAPI.Data.csproj", "EmployeeAPI.Data/"]
COPY ["EmployeeAPI.Common/EmployeeAPI.Common.csproj", "EmployeeAPI.Common/"]
COPY ["EmployeeAPI.Models/EmployeeAPI.Models.csproj", "EmployeeAPI.Models/"]
RUN dotnet restore "./EmployeeAPI.Seeder/EmployeeAPI.Seeder.csproj"
COPY . .
WORKDIR "/src/EmployeeAPI.Seeder"
RUN dotnet build "./EmployeeAPI.Seeder.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployeeAPI.Seeder.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeAPI.Seeder.dll"]