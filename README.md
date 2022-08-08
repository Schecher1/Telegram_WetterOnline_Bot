# Telegram-WetterOnline-Bot

## Important!
The bot text you in english and was "programmed in english", but the WetterOnline API runs only over DEU and ITA, that means you don't get weather data in english! So do not be surprised if the bot sends you German weather data, so the image then contains German words!

## Application description:

This bot runs over the "unofficial" API of WetterOnline, a german weather data service. For this purpose the converter ConvertApi is used to convert html code to png.

## Required

Make sure that it is a **RUNTIME** of **.NET6**!

### Linux:
Before the bot can run it has to have the DotNet Runtime version 6 installed. It has to be done as explained [here](https://docs.microsoft.com/en-us/dotnet/core/install/linux) in the Microsoft documentation.

A direct link for:
[CentOS](https://docs.microsoft.com/en-us/dotnet/core/install/linux-centos) 
[Debian](https://docs.microsoft.com/en-us/dotnet/core/install/linux-debian) 
[Fedora](https://docs.microsoft.com/en-us/dotnet/core/install/linux-fedora) 
[OpenSUSE](https://docs.microsoft.com/en-us/dotnet/core/install/linux-opensuse) 
[Ubuntu](https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu) 
[SLES](https://docs.microsoft.com/en-us/dotnet/core/install/linux-sles) 

### Windows:
Before the bot can run it has to have the DotNet Runtime version 6 installed.

A direct link for:
[Windows x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.7-windows-x64-installer) 
[Windows x86](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.7-windows-x86-installer) 

## How to Download:

Go to the "Releases" and download any version.
Or <br/>
[press here to download for windows(.zip)](https://github.com/Schecher1/Telegram_WetterOnline_Bot/releases/download/Telegram-WetterOnline-Bot-Vers-1.5.0.4/Telegram_WetterOnline_Bot-WindowsX64.zip) <br/>
[press here to download for linux (.zip)](https://github.com/Schecher1/Telegram_WetterOnline_Bot/releases/download/Telegram-WetterOnline-Bot-Vers-1.5.0.4/Telegram_WetterOnline_Bot-LinuxX64.zip)<br/>
[press here to download for linux (.tar.xz)](https://github.com/Schecher1/Telegram_WetterOnline_Bot/releases/download/Telegram-WetterOnline-Bot-Vers-1.5.0.4/Telegram_WetterOnline_Bot-LinuxX64.tar.xz)<br/>
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

## 1.5.0.4
- The problem was found, why it sometimes worked and sometimes not (NULL Expection). It was my fault. This now works.

## 1.5.0.3
- Program is no longer standalone, that means .Net 6 must be installed on the Server, explained above in the "required" section. Reason: There were errors during the runtime that were not during debugging.
- The file is no longer so large because the .Net6 must now be provided by you.

## 1.5.0.2
- A NULL exception was found and fixed (wetteronline)
- Some missing Try Catch was added. (more will be added soon)

## 1.5.0.1
- A NULL exception was found and fixed (convertAPI)
- The log folder was named logs, reason: logic

## 1.5.0.0
- The Console now has a UI with the following functions:
    - Start and Stop the Telegram Bot
    - Show the current confs
    - Reload the configs
    - Show status from the System
    - Show all Logs and with Filter Options
    - Quit the bot, but also with ctrl c

## 1.0.0.0
- The bot can run on Linux and Windows
- The bot keeps a log file with history
- The bot can give you locations suggestions
- The bot sends your weather data as a image
