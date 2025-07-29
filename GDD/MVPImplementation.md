# Project Township: High-Level Architecture (Godot)

---

## **📊 Overview**

Project Township is a 2D RPG-management hybrid being built in Godot, emphasizing immersive town building, character-driven progression, and strategic combat. This architecture document outlines the high-level implementation plan.

---

## **🛠️ Architecture Pillars**

### **1. Modular Systems Layer (Core Game Logic)**

Each core game system is implemented as a self-contained module, accessible via singleton (autoload) or as dynamic scene children. All systems are interconnected and event-driven where possible, with a strong emphasis on shared data, signals, and scalable interfaces.

- **Character System**: Handles character data, stat growth, class/job interactions, and level-ups.
- **Combat System**: Manages turn-based combat flow, turn order (TBA), ability resolution, and victory/failure conditions.
- **Town System**: Governs citizen task assignments, building construction, morale, shopping, progression, and resource production.
- **Quest System**: Manages quest availability, outcomes, adventurer dispatch, and loot generation.
- **Exploration System**: Supports map node management, adventuring zones, and enemy scaling.
- **Time System**: Advances game time, triggers daily events, resource ticks, and quest roll resolutions.

### **2. Data Layer (Static + Dynamic)**

- **Resources**: Use `.tres` and `.res` files to store:
  - Class/job templates
  - Race stat ranges
  - Equipment & item data
  - Abilities and attributes
  - Monster templates
  - Quest templates
  - Zone/Location templates
  - Building templates

- **Dynamic Data**: Runtime-only structures (classes/dictionaries) that represent the current state:
  - Character state
  - Town state (resources, structures, citizen list, expansion progress)
  - Quest state
  - Party rosters and exploration status

### **3. Scene/UI Layer**

- **Scenes**:
  - `TownScene` - hub for management
  - `CharacterScene` - roster management
  - `CombatScene` - active turn-based battles
  - `QuestScene` - map interface / dispatch
  - `ExplorationScene` - resolve resource-gathering

- **UI Composition**:
  - Modular Control nodes
  - Signals used to trigger system-level logic
  - Data-binding from systems → UI display

### **4. State Management & Interconnectivity**

- **GameState Singleton**:
  - Tracks global context: current day, renown, gold, major flags
  - Manages shared access to key game variables (time of day, weather, unlocked features)
  - Handles scene transitions and persistence between systems

- **Signal-Driven Communication**:
  - Systems emit and subscribe to shared signals for minimal direct coupling
  - Examples:
    - `on_day_changed` → QuestSystem checks for expirations, ExplorationSystem updates encounters
    - `on_quest_completed` → TownSystem unlocks buildings, CharacterSystem unlocks new classes
    - `on_building_completed` → CharacterSystem unlocks new job/class options, QuestSystem updates availability
    - `on_zone_explored` → ExplorationSystem triggers unlockables, TimeSystem shifts encounter tables

- **Shared Data Access Patterns**:
  - Systems read from global state and shared data registries (e.g., `GameState`, `DataRegistry`) rather than querying each other directly
  - Promotes independence and debugging clarity

---

## **📂 Folder Structure (Proposed)**

```
/Scenes
  /Town
  /Combat
  /Character
  /Quests
  /Exploration
  /UI
/Systems
  CombatSystem.cs
  TownSystem.cs
  QuestSystem.cs
  CharacterSystem.cs
  ExplorationSystem.cs
  TimeSystem.cs
/Data
  /Characters
  /Abilities
  /Items
  /Races
  /Jobs
  /Quests
  /Zones
  /Buildings
/Resources
  /Audio
  /Tilesets
  /Icons
/Scripts
  GameState.cs
  DataRegistry.cs
  Utils.cs
```

---

## **🏡 Town System Architecture**

### **Overview**

The TownSystem is a live simulation and management layer for the player's evolving hub. It coordinates economy, citizen behavior, building construction, expansion, and the interface through which quests and character progression are framed.

### **Core Responsibilities**

- **Resource Tracking**: Manages currency, crafting materials, food, morale, renown
- **Building & Construction**:
  - Place/upgrade buildings from templates
  - Buildings have effects (e.g., unlock gear, quests, classes, production bonuses)
  - Tracks build queue or timers
- **Quest Delegation**:
  - Handles assignment of adventurers to auto quests
  - Routes resource needs (like consumables or equipment) from shops
- **Expansion Progress**:
  - Unlocks new terrain space via renown or exploration
  - Triggers new building options or citizen spawns

### **NPC Behavior**

- **Scheduling**:
  - Citizens move through town based on job schedules and routines
  - Adventurers visit shops to gear up pre-quest (consumables, gear)
- **Movement & Simulation**:
  - Background systems simulate location, activity, and role status
  - Optional visual feedback (e.g., sprite walking to shop)
- **Dialogue System**:
  - Basic NPC interactions triggered on click
  - May reflect mood, job, or dynamic state (injured, excited, tired)

### **Shopping & Commerce**

- **Vendors**: Stock items based on production buildings or supply chain
- **Adventurer Gear Up Phase**: Before questing, characters purAxiom and equip items as needed
- **Shop Stock**: Updates based on resource flow and upgrades

### **UI Components**

- Resource bar (gold, food, wood, morale, renown)
- Building interface (available upgrades, current queue)
- Assignment panels (jobs, quest dispatch)
- Character hover/inspect panels
- Day-end report (events, updates, resource changes)

### **Signals**

