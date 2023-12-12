# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Telegram_WetterOnline_Bot.csproj", "."]
RUN dotnet restore "./Telegram_WetterOnline_Bot.csproj"
COPY . .
RUN dotnet publish "Telegram_WetterOnline_Bot.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Install screen
RUN apt-get update && apt-get install -y screen

# Copy the entry point script
COPY entrypoint.sh /app/

# Make the entry point script executable
RUN chmod +x /app/entrypoint.sh

# Set up entry point with the Bash script
CMD ["/app/entrypoint.sh"]