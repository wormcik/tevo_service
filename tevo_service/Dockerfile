FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 👇 Go into the project folder and copy csproj
COPY tevo_service/*.csproj ./tevo_service/
WORKDIR /app/tevo_service

RUN dotnet restore

# 👇 Copy the rest of the project
COPY . ./  # copy everything from current repo root
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/tevo_service/out ./

EXPOSE 10000

ENTRYPOINT ["dotnet", "tevo_service.dll"]