- `on_building_started(building)`
- `on_building_completed(building)`
- `on_resource_changed(type, amount)`
- `on_shop_stock_updated()`
- `on_citizen_schedule_updated()`
- `on_npc_interacted(npc_id)`

### **Integration Points**

- **TimeSystem**: Triggers ticks for construction, schedules, and shop restocks
- **QuestSystem**: Routes adventurers to vendors pre-departure, handles assignment readiness
- **CharacterSystem**: Validates job/class unlocks via buildings
- **ExplorationSystem**: Unlocks new terrain for expansion

---

## **🚀 Interconnected System Scenarios**

| Trigger              | Systems Affected                     | Effect                                                                 |
|----------------------|--------------------------------------|-----------------------------------------------------------------------|
| Quest time expires   | QuestSystem, TimeSystem             | Marks quest failed, notifies TownSystem and CharacterSystem           |
| Time of day changes  | TimeSystem, ExplorationSystem, CombatSystem | Changes enemy pools, affects visuals and stat modifiers               |
| Building completed   | TownSystem, CharacterSystem, QuestSystem | Unlocks new classes, buildings, and quests                            |
| Zone explored        | ExplorationSystem, TownSystem, QuestSystem | Unlocks new zones, buildings, or quest chains                         |
| Quest completed      | QuestSystem, CharacterSystem, TownSystem | Rewards, unlocks, morale boosts, access to new content                |
| Combat initiated     | CombatSystem, TimeSystem, ZoneResource | Determines visuals and encounter modifiers based on time and zone      |
| Party dispatched     | QuestSystem, ExplorationSystem, TimeSystem | Ties quest duration to world time; initiates progress tracking        |

---

## **⚒️ MVP Definition**

### **Scope**

A vertical slice of gameplay that exercises all core pillars in a simple, guided loop. It serves as a foundation for scalable development and a testbed for data/system interconnectivity.

### **MVP Player Flow**

1. Control a **predefined player character** in a small, walkable town map.
2. Town has a **predefined set of resources** and only one buildable structure.
3. Player builds a **single type of building** (e.g., Guild Hall).
4. Building unlocks ability to **hire a generated adventurer**.
5. Player can open a menu to **view adventurer stats**.
6. Player interacts with the **town quest board** to view **two predefined quests**:
   - "NPC Quest" (assignable to hired adventurer).
   - "Player Quest" (undertaken by player character).
7. Player assigns the NPC Quest to the adventurer and accepts the Player Quest for themselves.
8. Adventurer immediately departs town to begin NPC Quest (simulated).
9. Player exits to **world map**, selects a location, and begins Player Quest.
10. Player fights a **single predefined enemy**.
11. Upon victory, player returns to town.
12. Player visits **quest board** again to mark Player Quest complete and receive XP/resources.
13. Player goes to **bed**, triggering end-of-day.
14. End-of-day screen shows result of NPC Quest.
15. Game rolls into Day 2, but all interaction is disabled (MVP end state).

### **⚒️ MVP Implementation Plan**

---

### **Phase 1: Core Infrastructure & World Setup**

1. **GameState Singleton**:
   - Stores player state, global variables, resources, current day.
2. **TimeSystem**:
   - Supports day ticking, sleeping, and end-of-day signal.
   - Triggers daily reset and quest results.
3. **Basic TownScene**:
   - Walkable environment with predefined interactive nodes:
     - Build Site.
     - Quest Board.
     - Bed.

---

### **Phase 2: Building & Adventurer Hiring**

4. **BuildingSystem**:
   - Single buildable structure: `Guild Hall`.
   - Simple click-to-build interaction.
   - Emits `on_building_completed`.
5. **CharacterSystem**:
   - Predefined player character.
   - On building complete: generate 1 adventurer with randomized stats.
6. **Adventurer Viewer**:
   - Menu to view adventurer stats after hiring.
   - Display only; no leveling yet.

---

### **Phase 3: Quest Assignment (Town Phase)**

7. **QuestSystem: Town Quest Menu**:
   - Interact with **Quest Board** to view two predefined quests:
     - **NPC Quest**: assignable to adventurer.
     - **Player Quest**: accepted by player.
8. **Quest Assignment Logic**:
   - Player assigns NPC Quest to adventurer (triggering simulated departure).
   - Player accepts Player Quest, added to GameState.

---

### **Phase 4: Player Quest Execution**

9. **World Map Menu**:
   - Player exits town and sees single location menu.
   - Select zone for Player Quest.
10. **CombatSystem**:
    - Hardcoded 1v1: player vs enemy.
    - Text resolution or stat vs stat.
    - On win, player returns to town.

---

### **Phase 5: Quest Completion & Sleep**

11. **Quest Turn-in**:
    - Interact with Quest Board to mark Player Quest complete.
    - Receive XP and reward (no leveling needed).
12. **Sleep System**:
    - Interact with bed to trigger sleep.
    - Triggers end-of-day screen.
13. **End of Day Report**:
    - Shows result of NPC Quest (success/fail, reward summary).
14. **Game End (Day 2)**:
    - Begin Day 2 with interaction disabled.

---

### **Systems Covered**

- Character System: player + adventurer creation, stat display.
- Town System: building construction, movement, quest board.
- Quest System: menu logic, quest assignment, quest resolution.
- Exploration System: zone selection, quest routing.
- Combat System: enemy encounter and resolution.
- Time System: day-end trigger and report delivery.

### **Design Goals**

- Demonstrate cohesion of all system layers.
- Validate architectural patterns.
- Produce content-independent framework.
- Establish player feedback loop.
- Show both player-controlled and autonomous quest resolution.
