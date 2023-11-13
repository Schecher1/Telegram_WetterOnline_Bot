FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Telegram_WetterOnline_Bot.csproj", "."]
RUN dotnet restore "./Telegram_WetterOnline_Bot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Telegram_WetterOnline_Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telegram_WetterOnline_Bot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telegram_WetterOnline_Bot.dll"]