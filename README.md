*Right now I'm trying to fix the static hell I've created by turning it into a bunch of singletons. If you're a recruiter, sorry for the mess.*
# TeslaX
Farming bot for Growtopia. Work in progress.  
You can join the development/discussion Discord server at https://discord.gg/9Zv2ppD.

Preview:  
![Demo](demo.gif)  
(only movement was automated; punching was manual)

This bot detects the player and blocks in front of him, and automatically moves forward to simulate normal farming behavior.

## Instructions
How to use:
 1. Enter a world with at least one full row of Wooden Platforms.
 2. Stand on one of the rows.
 3. Take off any head items, so the back of your head is fully visible.
 4. Fully zoom out, so that each block on the screen is 32x32.  
    If that's not working, see troubleshooting tips below.
 5. Launch the program and set your skin color.  
    Colors are numbered from 0 to 13, in order as they appear in game settings.
 6. If you're going to break blocks, select a block from dropdown menu.  
    If there are no blocks to break, you can still see your character detected in real time.
 7. Check "Windowed" if the game is in windowed mode.
 8. Press Start.  
    If input isn't disabled, your character should move whenever selected block is detected, until you're enough to reach by hitting SPACE. If debug information is displayed (highly advised), you'll see exactly where the bot thinks you and the nearest block are, as well as other useful information.

## Troubleshooting
Q: I can't fully zoom out.  
A: Set display scaling value to 100%. This can usually be done by right-clicking on desktop and selecting "screen settings" (or whatever it's called in your localisation). On certain high-DPI displays blocks might be drawn smaller than 32x32; they should still be detected normally.

Q: None of those tips help!  
Contact me through the Discord server, I'll be glad to assist you.
