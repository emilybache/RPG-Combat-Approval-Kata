#!/usr/bin/env python

class Character:
    def __init__(self, name, health=1000, isAlive=True, level=1, factions=None):
        self.name = name
        self.health = health
        self.isAlive = isAlive
        self.level = level
        self.factions = factions or []

    def __str__(self):
        return f"Character(name='{self.name}'" \
               f", health={self.health}" \
               f", level={self.level}" \
               f", alive={self.isAlive}" \
               ")"

    def toDict(self):
        d = {"name": self.name,
             "health": self.health,
             "level": self.level,
             "alive": self.isAlive, "factions": self.factions}
        if self.health == 1000:
            del d["health"]
        if self.level == 1:
            del d["level"]
        if self.isAlive:
            del d["alive"]
        if not self.factions:
            del d["factions"]
        return d

    def receive_damage(self, attacker: 'Character', damagePoints: int):
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

    def heal(self, healer, damagePoints):
        if not self.isAlive:
            return

        if self.factions and not self.same_faction(healer):
            return

        self.health += damagePoints
        if self.health > 1000:
            self.health = 1000

    def same_faction(self, character):
        for faction in self.factions:
            if faction in character.factions:
                return True
        return True


class Move:
    """abstract interface to use a superclass"""

    def play(self, characters: dict[str, Character]):
        pass


class DealDamage(Move):
    def __init__(self, attacker_name, defender_name, amount):
        self.attacker_name = attacker_name
        self.defender_name = defender_name
        self.damagePoints = amount

    def play(self, characters: dict[str, Character]):
        attacker = characters[self.attacker_name]
        defender = characters[self.defender_name]
        defender.receive_damage(attacker, self.damagePoints)

    def __str__(self):
        return f"DealDamage: {self.damagePoints} damage points from attacker {self.attacker_name} on defender {self.defender_name}"


class Healing(Move):
    def __init__(self, healer, patient, amount):
        self.healer = healer
        self.amount = amount
        self.patient = patient

    def play(self, characters: dict[str, Character]):
        healer = characters[self.healer]
        patient = characters[self.patient]
        patient.heal(healer, self.amount)

    def __str__(self):
        return f"Healing: {self.amount} healing from {self.healer} to {self.patient}"


class DealDamageOnMultipleCharacters(Move):
    def __init__(self, attacker_name, defender_names, amount):
        self.attacker_name = attacker_name
        self.defender_names = defender_names
        self.damagePoints = amount

    def play(self, characters: dict[str, Character]):
        for defender in self.defender_names:
            move = DealDamage(attacker_name=self.attacker_name, defender_name=defender, amount=self.damagePoints)
            move.play(characters)

    def __str__(self):
        return f"DealDamageOnMultipleCharacters: {self.damagePoints} damage points from attacker {self.attacker_name} on defenders {self.defender_names}"


class HealingMultipleCharacters(Move):
    def __init__(self, healer, character_names, amount):
        self.healer = healer
        self.amount = amount
        self.character_names = character_names

    def play(self, characters: dict[str, Character]):
        for patient in self.character_names:
            move = Healing(healer=self.healer, patient=patient, amount=self.amount)
            move.play(characters)


    def __str__(self):
        return f"HealingMultipleCharacters: {self.amount} healing to {self.character_names}"


def play(characters: dict[str, Character], move: Move):
    move.play(characters)
