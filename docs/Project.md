# Project Township: Project Management Log

## Current Goals & Milestones
- [ ] Scaffold Exploration pillar: location definition, travel system, encounter system, and exploration quest type (pm, prod)

## Active Tasks
- [ ] Define "location" data structure and properties (pm, dev, prod)
  - Assigned to: prod (design requirements), boss (technical requirements)
  - Progress: Design definition for locations and room structure completed and documented in GDD/GamePillarDetails.md (2025-05-08)
  - Progress: RoomType classes scaffolded as [GlobalClass]es, AssociatedLocation property removed to prevent circular references (2025-05-09)
- [ ] Implement player travel system that consumes in-game time (pm, dev, prod)
- [ ] Scaffold encounter system that triggers events based on location (pm, dev, prod)
- [ ] Create quest type for exploring locations (pm, dev, prod)

## Completed Tasks
- [x] Bootstrap project management and daily standup workflow (pm) (2025-05-08)
- [x] Break down current features into nucular tasks (pm) (2025-05-08)
- [x] Location resource registration in DataRegistry (pm, dev, prod) (2025-05-10)

## [2025-05-08] GDD Markdown Readability Improvements
- Standardized all stat range dashes to a single hyphen-minus '-' for consistency and human readability in GDD/GamePillarDetails.md.
- Converted all formulas in the Stat Calculation System and Core Stat Formulas sections to fenced code blocks for improved markdown preview.
- These changes improve clarity and preview display for all team members and future contributors.

## [2025-05-09] Exploration Pillar Implementation Progress
- RoomType classes scaffolded as [GlobalClass]es in Scripts/Exploration/LocationRooms/.
- AssociatedLocation property removed from all RoomType classes and LocationRoomSkeleton to prevent circular reference errors in Godot.
- Documentation and codebase are now aligned on this structure. See dev and doc standup for details.

## [2025-05-09] Standup Log
- Small win focus: Added location resource registration to DataRegistry for Exploration pillar progress. Task assigned to boss for implementation and verification in Godot. (pm)
- DataRegistry now loads all Location resources from Data/Exploration/Locations/ and exposes them via a Locations dictionary.
- Next: Boss to verify loading in Godot and report any issues or blockers.

## [2025-05-10] Standup Log

- Small win achieved: Location resource registration in DataRegistry now uses LocationConstants.LocationNames for explicit, reliable loading. Each location folder is expected to contain a main resource file named after the folder. Task completed by boss and verified in codebase. (pm)
- Next: Continue Exploration pillar implementation or select another quick win as time allows.

- Blocker identified: Further location and Exploration pillar design is blocked by lack of definition for the Resource system, building tiers/town impact, and Town Building pillar progression. (prod, boss)
- Next actionable task for tomorrow: Define the Resource system, building tiers, and their relationship to locations and town progression. This will enable meaningful location design and ensure the Exploration and Town Building pillars are tightly integrated.
- No further progress on Exploration pillar until this foundational system is defined.

## [2025-05-13] Standup Log
- Major unblocker: Locations, Town Resources, and their interaction design is now complete enough to proceed.
- Next actionable task: Implement first location resource and ensure DataRegistry loads and exposes it. (dev)
- Updated by pm on 2025-05-13.

- New session started. Awaiting boss direction for today's focus, priorities, or next actionable task. (pm)
- All roles are ready for check-in. See docs/StandupLog.md for session entry.

## Standup Ritual Expectations

The daily standup is a brief, structured check-in to align on project progress, priorities, and blockers. It ensures clarity, momentum, and accountability for each development session.

**Standup logs are now tracked in [docs/StandupLog.md](StandupLog.md).**

### Inputs
- Summary of what was accomplished in the previous session (from pm/dev/prod/doc as relevant)
- Current state of active tasks (from docs/Project.md)
- Any new blockers, questions, or design clarifications needed
- Updated priorities or focus areas from the prompter

### Ritual Steps
1. pm summarizes what was completed last session and current project status
2. pm/dev/prod/doc report on their respective areas if relevant
3. The prompter (boss) clarifies today’s focus, priorities, or any blockers
4. pm updates docs/Project.md with:
   - Standup log (date, summary, today’s focus, blockers)
   - Any changes to active/completed tasks
5. Confirm next actionable nucular tasks for the session

### Outputs
- Updated standup log in docs/Project.md
- Clear, actionable list of tasks for the session
- Identification of any blockers or design clarifications needed
- Alignment on next milestone or focus area

---
*Section added by pm on 2025-05-08: Defines expectations and flow for daily standup ritual to ensure consistency and clarity each session.*

---
*2025-05-08: prod and boss completed the design definition for locations and room structure in the Exploration pillar. See GDD/GamePillarDetails.md for details. Task updated to reflect progress. (pm)*

---
*2025-05-09: Session ended. Progress on Exploration pillar implementation and documentation alignment logged. (pm)*