# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SimpleIoT.Gateway.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet publish "SimpleIoT.Gateway.csproj" -c Release -o /app/publish

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8003

ENTRYPOINT ["dotnet", "SimpleIoT.Gateway.dll"]
