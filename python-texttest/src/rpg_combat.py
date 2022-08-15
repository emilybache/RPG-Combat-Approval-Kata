#!/usr/bin/env python

class Character:
    def __init__(self, name, health, isAlive):
        self.name = name
        self.health = health
        self.isAlive = isAlive

    def receive_damage(self, attacker: 'Character', damagePoints: int):
        # TODO: implement this functionality
        pass

    def __str__(self) -> str:
        "This is the Printer you need to implement so that you can see what happens to a character during combat"
        return super().__str__()


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

    def __str__(self) -> str:
        "This is the Printer you need to implement so that you can see what moves happen during combat"
        return super().__str__()


def play(characters: dict[str, Character], move: Move):
    move.play(characters)
