> **See also:** [Summary.md](Summary.md) for a high-level overview of the game pillars and core gameplay loop.

# Game Pillars

## Gameplay Loop

1. **Day Start:** Players review reports from ongoing quests, manage resources, and set new objectives for the day.
2. **In Town:** Interact with citizens to increase morale, assign tasks, and upgrade buildings. Manage work orders for artisans and allocate resources for research or construction.
3. **Adventure:** Players can personally undertake quests, exploring the wilderness, gathering resources, and engaging in turn-based combat.
4. **Day End:** Conclude the day’s activities, reflecting on achievements and planning for the next phase of growth.

# RPG Elements

## Unified Stat System

- **Endurance (END):** Vitality for physical labor and combat.
- **Strength (STR):** Determines physical attack power and work efficiency.
- **Defense (DEF):** Reduces damage taken and provides resilience.
- **Intelligence (INT):** Enhances magical damage and crafting aptitude.
- **Spirit (SPI):** Boosts healing, focus, and stamina for long tasks.
- **Magic Defense (MDF):** Provides resistance to magical attacks or accidents.
- **Speed (SPD):** Governs turn order and task efficiency.
- **Luck (LCK):** Influences critical hits and rare outcomes.
- **Accuracy (ACC):** Affects precision in attacks and crafting tasks.

### Character Template

**Properties:**

- **Name**: Character’s given name. (Randomly Generated based on species)
- **Species**: Chosen from available species (e.g., Gignen, Fae, Stoneheart, etc.).
- **Base Stats**: Randomly generated within species-specific ranges:
  - STR:
  - END:
  - DEF:
  - INT:
  - SPI:
  - MDF:
  - SPD:
  - LCK:
  - ACC:
- **Base Growth Rates**: Randomly generated percentages (not species-dependent).
- **Current Stats**: Base stats adjusted by level, equipment, and other factors.
- **Class Modifiers**: Reflect adjustments from classes, jobs, and effects.
- **Abilities**: Special skills or actions the character can use.
- **Affinities**: Special passive quirks characters are generated with that affect outcomes on targetable values such as stats, damage calculations, experience gained, etc.
- **Class**: (e.g., Warrior, Mage).
- **Job**: Non-adventuring profession (e.g., Farmer, Blacksmith).
- **Equipment**: Nested object containing:
  - Weapon
  - Armor
  - Offhand
  - Accessory 1
  - Accessory 2

### Stat Calculation System

```
Growth = (GrowthRate(Level) + Overflow) * (1 + Class Modifier)
Overflow = Growth % 1
```

