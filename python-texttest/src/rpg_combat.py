#!/usr/bin/env python

import json


class DealDamage:
    def __init__(self, attacker_name, defender_name, amount):
        self.attacker_name = attacker_name
        self.defender_name = defender_name
        self.damagePoints = amount

    def play(self, characters):
        attacker = characters[self.attacker_name]
        defender = characters[self.defender_name]
        defender.receive_damage(attacker, self.damagePoints)

    def __str__(self):
        return f"DealDamage: {self.damagePoints} damage points from attacker {self.attacker_name} on defender {self.defender_name}"


class DealDamageOnMultipleCharacters:
    def __init__(self, attacker_name, defender_names, amount):
        self.attacker_name = attacker_name
        self.defender_names = defender_names
        self.damagePoints = amount

    def play(self, characters):
        attacker = characters[self.attacker_name]
        defenders = [characters[name] for name in self.defender_names]
        for defender in defenders:
            defender.receive_damage(attacker, self.damagePoints)

    def __str__(self):
        return f"DealDamageOnMultipleCharacters: {self.damagePoints} damage points from attacker {self.attacker_name} on defenders {self.defender_names}"


class HealingMultipleCharacters:
    def __init__(self, character_names, amount):
        self.amount = amount
        self.character_names = character_names

    def play(self, characters):
        patients = [characters[name] for name in self.character_names]
        for p in patients:
            p.heal(self.amount)

    def __str__(self):
        return f"HealingMultipleCharacters: {self.amount} healing to {self.character_names}"


class Character:
    def __init__(self, name, health=1000, isAlive=True, level=1, factions=None):
        self.name = name
        self.health = health
        self.isAlive = isAlive
        self.level = level
        self.factions = factions or []

    def receive_damage(self, attacker, damagePoints):
        if self == attacker or self.same_faction(attacker):
            return

        if self.level >= (attacker.level + 5):
            self.health -= int(damagePoints / 2.0)

        elif self.level <= (attacker.level - 5):
            self.health -= int(damagePoints * 1.5)

        else:
            self.health -= damagePoints

        if (self.health <= 0):
            self.health = 0
            self.isAlive = False

    def heal(self, damagePoints):
        if not self.isAlive:
            return

        self.health += damagePoints
        if self.health > 1000:
            self.health = 1000

    def same_faction(self, character):
        for faction in self.factions:
            if faction in character.factions:
                return True
        return False


def play(characters, move):
    move.play(characters)
