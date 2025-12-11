How to play:
Press start and connect all of your controllers. Then you can press A and play!
To throw hold down the right trigger and to charge up angle with the left stick. To invert throwing controls simply press Y.
To move tilt right stick to walk and press A or left trigger to jump.
Be the last one standing!

Echofall is a chaotic 4 player FFA game where everyone is throwing stuff at eachother! Whether it be a lowly rock and the almighty fish, the weapon spawn is completely random.
This project had three requirements. It had to be physics based, have limited vision, and it also needed to have custom audio. 
The physics based led to the main idea of the game. throwing physics objects at eachother. We used rigid body 2d's as the projectiles to meet this requirement. It also made the game very fun!
The limited vision led to the main theming of the game. Because there was limited vision we decided to put it in a cave. This made the most sense and influenced two of the wepons. A rock and a boulder.
The custom audio was hard to make a focal point so I just had fun making all of the audio. I made it all myself except for the background music. I can't make very good music.

The physics based can be tested by playing the game. The code can be found in the 'rock' 'boulder' 'fish' and 'bomb' scenes. They all have custom scripts that inherit from a 'projectile' class.
The limited vision can be seen by playing the game and in the 'level 1' and 'level 2' scenes can be seen as the 'CanvasModulate'
The custom audio can be found on several nodes as 'AudioStreamPlayer's. They all have a custom sound attatched for fun.

Each OOP feature we want graded
The OOP feature of abstract class can be found in the 'projectile' script. That is the abstract class. It is implemented by the 'rock', 'bomb', 'fish', and 'boulder' scripts/scenes.
Private methods and variables can be found both in the player class and projectile class. This is because some scripts need to access some properties, while others are unnecessary.
I use is in both the projectile class and the fire class to check if what they collided with was a player.
In the player script I instantiate the new projectiles as projectile types so I know which variables I can and can't assign to it.
There is an enum used to define the rarity of the thrown objects. It can be found as the rarity enum.

Each Godot feature we want graded
The custom signal is used in both level one and level two to signal to the main script that a player has died. There is a signal bus node that connects the player to the main scene. It allows the scene to restart when all the players have died.
I use a packed scene to instantiate new nodes in the player script. This is how the new projectiles are spawned in.
I use the _Ready function in several scripts. I use it in the player script to set different references that are used later in the script. It is also used in the classes that inherit from projectile to set their different values like damage and stuff.
I use custom inputs and these can be used by going to the game settings and then to input map. There are button presses and trigger axis in there.
I change scenes between the main title to the controller connection to one of the two levels, and you can even go back again. There is lots of scene changing.

Ben was the main man on this. He got the levels, controller connection, menu, players with controller ID's, projectiles, audio, art, and much more.
Thang worked on the healthbar system and using a enum. These were very valuable tasks.
Shaurya was in charge of making the sample player and the HUD. These were also very important tasks, and the player was extra important early on.

No external google drive files necessary
