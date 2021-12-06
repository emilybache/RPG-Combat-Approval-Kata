RPG Combat Kata
===============

This is a fun kata that has you building simple combat rules 
as for a role-playing game (RPG). The domain doesn't include a map or any other character 
skills apart from their ability to damage and heal one another.

The problem is broken down into a sequence of iterations to help you to focus on doing one 
thing at a time. There are also a couple of retrospectives where you are invited to reflect 
on what you've learnt so far.

In this repo there is a starting position with one failing test case that uses Approvals.
The idea is to design a Printer for a PlayerCharacter and design Approval tests for the
various scenarios as you build up the functionality.

## Iteration One

1. All Characters, when created, have:
    - Health, starting at 1000
    - Level, starting at 1
    - May be Alive or Dead, starting Alive (Alive may be a true/false)

1. Characters can Deal Damage to Characters.
    - Damage is subtracted from Health
    - When damage received exceeds current Health, Health becomes 0 and the character dies

1. A Character can Heal a Character.
    - Dead characters cannot be healed
    - Healing cannot raise health above 1000

## Iteration Two

1. A Character cannot Deal Damage to itself.

1. A Character can only Heal itself.

1. When dealing damage:
    - If the target is 5 or more Levels above the attacker, Damage is reduced by 50%
    - If the target is 5 or more Levels below the attacker, Damage is increased by 50%

## Iteration Three

1. Characters have an attack Max Range.

1. *Melee* fighters have a range of 2 meters.

1. *Ranged* fighters have a range of 20 meters.

1. Characters must be in range to deal damage to a target.

## Retrospective

- Are you keeping up with the requirements? Has any iteration been a big challenge?
- Do you feel good about your design? Is it scalable and easily adapted to new requirements?
- Is everything tested? Are you confident in your code?
- How readable are the test cases? Can you understand what each one is testing?

## Iteration Four

1. Characters may belong to one or more Factions.
    - Newly created Characters belong to no Faction.

1. A Character may Join or Leave one or more Factions.

1. Players belonging to the same Faction are considered Allies.

1. Allies cannot Deal Damage to one another.

1. Allies can Heal one another.

## Iteration Five

1. Characters can damage non-character *things* (props).
    - Anything that has Health may be a target
    - These things cannot be Healed and they do not Deal Damage
    - These things do not belong to Factions; they are neutral
    - When reduced to 0 Health, things are *Destroyed*
    - As an example, you may create a Tree with 2000 Health

## Retrospective

- What problems did you encounter?
- What have you learned? Any new technique or pattern?
- Share your design with others, and get feedback on different approaches.
- How has test maintenance been going? Have you spent a lot of time updating approved files?

## Acknowledgements

This Kata was invented by Daniel Ojeda Loisel and this description is adapted from [Steve Smith's version](https://github.com/ardalis/kata-catalog/blob/main/katas/RPG%20Combat.md)