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
-

### Other Info:
- Communicating between classes was done using an [event channel architecture](https://github.com/UnityTechnologies/open-project-1/wiki/Event-system) inspired by the open-source project Chop-Chop made by Unity.
- Assets used : to be added

