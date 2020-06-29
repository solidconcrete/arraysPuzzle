# arrayPuzzle
This application takes any array of numbers finds the shortest way to the end of the array itself, if possible.
The rules of the puzzle are such: every digit in array determines how many steps forward/backward can you move.
The goal is to find the most optimal way to the end of the array.

~~~~~~~~~LAUNCHING~~THE~~APP~~~~~~~~~~~
In order to run it without compiling, it's enough to just download the Release.rar from main project directory, unzip it,
go to Release/netcoreapp3.1/publish and run FutureProsHW.exe.

Tested only on Windows 10

~~~~~~~~~~~HOW~TO~USE~IT~~~~~~~~~~~~~
Enter an array of numbers like this [x, x, x, x, x, x]
where x is any number.
It is also possible to count several arrays at once by feeding the app with arrays like this [x, x, x, x, x][x, x, x, x, x] and so on.
If data is fed not like shown before, the application will not try to recognize the input and will wait for another input.
To show puzzle history, just type "history" and press ENTER. From the begginning the history will not be empty and will have some examplatory
puzzles given, and it will be updated with any new puzzle you feed the program with

~~~~~~~~~~~HOW~IT~WORKS~~~~~~~~~~~~~~
It recursively counts every possible winning combination and then shows what is the fastest way to solve this puzzle.
It also saves all arrays and their solutions so it won't be necessary to count again, if presented with the same array.


