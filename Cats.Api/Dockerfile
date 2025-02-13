# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the project file(s) and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the remaining files and build the project
COPY . .
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Expose port 80 (the port your app listens on inside the container)
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "Cats.Api.dll"]
