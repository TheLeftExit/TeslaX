# TeslaX
Farming bot for Growtopia.

This project does use the common approach of tampering with network packets, instead reading game data from RAM and simulating input based on that data. This makes it significantly harder to detect and easier to maintain.

For any questions contact me at theleftexit@protonmail.com.

#### Why are you no longer talking about changing pointer values?
Because it's going to become automated very soon. Stay tuned.  
In the meantime I've updated pointer values for 3.63 (courtesy of [**0xD3F**](https://github.com/DefaultO)).

## Overview
What this tool CAN do:
 - Simulate human-like keyboard input to move forward and punch, breaking lines of blocks.
 - Advance to the next row (once the feature has been properly configured).
 - Overall, help in a normal farming routine by automating the breaking process.

What this tool CANNOT do:
 - Place any blocks.
 - Break blocks that aren't placed in a straight line.
 - Hack the game, steal accounts, etc.  
(I've had issues from children trying to do that, so I have to mention it)

Recent version's interface (features may have changed):  
![Window](shot.PNG)

## How this works
 - Stand in front of a row of blocks.
 - Click "Detect" to make sure the tool is working. It should display ID and distance to the block you're facing.
 - Set "Block ID" to value from the status bar (either foreground or background).
 - Press Start to start breaking.
 - The tool will keep breaking until there are no more blocks or you manually cancel it.
 - You can configure the tool to break multiple rows automatically. For that you will need to specify "Door ID" and end-of-row script, and enable the feature.  
One of possible scenarios: walk forward for 1000 ms, punch (enter the door), wait for 1000 ms - will work with one-side-blocked Door pointing to a Path Marker.

## Notes
This project used to rely on graphical detection. For those versions, check `graphics-based` branch.  