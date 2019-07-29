# TeslaX
Farming bot for Growtopia. Work in progress.  
You can join the development/discussion Discord server at https://discord.gg/9Zv2ppD.
Download latest version in `Releases` tab.

Recent version's interface:  
![Window](shot.PNG)

## Features
 - Walk forward and break blocks in front of the character, including non-solid foreground and background tiles.
 - Precise control of how input is determined based on distance and time passed.
 - Scripting system to support breaking multiple rows without manual intervention, whatever the farm layout is.
 - Discord Rich Presence, if that appeals to you for some reason.
 - Settings are saved automatically.

## Instructions
The following conditions must be met:
 - Your character is standing in front of a row of tiles.
 - Tile ID is selected in `Block: tile` menu.

If all of those are true, and you've set relevant settings, you should be ready to click Start and have your blocks broken for you.

## Notes
This project has recently been updated from graphical detection to directly working with the game's memory. For previous versions, check `graphics-based` branch.