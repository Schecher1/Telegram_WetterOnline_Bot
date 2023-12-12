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

# Set up entry point with the Bash script
ENTRYPOINT ["dotnet", "Telegram_WetterOnline_Bot.dll"]