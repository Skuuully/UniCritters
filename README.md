# UniCritters
Critter AI made for ProgII Assignment 

Note: this is not the full environment for the critters to run, requires critterworld provided by Dave Voorhis. This creates a dll to use for in that scenario.

Four critters which all behanve differently:
- All rounded critter which heads for the exit whilst trying to kill weaker critters and run away from stronger ones.
- A very bad attempt at pathfinding, heads straight unless can see exit, if not adds a small angle to itself to follow walls.
- One that should bait others, would act similarly to the first critter however if nearby critter that seems to be chasing it, will start to circle hoping that they follow.
- An attempt at A*, flawed by not being able to test for a wall betwenn x,y and hypothetical x,y however should do a decent job of finding the exit.
