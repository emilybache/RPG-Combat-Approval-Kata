import textwrap

import pytest
from approvaltests import verify

from rpg_combat import Character


def print_characters(characters):
    report = textwrap.dedent(f"""
    Characters:
    """)
    for character in characters:
        report += f"    {character}\n"
    return report

def test_basic_battle():
    report = ""

    hero = Character("Hero", factions=["Elf"])
    orc = Character("Orc1", factions=["White Hand"])
    characters = [
        hero,
        orc,
    ]
    report += print_characters(characters)

    damage = 100
    report += f"Orc Receives Damage From Hero {damage}"
    orc.receive_damage(hero, damage)

    report += print_characters(characters)

    verify(report)
