# Telegram-WetterOnline-Bot

## Important!
The bot text you in english and was "programmed in english", but the WetterOnline API runs only over DEU and ITA, that means you don't get weather data in english! So do not be surprised if the bot sends you German weather data, so the image then contains German words!

## Application description:

This bot runs over the "unofficial" API of WetterOnline, a german weather data service. For this purpose the converter ConvertApi is used to convert html code to png.


## How to Download:

Go to the "Releases" and download any version.
Or <br/>
[press here to download for windows](https://github.com/Schecher1/Telegram_WetterOnline_Bot/releases/download/Telegram-WetterOnline-Bot-Vers-1.5.0.0/Telegram_WetterOnline_Bot-WindowsX64.zip) <br/>
[press here to download for linux (zip)](https://github.com/Schecher1/Telegram_WetterOnline_Bot/releases/download/Telegram-WetterOnline-Bot-Vers-1.5.0.0/Telegram_WetterOnline_Bot-LinuxX64.zip)<br/>
[press here to download for linux (tar xz)](https://github.com/Schecher1/Telegram_WetterOnline_Bot/releases/download/Telegram-WetterOnline-Bot-Vers-1.5.0.0/Telegram_WetterOnline_Bot-LinuxX64.tar.xz)<br/>
to download if you want the latest one


## Features:

✔️ Selfhost<br/>
✔️ Logs with history<br/>
✔️ Location suggestions<br/>
✔️ Config File<br/>
✔️ Weather data as a pretty picture<br/>

## Image:
### Config-File:
![Config-File](IMAGES/Version%201.0.0.0/ConfigFile.PNG)

### Telegram Suggestion:
![Telegram-Suggestion](IMAGES/Version%201.0.0.0/TelegramSuggestion.png)

### Telegram Weather Picture:
![Telegram-Weather-Picture](IMAGES/Version%201.0.0.0/TelegramWeatherPicture.png)

### Console UI:
![Console-UI](IMAGES/Version%201.5.0.0/ConsoleUI_UI.PNG)

### Console All Logs:
![Console-All-Logs](IMAGES/Version%201.5.0.0/ConsoleUI_Log_All.PNG)

### Console Log Filter:
![Console-Log-Filter](IMAGES/Version%201.5.0.0/ConsoleUI_Log_Err.PNG)


## Process description:

1. download program

2. start program

3. fill the generated config file with your data 

     3. 1. get an API token for a Telegram bot (https://t.me/BotFather) just write to him.

     3. 2. get an API token for the converter (https://www.convertapi.com/), you need the "API Secret" and NOT the "API Key".

     3. 3. then write you username in it. (is not important, can be also left empty)

4. start the bot and write to him, no matter what. It will tell you that he is not allowed to serve you and at the same time he´s send you your ID, which you then have to enter in the config file. If you want to enter more than one ID you have to separate it with a decimal point.

5. write the bot a place e.g. Berlin or Berli and he will suggest you places that you could mean, and more is then described in the message.

6. have fun!


# CHANGELOG

## 1.5.0.1
-A NULL exception was found and fixed (convertAPI)
-The log folder was named logs, reason: logic

## 1.5.0.0
-The Console now has a UI with the following functions:
    -Start and Stop the Telegram Bot
    -Show the current confs
    -Reload the configs
    -Show status from the System
    -Show all Logs and with Filter Options
    -Quit the bot, but also with ctrl c

## 1.0.0.0
- The bot can run on Linux and Windows
- The bot keeps a log file with history
- The bot can give you locations suggestions
- The bot sends your weather data as a image