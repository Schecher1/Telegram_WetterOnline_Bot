# Telegram-WetterOnline-Bot

## Important!
The bot text you in english and was "programmed in english", but the WetterOnline API runs only over DEU and ITA, that means you don't get weather data in english! So do not be surprised if the bot sends you German weather data, so the image then contains German words!

## Application description:
This bot runs over the "unofficial" API of WetterOnline, a german weather data service.  
So, the German weather service "WetterOnline" has a free widget function and I misused it, for this bot.   

The widgets are fetched, screenshotted by a headless browser and sent.  

## Required
Make sure that it is a **RUNTIME** of **.NET6**!

### Docker:
This bot is now only available for docker.  
There are simple reasons, the bot does not need much and can be easily docked by anyone.  


## Features:
✔️ Selfhost<br/>
✔️ Logs with history<br/>
✔️ Location suggestions<br/>
✔️ Config File<br/>
✔️ Weather data as a pretty picture<br/>
✔️ Optimized for docker<br/>

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

1.) run this bot as a docker container 

2.) fill the generated config file with your data 

     2. 1.) get an API token for a Telegram bot (https://t.me/BotFather) just write to him.

     2. 2.) then write you username in it. (is not important, can be also left empty)

3. restart the bot and write to him, no matter what. It will tell you that he is not allowed to serve you and at the same time he´s send you your ID, which you then have to enter in the config file. If you want to enter more than one ID you have to separate it with a ','.

4. write the bot a place e.g. Berlin or Berli and he will suggest you places that you could mean, and more is then described in the message.

5. have fun!


# CHANGELOG

## 1.6.0.0
- The ConvertAPI was replaced with something better and therefore no longer used
- If there is just one suggest, then the bot will send you the weather image directly
- The Bot is now only for docker, because it is easier to use and maintain

## 1.5.0.5
- The next NULL error was found and fixed (probable there are still some, but will be fixed immediately).

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
