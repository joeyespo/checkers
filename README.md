Checkers
--------

Play Checkers with multiple levels of difficulty, with another player, or over
the net, which features a fully functional in-game chat. The game is fully
customizable and you can even select your own piece images.


Screenshots
-----------

![Checkers Screenshot 1](http://i.imgur.com/8FY7V.png)

The game.


![Checkers Screenshot 2](http://i.imgur.com/9k3qc.png)

It starts. Player against computer.


![Checkers Screenshot 3](http://i.imgur.com/ekLYk.png)

Player is winning!


![Checkers Screenshot 4](http://i.imgur.com/ymOqp.png)

The New Game dialog with custom pieces.


![Checkers Screenshot 5](http://i.imgur.com/6EFCR.png)

Setting up a network game on the New Game dialog.


![Checkers Screenshot 6](http://i.imgur.com/nrPl8.png)

Player vs computer with custom pieces!


View album on [imgur](http://imgur.com/a/Lbf3q).


Implementation Details
----------------------

The project's solution is partitioned into three Visual Studio (2008) solution
file projects.
  
  1. Checkers.Framework -- A fully functional and modular Checkers game API
  2. Checkers.UI -- A customizable user control based on Checkers.Framework
  3. Checkers -- The application EXE with all the extra bells and whistles

These modular assemblies can easily be dropped into other projects.
