# Dungeon Adventure (Final Project)
A proof of concept third person Role-Playing Game inspired by Dark Souls, Legend of Zelda and other classic RPGs.
The goal of this project was to implement the most common features of a classic fantasy RPG. This was made for my final university project but it will remain a personal project that I will try to release on Steam in the future.

Download/Screenshots: https://soulzhd.itch.io/dungeon-adventure  
Gameplay Demo : https://www.youtube.com/watch?v=nRF46DjXM1w  
Trello : https://trello.com/b/vYOOrIYk/final-project-game
Bachelor Thesis (Romanian), includes documentation  : https://docs.google.com/document/d/19FW7lRtb4r-OE3Wjal5gFXiBBldQoU8unKMLcGJddA8/


### Implemented Features:

- 3rd person movement with sprinting, jumping, rolling and aiming
- Inventory & Equipment System
- Character Stats System (Current stats: Melee attack, Magic attack, Defence, Poise)
- Souls-Like Combat System - Light/Heavy Attacks, Health, Stamina, Stagger Bar
- Component-based item system that allows designers to create and customize item categories and attributes. Implemented custom editor tools, that ease the process of creating an item.
Current important types of items : Consumables (speed, health, attack boost potions), Armor, Shields, Melee items, wands that can charge and shoot projectiles, Keys, quest items
- Custom Editor Showcase video : To Be Added
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
- Character creation
- Shops & currency
- Two handed swords with animation rigging
- Sound effects & music
- Skills and leveling system

### Other Info:
- Communicating between classes was done using an [event channel architecture](https://github.com/UnityTechnologies/open-project-1/wiki/Event-system) inspired by the open-source project Chop-Chop made by Unity.

### Known Bugs:
- The selected quest visual in the journal doesn't always corresepond to the active step list when starting a new quest or completing a new step. This has something to do with how Unity handles toggle groups for disabled object. I plan to remake/change the behavior of the quest log, so for now its not a priority.
- If the player enters the cave and kills the monsters before starting the first quest, they may get stuck if they decide to start the quest later. Although blocking the entrance until the quest is started or removing the "kill monsters" step would solve the problem, it would make the quests more linear than they already are. I intend to rework the quest system to accommodate non-linear completion of certain steps (as in The Witcher 3), while also ensuring that the player's quest-related actions are reflected in the journal.
- In very rare ocasions, combo melee hits are not getting registed. This is because animation events can get called in the middle of transitions as well. So sometimes the DisableCollider animation event from the previous animation can get called right after EnableCollider from the new animation. It is possible to get the animation info related to an event such as the animation weight, so we could do some checks when we disable the collider based on that.



### Assets used:
- Odin inspector
- The map and 3d models are all from Synty.
- Animations from Kubold and Kevin Iglesias
- For UI graphics I used RPG MMO UI 5.
- many more small ones