[See the Character Creation tool for examples of stat growth](https://chase-skibeness.github.io/PT-character-generator/)

### Core Stat Formulas {#core-stat-formulas}

```
HP = 10 * END
MP = 12 * SPI
Basic Attack Damage = STR * (1 + Weapon Power / 100) * (User STR/Target DEF) * Crit
Basic Attack To Hit = Basic Attack Acc 90 + (ACC / 10)
Action Speed (TBA) = 5.5 - (SPD / 100)
Critical Rate = 5 * (1 + LCK)%
To Hit = Ability Accuracy + (User ACC / 10)%
```

## Character Customization Framework

### Species

Below are the available species for characters in the game. Each species is defined by thematic traits and specific bonuses. **Base stats** are expressed as ranges to enable randomized character generation. Growth rates are not species-specific and are randomly generated separately.

**Base Stat Generation**

- Each species has predefined ranges for base stats, reflecting their thematic strengths and weaknesses.  
  - For example, Stonehearts excel in **Endurance** and **Defense**, while Demarkin favor **Intelligence** and **Magic Defense**.  
- Upon character creation, base stats are randomly generated within these ranges.

Gignen

- **Description:** Versatile and adaptive, Gignen are generalists suited to any role.  
- **Base Stat Ranges:**  
  - STR: 8-12  
  - END: 8-12  
  - DEF: 8-12  
  - INT: 8-12  
  - SPI: 8-12  
  - MDF: 8-12  
  - SPD: 8-12  
  - LCK: 8-12  
  - ACC: 8-12  
- **Total Stats:** Minimum: 72, Maximum: 108  
- **Quirk:** Growth rates tend to be higher for Gignen.  
  ---

Fae

- **Description:** Graceful and intelligent, Fae are well rounded for magical positions.  
- **Base Stat Ranges:**  
  - STR: 6-10  
  - END: 6-10  
  - DEF: 6-10  
  - INT: 10-16  
  - SPI: 10-14  
  - MDF: 8-14  
  - SPD: 8-12  
  - LCK: 8-12  
  - ACC: 8-12  
- **Total Stats:** Minimum: 70, Maximum: 110  
  ---

Stoneheart

- **Description:** Stalwart and industrious, Stoneheart are natural craftsmen and warriors.  
- **Base Stat Ranges:**  
  - STR: 8-14  
  - END: 10-16  
  - DEF: 10-14  
  - INT: 6-10  
  - SPI: 8-12  
  - MDF: 6-10  
  - SPD: 6-10  
  - LCK: 8-12  
  - ACC: 8-12  
- **Total Stats:** Minimum: 70, Maximum: 110  
  ---

Beastkin

- **Description:** Agile and primal, Beastkin possess keen senses and excel in physical prowess.  
- **Base Stat Ranges:**  
  - STR: 10-16  
  - END: 8-12  
  - DEF: 6-10  
  - INT: 6-10  
  - SPI: 6-12  
  - MDF: 6-10  
  - SPD: 10-16  
  - LCK: 6-10  
  - ACC: 10-14  
- **Total Stats:** Minimum: 68, Maximum: 110  
  ---

Kithan

- **Description:** Nimble and lucky, Kithan excel in evasion and luck-based roles.  
- **Base Stat Ranges:**  
  - STR: 6-14  
  - END: 6-10  
  - DEF: 6-10  
  - INT: 8-12  
  - SPI: 8-12  
  - MDF: 6-10  
  - SPD: 10-16  
  - LCK: 10-14  
  - ACC: 10-14  
- **Total Stats:** Minimum: 70, Maximum: 110  
  ---

Angarkin

- **Description:** Agile and celestial, Angarkin possess keen senses and excel in physical prowess.  
- **Base Stat Ranges:**  
  - STR: 8-16  
  - END: 8-12  
  - DEF: 6-12  
  - INT: 6-10  
  - SPI: 8-12  
  - MDF: 6-10  
  - SPD: 8-12  
  - LCK: 8-10  
  - ACC: 10-16  
- **Total Stats:** Minimum: 68, Maximum: 110  
  ---

Demarkin

- **Description:** Inventive and clever devils, Demarkin excel in crafting and magical support.  
- **Base Stat Ranges:**  
  - STR: 4-10  
  - END: 6-10  
  - DEF: 6-10  
  - INT: 12-16  
  - SPI: 6-12  
  - MDF: 10-16  
  - SPD: 8-12  
  - LCK: 6-10  
  - ACC: 8-14  
- **Total Stats:** Minimum: 66, Maximum: 110  
  ---

Reptilekin

- **Description:** Calculated and resilient, Reptilekin balance endurance and defense while maintaining sharp focus in battle.  
- **Base Stat Ranges:**  
  - STR: 8-12  
  - END: 6-10  
  - DEF: 10-14  
  - INT: 6-10  
  - SPI: 8-16  
  - MDF: 10-14  
  - SPD: 8-14  
  - LCK: 6-10  
  - ACC: 6-10  
- **Total Stats:** Minimum: 68, Maximum: 110

### Classes & Jobs

**Character Development:** 

Players influence citizens' growth by assigning them jobs or adventuring classes and occasionally providing rare stat-boosting items. While players cannot directly assign stats, their choices in roles and items shape the citizen’s potential.

#### Class-Based Growth Modifiers

Classes modify the growth rates of specific stats to reflect the adventurer’s role and specialization.

Modifiers adjust the base stat increases by percentages:

Tier 1

- **+25%:** Major boost to core stats.

Tier 2

- **+35%:** Major boost to core stat.  
- **+25%:** Minor boost to secondary stat.  
- **+10%:** Minor boost to ternary stat

Tier 3

- **+50%:** Major boost to core stats.  
- **+25%:** Minor boost to secondary stat.  
- **-30%:** Minor negative to unrelated stats due to specialization

**Jobs and Roles:**  
Citizens can perform essential non-adventuring roles such as Farmer, Blacksmith, Artisan, Merchant, or Scholar, each with unique contributions to the town.

Specialized roles can emerge as the player develops their settlement, such as Weapon smiths, Armor smiths, Alchemists, Master Artisans, or Botanists, unlocked through research and building upgrades.

- **Base Class:**  
  - **Adventurer:** Balanced stats, capable of using simple weapons and armor. All adventurers begin here.  
    - All Stats: +10%  
- **Tier 1 Classes (Available Level 5+):**  
  - **Warrior:** Specializes in physical combat.  
    - STR: +25%  
    - END: +25%  
  - **Magician:** Focuses on entry level magic.  
    - INT: +25%  
    - SPI: +25%  
  - **Scout:** Excels in speed and utility.  
    - SPD: +25%  
    - LCK: +25%  
- **Tier 2 Classes (Available Level 25+):**  
  - Warrior:   
    - **Knight:** Tanky defense and endurance.  
      - DEF: +35%  
      - END: +25%  
      - STR: +10%  
    - **Berserker:** High physical damage output  
      - STR: +35%  
      - SPD: +25%  
      - END: +10%  
  - Magician:   
    - **Elemental Mage:** Offensive elemental magic.  
      - INT: +35%  
      - SPD: +25%  
      - MDF: +10%  
    - **Light Mage:** Healing magic, buffs and status condition cures  
      - SPI: +35%  
      - MDF: +25%  
      - LCK: +10%  
    - **Dark Mage:** Offensive dark magic, de-buffs and status conditions  
      - INT: +35%  
      - ACC: +25%  
      - MDF: +10%  
  - Scout:   
    - **Rogue:** High damage but low accuracy abilities  
      - ACC: +35%  
      - LCK: +25%  
      - SPD: +10%  
    - **Explorer:** Excels in exploration and resource gathering  
      - SPD: +35%  
      - LCK: +25%  
      - END: +10%  
- **Tier 3 Classes (Available Level 50+):**   
  - Knight:  
    - **Sentinel**: Overpowering Defensive capabilities  
      - DEF: +50%  
      - END: +50%  
      - MDF: +25%  
      - SPD: -30%  
    - **Paladin**: Mixes defensive and healing capabilities. Gains abilities to restore health while tanking damage  
      - MDF: +50%  
      - SPI: +50%  
      - DEF: +25%  
      - STR: -30%  
    - **Dread Knight**: Mixes defensive and dark magic. Gains abilities to debuff enemies and inflict status conditions   
      - END: +50%  
      - MDF: +50%  
      - INT: +25%  
      - LCK: -30%  
  - Berserker:  
    - **Warlord**: Highest consistent physical damage output  
      - STR: +50%  
      - SPD: +50%  
      - LCK: +25%  
      - MDF: -30%  
    - **Battle Dancer**: Agile, critical-hit-focused melee fighter  
      - SPD: +50%  
      - LCK: +50%  
      - ACC: +25%  
      - DEF: -30%  
    - **Spellblade**: Combines physical prowess with magical damage  
      - STR: +50%  
      - INT: +50%  
      - SPD: +25%  
      - ACC: -30%  
  - Red Mage:  
    - **Sorcerer**: Mastery over elemental magic  and multi-target elemental attacks   
      - INT: +50%  
      - SPD: +50%  
      - ACC: +25%  
      - DEF: -30%  
    - **Spellblade**: Combines physical prowess with magical damage. Uses elemental attacks infused in melee strikes.   
      - STR: +50%  
      - INT: +50%  
      - SPD: +25%  
      - ACC: -30%  
    - **Sage**: Capable of both elemental and light schools of magic  
      - INT: +50%  
      - SPI: +50%  
      - ACC: 25%  
      - END: -30%  
  - White Mage:  
    - **Priest**: Combines healing, buffs, and powerful support magic  
      - SPI: +50%  
      - INT: +50%  
      - LCK: +25%  
      - STR: -30%  
    - **Paladin**: Mixes defensive and healing capabilities. Gains abilities to restore health while tanking damage  
      - MDF: +50%  
      - SPI: +50%  
      - DEF: +25%  
      - STR: -30%  
    - **Sage**: Capable of red & white schools of magic  
      - INT: +50%  
      - SPI: +50%  
      - ACC: +25%  
      - END: -30%  
  - Black Mage:  
    - **Warlock**: Specializes in dark magic with debuffs  
      - INT: +50%  
      - ACC: +50%  
      - MDF: +25%  
      - LCK: -30%  
    - **Dread Knight**: Mixes defensive and dark magic. Gains abilities to debuff enemies and inflict status conditions  
      - END: +50%  
      - MDF: +50%  
      - INT: +25%  
      - LCK: -30%  
    - **Shadowblade**: Uses dark magic to inflict debuffs on top of high damaging abilities  
      - INT: +50%  
      - ACC: +50%  
      - SPD: +25%  
      - DEF: -30%  
  - Rouge:  
    - **Assassin**: Precision-focused abilities, excelling in one-hit critical strikes.  
      - ACC: +50%  
      - LCK: +50%  
      - STR: +25%  
      - END: -30%  
    - **Battle Dancer**: Agile, critical-hit-focused melee fighter  
      - SPD: +50%  
      - LCK: +50%  
      - ACC: +25%  
      - DEF: -30%  
    - **Shadowblade**: Uses dark magic to inflict debuffs on top of high damaging abilities   
      - INT: +50%  
      - ACC: +50%  
      - SPD: +25%  
      - DEF: -30%  
  - Explorer:  
    - **Ranger**: Utilizes abilities to support combat and exploration.   
      - SPD: +50%  
      - END: +50%  
      - ACC: +25%  
      - LCK: -30%  
    - **Trailblazer**: Pinnacle of Exploration, Resource Gathering, and Wayfinding.  
      - SPD: +50%  
      - LCK: +50%  
      - SPI: +25%  
      - ACC: -30%

### Growth Rate System

Each stat is assigned a growth rate upon character creation, defining how much it increases per level. Growth rates are determined randomly using the following probabilities:

- **Minimal -- (5% Chance):**  
  **Stat Increase (per level) = {0 otherwise1 if Level mod 2 = 0**   
   *Stat increases by 1 every 2 levels.*

- **Steady - (20% Chance):**  
  **Stat Increase (per level) = {0 otherwise2 if Level mod 3 = 0**   
   *Stat increases by 2 every 3 levels.*

- **Normal _ (40% Chance):**  
  **Stat Increase (per level) = 1**  
   *Stat increases by 1 every level.*

- **Gradual (20% Chance):**  
  **Stat Increase (per level) = {1 otherwise2 if Level mod 3 = 0**   
   *Stat increases by 1 every level, with an additional +1 every 3 levels.*

- **Accelerated (10% Chance):**  
  **Stat Increase (per level) = {1 otherwise2 if Level mod 2 = 0**   
   *Stat increases by 1 every level, with an additional +1 every 2 levels.*

- **Exceptional (5% Chance):**  
  **Stat Increase (per level) = 2**  
   *Stat increases by 2 every level.*

- **Gignen Exception:**  
  - Gignen characters must always have **at least two stats assigned a “Accelerated” growth rate** to reflect their versatility. These stats are randomly selected during character creation, after growth rates have been determined and SPI only target growth rates that are lower than “Accelerated”

### Abilities {#abilities}

Abilities can be split into two categories:

- Active Abilities  
  - Active Abilities are actions in combat located in the Ability section of the action menu. They usually have some kind of activation cost, an attribute or multiple attributes, a crit chance, a base “to-hit” rate that is affected by the user’s ACC and an affecting formula that actively changes something during battle  
    Ex: Spark  
    - Cost: 50 mp  
    - Ability Power: 40  
    - Accuracy: 85  
    - Affecting Formula - Damages Target:  
      - User INT * (1 + Ability Power/100) * (User INT / Enemy MDF) * Attribute Interaction * Crit  
- Passive Abilities  
  - Passive Abilities are always active modifications to other modifiable aspects of a given character or monster  
  - Ex: Flame Swordsman  
    - Affected aspect: Basic Attack  
      - User INT / 10 * Attribute Interaction * Crit fire damage added to all Basic Attacks  
  - Ex: Blessing of Light  
    - Affected aspect: Light Attribute Abilities  
      - User SPI / 10 added to all Light Attribute Abilities

Adventurer Abilities:

Balancing and Scoring System: To ensure abilities are balanced and intuitive, we use a scoring system based on several criteria. Each ability is scored as follows:

- **Cost:** High (-1), Medium (0), Low (+1)  
- **Damage:** High (+1), Medium (0), Low (-1)  
- **Accuracy:** High (+1), Medium (0), Low (-1)  
- **Targeting:** Single Target (0), Multiple Target (+1)  
- **Status Effects:** Inflicting a status effect (+1)

Abilities should generally balance out with a score between -2 and +2, with higher or lower scores reserved for specialized or situational abilities. Attribute tropes also guide the design of abilities, with each element having distinct characteristics:

- **Fire:** High cost, high damage, single target, medium accuracy.

- **Water:** Low cost, medium damage, single target, medium accuracy.

- **Wind:** Medium cost, high damage, single target, low accuracy.

- **Earth:** High cost, low damage, multiple target, high accuracy.

Adventurer Abilities

- Power Strike:

  - Cost: 10 mp  
  - Type: Damaging  
  - Attribute: Physical  
  - Ability Power: 25  
  - Accuracy: 85  
  - Formula: User STR * (1+Ability Power/100) * (User STR/Target DEF) * Crit  
- Spark:

  - Cost: 15 mp  
  - Type: Damaging  
  - Attribute: Fire  
  - Ability Power: 25  
  - Accuracy: 85  
  - Formula: User INT * (1+Ability Power/100) * (User INT/Target MDF) * Attribute Interaction * Crit  
- Focus:

  - Cost: 0 mp  
  - Type: Restorative  
  - Attribute: Light  
  - Formula: User Mp += User SPI  
- Bubble:

  - Cost: 15 mp  
  - Type: Damaging  
  - Attribute: Water  
  - Ability Power: 25  
  - Accuracy: 85  
  - Formula: User INT * (1+Ability Power/100) * (User INT/Target MDF) * Attribute Interaction * Crit  
- Brace:

  - Cost: 5mp  
  - Type: Buff  
  - Attribute: Physical  
  - Formula: User DEF += 0.3 * User DEF

Warrior Abilities

- Rally Cry  
  - Cost: 15 mp  
  - Type: Buff  
  - Attribute: Phsyical  
  - Formula: User STR += 0.3 * User STR  
- Cleave  
  - Cost: 15 mp  
  - Type: Damaging  
  - Attribute: Physical  
  - Accuracy: 80  
  - Formula: User STR * 0.6 to each enemy  
- Shield Bash  
  - Cost: 15 mp  
  - Type: Damaging  
  - Attribute: Physical  
  - Ability Power: 35  
  - Accuracy: 80  
  - Formula: User DEF * (1 + Ability Power / 100) * (User STR/ Enemy DEF) * Crit  
- Reckless Charge  
  - Cost: 25 mp  
  - Type: Damaging  
  - Attribute: Physical  
  - Ability Power: 50  
  - Accuracy: 70  
  - Formula: User STR * (1 + Ability Power /100) * (User STR/Enemy DEF) * Crit  
- Grit  
  - Cost: 50 mp  
  - Type: Restorative  
  - Formula: User HP += User END

Magician Abilities

- Air Pressure  
  - Cost: 25 mp  
  - Type: Damaging  
  - Attribute: Wind  
  - Ability Power: 40  
  - Accuracy: 80  
  - Formula: User INT * (1 + Ability Power /100) * (User INT/Enemy DEF) * Attribute Interaction * Crit  
- Aftershock  
  - Cost: 40 mp  
  - Type: Damaging  
  - Attribute: Earth  
  - Ability Power: 30  
  - Accuracy: 90  
  - Formula: User INT * (1 + Ability Power /100) * (User INT/Enemy MDF) * Attribute Interaction * Crit to each enemy  
- Scorch  
  - Cost: 50 mp  
  - Type: Damaging  
  - Attribute: Fire  
  - Ability Power: 60  
  - Accuracy: 85  
  - Formula: User INT * (1 + Ability Power/100) * (User INT/Enemy MDF) * Attribute Interaction * Crit  
- Water Bomb  
  - Cost: 25 mp  
  - Type: Damaging  
  - Attribute: Water  
  - Ability Power: 35  
  - Accuracy: 90  
  - Formula: User INT * (1 + Ability Power/100) * (User INT/Enemy MDF) * Attribute Interaction * Crit  
- Barrier  
  - Cost: 25 mp  
  - Type: Buff  
  - Attribute: Light  
  - Formula: User MDF += User MDF * 0.3  
- Cure Minor Wounds  
  - Cost: 25 mp  
  - Type: Buff  
  - Attribute: Light  
  - Formula: Target HP += User SPI * (1 + Ability Power/100)  
- Inflict Minor Poison  
  - Cost: 30 mp  
  - Type: Status  
  - Attribute: Dark  
  - Accuracy: 55  
  - Formula: Target gains Posion (Minor) status condition  
- Drain Touch  
  - Cost: 30 mp  
  - Type: Damaging  
  - Attribute: Dark  
  - Ability Power: 25  
  - Accuracy: 70  
  - Formula:  
    - Target Damage = User INT * (1 + Ability Power/100) * (User INT/Target MDF) * Attribute Interaction * Crit  
    - User HP += Target Damage * 0.3

Scout Abilities

- Swift Strike  
  - Cost: 15 mp  
  - Type: Damaging, Buff  
  - Ability Power: 15  
  - Accuracy: 85  
  - Formula:  
    - User STR * (1 + Ability Power / 100) * (User STR / Target DEF) * Crit  
    - User TBA -= (SPD /100) * 1.5  
- Lock In  
  - Cost: 20 mp  
  - Type: Buff  
  - Formula: User LCK += User LCK * 0.3  
- Throw Sand  
  - Cost: 15 mp  
  - Type: Status  
  - Accuracy: 75  
  - Formula: Target ACC -= Target ACC * 0.3  
- Mark  
  - Cost: 20 mp  
  - Type: Status  
  - Formula: Next To Hit on Target += User ACC / 10  
- Identify  
  - Cost: 5 mp  
  - Type: Info  
  - Formula: Enemy Attribute, HP, Max HP, MP, Max MP, and resistances become known to the user

Note on Abiliti

### Attributes

#### Core Attributes

- **Fire**  
- **Wind**  
- **Water**  
- **Earth**  
- **Dark**  
- **Light**  
- **Attributeless (Physical Damage)**  
  - Creatures or attacks without elemental alignment fall into this category.  
  - Physical damage bypasses elemental interactions, making it versatile but without additional magical effects.

---

#### Elemental Relationships

- **Fire > Wind**: Fire consumes oxygen, dominating Wind.  
- **Wind > Earth**: Wind erodes and scatters Earth.  
- **Earth > Water**: Earth absorbs and controls Water.  
- **Water > Fire**: Water extinguishes Fire.  
- **Dark <> Light**:  
  - Dark is strong against Light but also weak to it.  
  - Light is strong against Dark but also weak to it.  
  - Both **resist themselves** (Dark resists Dark, Light resists Light).

---

#### Interaction Tiers

1. **Resistance**: Reduces damage from the same element (e.g., Fire resists Fire).  
2. **Weakness**: Increases damage taken from the countering element (e.g., Fire is weak to Water).  
3. **Immunity**: Nullifies all damage from a specific element.  
4. **Absorption**: Converts damage from an element into healing or buffs.

---

#### Special Mechanics

1. **Elemental Affinities**:  
   - Some monsters and rare citizens have an affinity for one element, granting resistances, weaknesses, and sometimes special effects.  
2. **Elemental Variants**:  
   - Attributeless monsters (e.g., Goblins, Wolves) may gain elemental variants influenced by their environments (e.g., Fire Goblin, Ice Wolf). These variants inherit elemental traits while maintaining their core identity.  
3. **Dynamic Combat**:  
   - Players must strategically exploit elemental weaknesses while protecting against resistances or immunities.

### Equipment and Gear

Equipment and gear is purchased or created in town, or found while Exploring. 

Equipment such as weapons, offhands and shields modify core stats and effect damage calculations or can augment other important values

Gear such as armor, consumable items, and accessories are other ways to modify targetable properties of a given character

### Affinities

Affinities are passive abilities that characters are usually generated with. They usually take the form of an increased ability of a certain attribute, weapon type, stat, job, or really any targetable value on a character sheet. 

Affinities come in different strengths:  
Minor - a small benefit to a specific value  
Major - a medium benefit to a specific value  
Prodigal - large benefit to a specific value with a small drawback to another value

## Combat

Goal: create a combat system that is both engaging enough for a player to actively participate in it but simple enough that many automated battles can occur without much resource load

### Party Composition

- **Players:** 1-5 adventurers per party.  
- **Enemies:** 1-5 enemies per encounter, scaling as the player unlocks additional party slots and grows stronger.  
- **Early Levels:** Start small—1-2 adventurers vs. 1-3 enemies, keeping battles manageable while teaching core mechanics.  
- **Scaling:** Gradually increase party size and complexity, introducing synergistic roles (e.g., tank, DPS, healer) as adventurers unlock advanced classes.

---

### Turn Order and Action Timing

- **Turn Order Mechanics:** [Determined by the **Speed (SPD)** stat.](#core-stat-formulas)  
  - Characters with higher SPD act more frequently during battle.  
- **TBA (Time Between Action):** Introduce a visual mechanic showing when characters will act next (like an ATB gauge or initiative bar). This is essentially a timer determining who goes next and in what order  
- **Future Balancing:** Consider diminishing returns on extremely high SPD to prevent it from dominating.

---

### Actions in Battle

1. **Attack:**  
   - [**Base Attack Formula**](#core-stat-formulas)  
   - Deals unattributed physical damage based on STR calculated if hit by ACC  
2. [**Ability**](#abilities)**:**  
   - **Stat Influence:** Abilities are influenced by specific stats, scaling appropriately (e.g., INT for magic abilities, STR for physical abilities).  
   - **Mana Costs:** Introduce a mana (MP) pool for limiting ability use, replenished by potions, resting, or end of battle.  
   - **Examples:**  
     - **Heal (SPI):** Restores HP to an ally.  
     - **Fireball (INT):** AoE fire magic targeting multiple enemies.  
     - **Piercing Strike (STR):** Ignores a percentage of the target's DEF.  
3. **Item:**  
   - **Consumables:** Use potions, elixirs, or other items directly from an adventurer's inventory.  
   - Examples:  
     - Health Potion: Restores HP.  
     - Antidote: Removes poison.  
     - Attack Buff Scroll: Temporarily increases STR.  
4. **Run:**  
   - **Escape Mechanic:** Attempt to flee battle and return to town.  
   - **Chance of Success:** Tied to SPD, LCK, or both.  
   - Failure to escape may result in the enemy getting a free attack turn.

---

### Victory Conditions

- **Primary Condition:** Defeat or route all enemies.  
  - **Rewards:** Victory unlocks loot as the primary way to acquire:  
    - Consumable items.  
    - Town resources (e.g., materials, gold, crafting ingredients).  
    - Gear and weapons.  
  - **Quest Completion:** Directly tied to battle outcomes (e.g., "Defeat X Goblins" or "Retrieve a Lost Item").

---

### Failure Conditions

- **Party Defeated:** All adventurers are incapacitated (HP reaches zero).  
- **Flee:** Choosing to run ends the quest prematurely, returning the party to town empty-handed (or with reduced rewards if partial success is allowed).

## Exploration Pillar: Locations & Room Structure

### Exploration of the World

The World map is arranged as various locations outside of town the players or hired adventurers will travel to in order to conduct their questing, gather resources, unlock new buildings or research opportunities, and generally progress the game forward.

### Location Overview

Locations are explorable map nodes outside of town, each representing a unique area (e.g., forest, ruins, cave) with its own theme, difficulty, and rewards. Each location is structured as a short, procedurally generated “mini-dungeon” typically consisting of ~5 rooms. The structure, content, and rewards of each location are defined by the following properties:

**Location Properties:**
- Name / Type / Biome (unified or separated as needed)
- Difficulty Level (controls encounter scaling and progression)
- Room Pool (list of possible room types/events for this location)
- Bestiary (monsters/monster groups, with difficulty tiers)
- Treasure Pool (possible rewards, with rarity/tiers)
- Resource Pool (gatherable resources)
- Exploration Progress (tracks player’s exploration, affects encounter/resource/treasure rates)
- Connected Locations (map node links)
- Skeleton/Structure (sequence and type of rooms, e.g., boss at end, minibosses, resource-heavy, etc.)

**Location Regeneration:**
- Locations regenerate after being cleared, but the first clear is the most rewarding.

**Travel:**
- Accessed via a 2D world map. Travel time is based on distance from Town; some locations require special preparation (e.g., camping gear).

### Room Definition

Each room is an encounter or event, pulled from the location’s room pool. Room types include:
- Monster Encounter (with difficulty tiers and monster groups)
- Treasure Room (with rarity/tiers)
- Resource Node (gatherable resources)
- Narrative/Quest Event (predefined for story beats)
- Special/Unique Room (miniboss, puzzle, etc.)

**Room Pool:**
- Manually defined per location, with possible tagging for efficiency.

**Structure Rules:**
- Most locations end with a boss encounter and a unique reward.
- Some locations may have minibosses, unique enemies, or be resource-heavy.
- Each location has a “skeleton” outlining the sequence and type of rooms.

**Player Goals:**
- Vary by context: quest completion, dungeon clear, resource gathering, leveling, etc.

On Resource Node rooms:
Something I'd really like to capture is the same feeling I got in Pokemon Diamond and Pearl when you dug for fossils in the underground. A tactile, layered, satisfying, and full of little dopamine hits mini game that takes about the length of a short combat encounter to finish would be perfect.

### Player vs. NPC Adventuring

Players can either embark on quests themselves or send NPC adventurers to complete them autonomously.

### Quest Types 

#### Main Story Quests

#### Exploration Quests

#### Resource Gathering Quests

#### Item Retrieval Quests

#### Combat Missions

#### Training Quests

### Monsters 

Monsters pose a dynamic challenge through a stat scaling system that adapts to the player's progress.

#### Monster Stat Scaling Framework

Monsters use base stats combined with level-based scaling to dynamically adjust their challenge. This approach allows for consistent difficulty across various zones and introduces randomness to keep encounters fresh.

Stat Formula:

- **Base Stat:** The monster’s innate stat value at level 0, representing its core strength.  
- **Scaling Multiplier:** The per-level increment for the stat, adjusted by monster type.  
- **Randomization Modifier:** Each stat has a small variance (e.g., ) to introduce unpredictability.

Examples of Scaling Multipliers (per stat):

- **STR:** 1.0  
- **END:** 2.0  
- **DEF:** 1.5  
- **INT:** 0.5  
- **SPI:** 1.0  
- **MDF:** 1.5  
- **SPD:** 0.5  
- **LCK:** 0.3  
- **ACC:** 1.0

Example Formula Application:

- For a **Level 3** monster with **Base STR = 6** and a **Scaling Multiplier = 1.0**:  
  - STR 6 + (Level 3 * (Scaling Multiplier 1.0  +  Random (0.5-1.5)))  
  - STR = 9-15

Example Monster

### **Slime (Basic Enemy)**

#### **Behavior:**

- **Aggressive**  
  - Focuses on attacking the player with damaging abilities rather than defensive or supportive moves.

#### **Base Stats (Level 1):**

- **STR:** 6  
- **END:** 12  
- **DEF:** 10  
- **INT:** 2  
- **SPI:** 8  
- **MDF:** 12  
- **SPD:** 4  
- **LCK:** 4  
- **ACC:** 10

#### **Scaling Multipliers (Per Level):**

- **STR:** +1  
- **END:** +2  
- **DEF:** +1.5  
- **INT:** +0.5  
- **SPI:** +1  
- **MDF:** +1.5  
- **SPD:** +0.5  
- **LCK:** +0.3  
- **ACC:** +1

#### **Attribute:**

- **Water**  
  - The Slime benefits from the **Water attribute**, giving it resistance to Fire and weaknesses to Earth.

#### **Abilities:**

1. **Spit Up**  
   - **Cost:** 25 MP  
   - **Ability Power:** 40  
   - **Attribute:** Water  
   - **Accuracy:** 85  
   - **Formula:** User END × (1 + Ability Power / 100) × (User END/Enemy MDF) × Attribute Interaction × Crit  
   - **Description:** A high-pressure water-based attack utilizing the Slime's high Endurance stat to deal significant damage.

#### **Weapon:**

- **Natural Weapon Power:** 30

### Encounter-Level Determination

Monster levels are determined by the location’s level range and difficulty class. Every location will have 

# Town Building

## Buildings and Structures

Key building types include housing, production facilities, and specialized structures.

- Key building types:  
  - Unlock Buildings
  - Population Buildings
  - Resource Generation Buildings
  - Item Buildings
  - Augmentation Buildings
  - Management Buildings
  - Renown Buildings
  - Buff Buildings

*Buildings can have multiple tags

### Unlock Buildings
Buildings that unlock something or progress the game in some way. These also include buildings that have research options that unlock something

Ex. Barracks, Merchant Stall, Magician's Tower, Tavern, etc. These are all buildings that unlock either a new class for your adventurers to promote into or unlock functionality like the ability to purchase items

### Population Buildings
Buildings that change the population stat in some way, either by increasing the current number of residents in your town or increasing the maximum number of residents you can have in your town.

Ex. Houses increase the current population of your town whereas Granaries, Waterworks, and other key buildings increase the maximum population cap in your town. 

### Resource Generation Buildings
Later in the game it may be good to provide the player with buildings that generate Materia and other Resources to supplement having to grind for them. Urum is included in this possible list of generatable resources.

Ex. Market might generate Urum, whereas building a "Forest Shrine" might generate Terratite

### Item Buildings
Buildings where items can either be purchased or created.

Ex. Merchant Stall, Market, Armorsmith, Weaponsmith, Forge, etc.

### Augmentation Buildings
These buildings influence other buildings that are around it in some way.

Ex. Building a Library next to housing will result in a passive increase in base INT stats and growth rates for citizens that live in those housing. Building a Pyre Shrine next to housing may increase the chances of a new adventurer having an Affinity for Fire

### Management Buildings
These buildings directly interact with mechanics that normally may not be interactable or manipulatable. 

Ex. The Quest board and Guild Hall are Management Buildings that interact with quest mechanics. Where as the Ability Obelisk or Fortune Teller can influence or interact with character stats

### Renown Buildings
Buildings that increase the town's renown level enabling upgrade and expansion.

Ex. The Guildhall, a Granary, honestly every building may contribute to Renown slightly

### Buff Buildings
Buildings Adventurers (and the player) can visit before leaving for their quest to receive some kind of temporary buff

Ex. Lumia condenser
## Building Mechanics:

All building within the town is done by the player utilizing Resources (called Materia) that is gathered via Adventurers excavating it from Materia nodes, or by gathering it from Monsters. 

- Tr - Terratite - Earth
- Aq - Aquatite - Water
- Ve - Ventite - Wind
- Ig - Ignitite - Fire
- Tn - Tenebria - Dark
- Lm - Lumia - Light

And the seventh Resource to rule them all: Money, called Urum in this kingdom. The money cost involved is the investment in supplying everything else needed in a building that was created with Materia as well as hiring the necessary staff to run whatever was built there. 

There is an 8th hidden resource here that the player needs to have enough population to run the buildings they've created. Certain buildings like the Mage Tower may have a set NPC come to town to inhabit it and operate it, but others like a Merchant stall or forge may require the player to assign one of their townsfolk to run the business (the other side of the stat system beyond combat)

Here's the player flow I'd like: 

1. Player walks up near a buildable area which then lights up a bit to indicate to the player that it is ready to be interacted with.   
2. The player hits the interact button (E key or A button) which brings up the build menu.   
3. The player can then see which buildings they have access to  
   1. the options that won't fit in the space they're interacting with are greyed out / not selectable.   
4. The player chooses the building they would like to build and a ghost version of it shows up on the buildable grid area (snapping to the grid)  
5. the player can then choose orientation of the building and if the building size is smaller than the buildable area then they can also move it around the buildable area to choose where on the area they would like it to go   
6. The player can confirm their selection of location and then the building appears in that area (later in game it'll take time to build, have animations etc.)

## Town Development and Progression

The town evolves visually and mechanically as players expand its borders and upgrade its buildings.

As the player and co. explore the various nodes on the map, defeat location bosses, collect unique items, and finish certain quests more buildings will unlock and become buildable furthering the development of their town in both available Adventuring options **and** Renown.

### Renown and External Influence

- Renown serves as a measure of the town’s prestige, unlocking higher-quality citizens, advanced buildings, and new challenges.  
- Renown will also be an important “resource in growing the borders of the town

### Building Tiers

### Citizen Interaction

- Players engage with citizens to increase morale.

- A romance system is under consideration.

# Narrative
We are leaning heavily on the Attributes as a frame for our world to come alive around. I think the very classic "very advanced civilization is now ancient ruins" setting is an appropriate usage here. The various locations the Player and their team of Adventurers will be going to will occasionally be remnants of this great civilization that was capable of using the Materia in a way that far surpasses what any living species is capable of today and now as the current day civilization builds outwards and explores it is a bit of an arms race to see who can find the most powerful weapon left behind

I guess I'm gonna define the cosmology now:
The world the living mortal species dwell in stand in the flux of the 4 elemental gods dominions allowing their elemental influence to mix and create the planet as we know it today. This is also why certain areas take on extreme influence from a particular element, because it may be a gathering place for that particular element (like a chunk of flour you find in your poorly made cake)
Beyond these four horizontal axis, lies the domains of Light and Dark (Selflessness and Selfishness, Commune vs Individual, Positive and Negative), these vertical axis swirl in a perfect duality contrasting the elemental discord. Monsters manifest in this world in multiple ways: perhaps they have slipped through the fabric of the dominions and found themselves on the Mortal dimension, or they may be beings that were born here but mutated by the entropic forces, some may even be manifestations of a given dominion itself

As a Tekton, you are someone who has the capability of utilizing the crystalized essence of these different dominions, what we call Materia, to create buildings and structures at a speed that would otherwise take years. As such, you are sent to this town to become it's new guildmaster and oversee its revitalization. 
## **Act Structure Overview**

### **Act 1: Reclaiming the Outpost**

**Narrative Focus:** Arrival and first steps in rebuilding the village **Gameplay Milestones:**

- Construct Guild Hall  
- Build Basic Support Structures (housing, workshop, farm, etc.)  
- Unlock Tier 1 Structures

**Contextual Events:**

- Player meets remaining villagers  
- Initial adventurers arrive in town  
- Rumors of unusual monster activity begin to emerge

---

### **Act 2: Establishing a Community**

**Narrative Focus:** Expansion and discovery **Gameplay Milestones:**

- Unlock and build Tier 2 Structures  
- Begin constructing Research Structures  
- Citizens and adventurers become more specialized

**Contextual Events:**

- Adventurer quests start to return strange reports from nearby ruins  
- Mentions of a dark force begin to surface more clearly  
- The town becomes a regional safe haven, attracting new citizens

---

### **Act 3: Becoming a Beacon**

**Narrative Focus:** Culmination of growth and foreshadowing future threats **Gameplay Milestones:**

- Unlock Tier 3 Structures  
- Remove previous research route blocks, allowing full tech tree access  
- Finalize core town infrastructure

**Contextual Events:**

- Narrative culminates with recognition of a larger threat looming  
- Town reaches peak renown and influence  
- Final quests hint at continuing challenges beyond the MVP scope

---

## **Implementation Notes:**

- Unlocks occur progressively within each act, not all at once  
- The narrative will mostly unfold through:  
  - Quest logs / summaries  
  - Town gossip / NPC chatter  
  - End-of-day reports and flavor text

# Parking Lot

1. How should stats impact non-adventuring jobs? Should unique job-specific stats be introduced?  
2. What is the optimal balance between player-driven quests and autonomous NPC tasks?  
3. What detailed mechanics should define endgame and multiplayer systems?  
4. How can the narrative and world-building be further developed to support the core gameplay loop and enhance player immersion?  
5. What art style and tone will best capture the game's themes and appeal to the target audience?  
6. What are the key milestones and deliverables for achieving the MVP within the given timeframe and budget?  
7. Could AI be leveraged to create a more immersive experience interacting with characters that already have a high degree of variability to begin with?  
8.