# Project Township Architecture Overview

> This document summarizes the high-level architecture and system outline for Project Township. All information is referenced from the [Game Design Document (GDD)](../GDD/Summary.md, ../GDD/MVPImplementation.md), the project README, and the current codebase/folder structure. Implementation notes are based on the actual code in the Scripts/Town and Scripts/DataRegistry directories as of May 7, 2025.

---

## Core Concept

Project Township is a 3D RPG-management hybrid (see GDD/Summary.md) focused on building, managing, and restoring a thriving town. Players balance personal questing and combat with the management of townspeople, resources, and infrastructure.

## Architecture Pillars

_Reference: GDD/MVPImplementation.md, "Architecture Pillars" and Scripts/Town_

1. **Modular Systems Layer**: Each core system (e.g., Town, Character, Combat, Quest, Exploration, Time) is implemented as a self-contained module. In practice, some systems (e.g., TownManager) are scene nodes, not singletons/autoloads.
2. **Data Layer**: Static data (e.g., classes, items, quests) is stored in `.tres`/`.res` files under `/Data`. Dynamic data (e.g., current town state, quest progress) is managed at runtime. DataRegistry is a runtime node that loads and registers templates.
3. **Scene/UI Layer**: Major scenes include Town, Character, Combat, Quest, and Exploration. UI is modular and signal-driven, but some UI/resource display logic is handled directly in system nodes (e.g., TownManager).
4. **State Management & Interconnectivity**: Global state is managed via a `GameSignalBus` singleton and scene nodes. Systems communicate through signals for loose coupling, but some integration is scene-based.

## Major Systems

_Reference: GDD/MVPImplementation.md, "Modular Systems Layer" and Scripts/Town_

- **Town System**: Implemented as a Node3D (TownManager) in the Town scene. Manages town simulation, building, resources, and citizen behavior. Handles resource display, building placement, and adventurer spawning.
- **Character System**: Handles player and NPC data, stats, and progression. (See Scripts/Characters)
- **Combat System**: Manages turn-based combat encounters. (See Scripts/Combat)
- **Quest System**: Handles quest assignment, progress, and resolution. (See Scripts/Quests)
- **Exploration System**: Manages world map, zones, and exploration events. (See Scripts/Exploration)
- **Time System**: Advances game time, triggers daily events, and manages day/night cycles. (See Scripts/Time)

## Town System Implementation Notes

_Reference: Scripts/Town/TownManager.cs, BuildSite.cs, BuildMenu.cs, BuildGrid.cs, GhostBuilding.cs, Building.cs_

- **TownManager** is a scene node, not a singleton. It tracks resources (Gold, Wood, Stone, Food), manages resource display, and listens for signals (building placed, quest completed).
- **Building Placement** is interactive and scene-driven:
  - **BuildSite** manages player interaction and triggers the build menu.
  - **BuildMenu** currently supports GuildHall selection.
  - **GhostBuilding** provides a visual preview and handles placement confirmation/cancellation.
  - **BuildGrid** manages tile states, placement validation, and emits signals when a building is placed.
- **DataRegistry** is a runtime node that loads building, race, class, and quest templates from disk at startup.
- **Resource Management** is handled directly in TownManager, using arrays of TownResource objects. Rewards from quests are distributed and displayed immediately in the UI.
- **Signals & Integration**: GameSignalBus is used for decoupled communication, but TownManager and BuildGrid are closely tied to the scene structure.

## Folder Structure (as of May 7, 2025)

_Reference: GDD/MVPImplementation.md, "Folder Structure (Proposed)", and actual project structure_

```
/Scenes
  /Buildings
    BuildSite.tscn
    GhostBuilding.tscn
    GridHelper.tscn
    GuildHall.tscn
  /UI
    BuildMenu.tscn
    CharacterInfoMenu.tscn
    NPCToAssignQuest.tscn
    QuestMenu.tscn
    QuestPanel.tscn
  Town.tscn
  WorldMap.tscn
/Scripts
  /Town
    Bed.cs
    BuildGrid.cs
    Building.cs
    BuildMenu.cs
    BuildSite.cs
    EnvGridMap.cs
    GhostBuilding.cs
    InteractNPC.cs
    TownExit.cs
    TownManager.cs
    TownNPC.cs
    TownResource.cs
  DataRegistry.cs
  ...
/Data
  /Buildings
    GuildHall.tres
  /Characters
    /Classes
      Adventurer.tres
      ...
    /Races
      ...
  /Quests
    GatherWoodQuest.tres
    ...
```

## Interconnected System Scenarios

_Reference: GDD/MVPImplementation.md, "Interconnected System Scenarios" and Scripts/Town_

- Quest time expires → QuestSystem, TimeSystem, TownManager, CharacterSystem
- Building completed → TownManager, CharacterSystem, QuestSystem
- Quest completed → QuestSystem, CharacterSystem, TownManager
- Time of day changes → TimeSystem, ExplorationSystem, CombatSystem

## Notable Deviations from GDD/Initial Plan

- Town system is a scene node, not a singleton/autoload.
- DataRegistry is a runtime node, not a static/global registry.
- Building placement is highly interactive and scene-based.
- UI/resource display is managed directly in TownManager.
- Only GuildHall is currently supported for building; adventurer spawning is hardcoded to this event.

---

## MVP Loop System Documentation Scaffold

<!--
Added: May 8, 2025
Summary: Initial lightweight documentation scaffold for MVP loop systems (Time, Town, Character, Quest, Exploration) per user request. See codebase and GDD for details. Generated by doc, 2025-05-08.
-->

### 1. Time System

- **Purpose:** Advances game time, triggers daily events, manages day/night cycle, and signals end-of-day for quest resolution.
- **Key Integration:** Sends signals to QuestSystem (for quest expiration/results), TownSystem (for resource ticks), and ExplorationSystem (for encounter updates).
- **Implementation:** See `Scripts/Time/Chronos.cs`, `Scripts/Time/Clock.cs`, `Scripts/Time/Helios.cs`.

### 2. Town System

- **Purpose:** Manages town simulation, building placement, resource tracking, and citizen/adventurer spawning.
- **Key Integration:** Listens for quest/building signals, updates UI, and interacts with CharacterSystem for adventurer management.
- **Implementation:** See `Scripts/Town/TownManager.cs`, `BuildGrid.cs`, `BuildMenu.cs`, `GhostBuilding.cs`, `Building.cs`.

### 3. Character System

- **Purpose:** Handles player/NPC data, stat generation, class/job assignment, and stat growth.
- **Key Integration:** Provides adventurer data to TownSystem and QuestSystem, manages player character.
- **Implementation:** See `Scripts/Characters/CharacterSystem.cs`, `CharacterData.cs`, `ClassData.cs`, `RaceData.cs`.

### 4. Quest System

- **Purpose:** Manages quest assignment, progress, completion, and reward distribution.
- **Key Integration:** Receives signals from TimeSystem (for quest timing), TownSystem (for quest completion), and CharacterSystem (for adventurer assignment).
- **Implementation:** See `Scripts/Quests/QuestManager.cs`, `Quest.cs`, `QuestGoal.cs`, `QuestReward.cs`, `QuestMenu.cs`.

### 5. Exploration System

- **Purpose:** Manages world map, zone selection, and triggers quest/adventure events outside of town.
- **Key Integration:** Works with QuestSystem for quest routing and TimeSystem for encounter timing.
- **Implementation:** See `Scripts/Exploration/WorldMapNodeSelect.cs`.

---

<!-- Next steps: Expand each section with class/method summaries, integration diagrams, and usage examples as implementation matures. -->
