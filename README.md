# TeslaX
Farming bot for Growtopia. Work in progress.  
You can join the development/discussion Discord server at https://discord.gg/9Zv2ppD.

Preview:  
![Demo](demo.gif)  
(only movement was automated; punching was manual)

Recent version's interface:  
![Window](shot.PNG)

## About
TeslaX is designed to be flexible and extensible, so that in the future
any player in any world can tweak the tool to work for them,
and be sure that they won't be found out using it.
Whenever the progress isn't on new features, it's on broadening
the range of conditions in which the tool can work.

## Features
 - Walk forward and break blocks in front of the character, including non-solid foreground and background tiles.
 - Built-in support for many farmables, with an option to load custom spritesheets for other tiles.
 - Scripting system to support breaking multiple rows without manual intervention, whatever the farm layout is.
 - User-friendly texture injection system, for more stable detection.
 - Precise control of how input is determined based on distance and time passed.
 - Settings are saved automatically.

## Instructions
The following conditions must be met:
 - The game is zoomed out (each block is 32x32 pixels on-screen).
 - The back of your player's head is fully visible.
 - Your player is standing on a mostly-screen-wide row of Wooden Platforms.
 - There's a row of blocks ahead of you (at least one is enough).

If all of those are true, and you've set relevant settings, you should be ready to click Start and have your blocks broken for you.

## Troubleshooting
Q: I can't fully zoom out.  
A: Set display scaling value to 100%. This can usually be done by right-clicking on desktop and selecting "screen settings" (or whatever it's called in your localisation). On certain high-DPI displays blocks might be drawn smaller than 32x32; they should still be detected normally.

Q: None of those tips help!  
Contact me through the Discord server, I'll be glad to assist you.
