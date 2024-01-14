# Telegram-WetterOnline-Bot

## Important!
The bot text you in German and was programmed in english, and the WetterOnline API runs only over DEU and ITA, that means you don't get weather data in english! So do not be surprised if the bot sends you German weather data, so the image then contains German words!

## Application description:
This bot runs over the "unofficial" API of WetterOnline, a german weather data service.  
So, the German weather service "WetterOnline" has a free widget function and I misused it, for this bot.   

The widgets are fetched, screenshotted by a headless browser and sent, using PuppeteerSharp.  

## Required
Make sure that your Server has the docker CLI

### Docker:
This bot is only available for docker.  
There are simple reasons, the bot does not need much and can be easily docked by anyone.  

## Features:
✔️ Selfhost<br/>
✔️ Logs with history<br/>
✔️ Location suggestions<br/>
✔️ With Docker Environment Variable<br/>
✔️ Weather data as a pretty picture<br/>
✔️ Optimized for docker<br/>
✔️ Set a Weather Timer (e.g. every day at 8 o'clock from berlin)<br/>

## Commands:
### ```/start```
The bot welcomes you and explains itself very shortly and if you are not yet on the whitelist, it sends you your chat ID.

### ```Berlin```
You just have to send your location as a message and he will send you the weather data, and if he couldn't find anything or more he will send you a selection

### ```/setTimer 14:00==Berlin```
Sends you the weather data for Berlin every day at 14:00 (12pm)

### ```/deleteTimer```
Shows you a list of all your timer events, which you can delete.

## Image:
### Welcome-Message:
![Telegram-Whitelist](IMAGES/Version%202.1.1.0/Welcome.png)

### Whitelist-Message:
![Telegram-Whitelist](IMAGES/Version%202.1.1.0/Whitelist.png)

### Suggestion-Message:
![Telegram-Suggestion](IMAGES/Version%202.1.1.0/TelegramSuggestion.png)

### Weather Picture:
![Telegram-Weather-Picture](IMAGES/Version%202.1.1.0/TelegramWeatherPicture.png)

### Logs:
![Console-Logs](IMAGES/Version%202.0.0.0/Console_DebugModeOff.png)

### Logs with Debug Mode:
![Console-Logs-WithDebug](IMAGES/Version%202.0.0.0/Console_DebugModeOn.png)


## Process description:
1.) Get an API token for a Telegram bot (https://t.me/BotFather) just write to him.

2.) Run this docker run command (with the API-Token):  
```
docker run -d \
  --name  telegramWetterOnlineBot_01 \
  -e TELEGRAM_API_TOKEN=0000000000:AAAAAAAAAAAAAAAAAAAaaaaaaaa-aaaaaaa \
  -e TELEGRAMBOT_OWNER_NAME=@Schecher_1 \
  -e TELEGRAM_ID_WHITELIST=000000000 \
  -e TZ=Europe/Berlin \
  -v telegramWetterOnlineBot_01_data =/app/data \
  --restart=always \
  schecher/telegramwetteronlinebot:latest
```

```use  '-e DEBUG=true' for debug mode```

3.) Write to the bot, no matter what. It will tell you that it is not allowed to serve you and at the same time send you your ID.

4.) Delete the container with the following command and run it again with the command (with the API-Token, Owner Name and your ID)
``` docker stop telegramWetterOnlineBot_01 && docker rm telegramWetterOnlineBot_01 ```

```(If you want to enter more than one ID you have to separate it with a ';')```

5.) write the bot a place e.g. Berlin or Berli and he will suggest you places that you could mean, and more is then described in the message.

6.) have fun!


# CHANGELOG

## 2.2.0.0
- The bot can now send you the weather data in events like every day at 8 o'clock
- The bot stores the user data into a json file (/app/data) you can mount it or you can use the docker run template above

## 2.1.1.0
- FIX: The bot waits until the image is rendered before sending it to you
- Quality and file size optimized

## 2.1.0.0
- The Container will accept the Environment Variable "TZ" (TimeZone) now
- The Container will now show the actual Time, not the UTC Time
- The Bot will now show the Suggestions in Buttons (Telegram Inline Keyboard)
- The Bot sends now a welcomes message, if you start the bot for the first time (/start command)

## 2.0.0.0
- The bot now runs with Docker
- The bot uses the Headless Browser PuppeteerSharp for the HTML to Image conversion
- The Container uses Environment Variables for the configs
- The bot will log to the console and to a file

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
