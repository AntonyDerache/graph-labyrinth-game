# 🔎 Labyrinth game

Labyrinth is a shell game where you will spawn in a labyrinth and you will have to find the correct escape room. Your goal is to find this room in the minimal steps as possible.

## Features

- Level selection menu
- Know how much steps you made to reach the escape room
- Know the minimal possible steps to reach the escape room.

## Levels folder instructions

All the levels are stocked in a /levels folder. This folder must be in the same directory of the the executable.
If an error occured during the file parsing, an error message will be displayed on your terminal and you won't be able to find your level on the selection menu.

You can create your own level if you wish to. For that, create a new .txt file in the /levels folder and follow the **Levels files syntax** instructions to fill it up correctly.

### Visual Studio

If you use **visual studio** to use the program, please take a look that the levels folder is correctly set on /bin/Debug/netcoreapp3.1/ directory

### Other

If you use anything else to compile the files (like linux or csc compiler), please again, be carefull that the /levels folder is in same directory than the executable will be.

## Level file syntax

- The levels files must contains 2 lines for the begin and start point. Of course the verteces you set on those must be valid referenced verteces.
E.G:
#start:A
#end:J
- All connections should be made in this format: vertex:neighbor1,neighbor2.
- All neighbors should only have a comma (',') to separate them.
- No matter how much characters you put on a vertex or a neighbor, only the first character will be used to value the vertex. (E.G: TYDFTF:ER,RE => T:E,R)
- If any of these rules are not respected, your file may be rejected or you can find some missing connections in you labyrinth once in game.

Made by ❤️ by Antony Derache
