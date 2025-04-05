# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the .csproj file and restore
COPY ./tevo_service/*.csproj ./ 
RUN dotnet restore

# Copy the rest of the project files
COPY ./tevo_service/. ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 10000
ENTRYPOINT ["dotnet", "tevo_service.dll"]
