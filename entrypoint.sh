#!/bin/bash

# Wechsel zum Verzeichnis /app
cd /app

# Starte die dotnet-Anwendung in einer Screen-Sitzung im Hintergrund
screen -dmS dotnet-app dotnet Telegram_WetterOnline_Bot.dll

# Führe eine Endlosschleife aus (zum Halten des Containers)
while true; do
  sleep 5
done
