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

# Install necessary dependencies, including Google Chrome
RUN apt-get update && apt-get install -y \
    wget \
    gnupg \
    unzip \
    libglib2.0-0 \
    libnss3 \
    libgconf-2-4 \
    libfontconfig1 \
    libxss1 \
    libappindicator1 \
    libasound2 \
    libxtst6 \
    libxrender1 \
    libfreetype6 \
    libx11-xcb1 \
    libxcb1 \
    && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | gpg --dearmor > /usr/share/keyrings/google-chrome-archive-keyring.gpg \
    && echo 'deb [signed-by=/usr/share/keyrings/google-chrome-archive-keyring.gpg arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main' > /etc/apt/sources.list.d/google-chrome.list \
    && apt-get update && apt-get install -y google-chrome-stable \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Add runtime user
RUN useradd -m worker

# Add Permissions
RUN chown -R worker:worker /app

# Switch to user
USER worker



# Set up entry point with the Bash script
ENTRYPOINT ["dotnet", "Telegram_WetterOnline_Bot.dll"]
