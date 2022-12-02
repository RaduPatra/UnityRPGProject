# Dungeon Adventure (Final Project)
A proof of concept third person Role-Playing Game inspired by Dark Souls, Legend of Zelda and other classic RPGs.
The goal of this project was to implement the most common features of a classic fantasy RPG. This was made for my final university project but it will remain a personal project that I will try to release on Steam in the future.

Gameplay Demo : to be added



### Implemented Features:

- 3rd person movement with sprinting, jumping, rolling and aiming
- Inventory & Equipment System
- Character Stats System (Current stats: Melee attack, Magic attack, Defence, Poise)
- Souls-Like Combat System - Light/Heavy Attacks, Health, Stamina, Stagger Bar
- Component-based item system (attributes & categories) with ScriptableObjects. Implemented custom editor tools for designers (aka me), that ease the process of creating an item. Current important types of items : Consumables (speed, health, attack boost potions), Armor, Shields, Melee items, Chargeable wands, Keys, quest items

- Enemy AI using a state machine that can execute various state actions such as: Idle, Patrol, Search Target, Chase, Strafe, Combat Decisions, Attack, Death

- Interactables: 
	- Reward chests inspired by BOTW
	- Bonfire checkpoints inspired from Dark Souls
	- Unlockable doors
	- Ground items
	- Quest NPCs

- Save / Load System

- Dialogue system with dialogue trees that can get modified depending on the state of the game

- Mission system with objectives such as talking to a character, reaching certain locations, finding items, killing monsters

Currently there is only a main and secondary mission that try to showcase all the implemented objectives:
- The Lost Child - the player has to find and save a child captured by monsters and bring him back home to his dad
- The Ancient Skull - an archeologist asks the player to retrieve a skull item from a guarded location.


### To Be Added:
- More camera improvements such as screen shake, zooming while aiming
- Cutscenes
-

### To Remake/Refactor:
to be added

### Bugs:
- The selected quest visual in the journal doesn't always corresepond to the active step list when starting a new quest or completing a new step. This has something to do with how Unity handles toggle groups for disabled object. I plan to remake/change the behavior of the quest log anyway.
- If you decide to enter the cave and kill the monsters before starting the first quest you can get stuck if you go and start the quest later. This can be easily fixed by blocking the entrance until the quest is started (or by just removing the kill monsters step) but that makes the quests too linear. I plan to remake the quest system to support non-linear completion of some steps (similar to Witcher 3) while still having the journal show the quest related actions done by the player.
- In very rare ocasions, combo melee hits are not getting registed. This is because animation events can get called in the middle of transitions as well. So sometimes the DisableCollider animation event from the previous animation can get called right after EnableCollider from the new animation. We can get animation info related to an event so a possible fix would be to disable the collider only if the animation weight is < 1(not in the middle of a transition).

### Other Info:
- Communicating between classes was done using an [event channel architecture](https://github.com/UnityTechnologies/open-project-1/wiki/Event-system) inspired by the open-source project Chop-Chop made by Unity.

### Assets used:
- Odin inspector
- The map and 3d models are all from Synty.
- Animations from Kubold and Kevin Iglesias
- For UI graphics I used RPG MMO UI 5.
- many more small ones


