#!/usr/bin/env python

import json


class Move:
    "abstract interface"

    def play(self, characters):
        pass


class DealDamage(Move):
    def __init__(self, attacker_name, defender_name, amount):
        self.attacker_name = attacker_name
        self.defender_name = defender_name
        self.damagePoints = amount

    def play(self, characters):
        attacker = characters[self.attacker_name]
        defender = characters[self.defender_name]
        defender.receive_damage(attacker, self.damagePoints)


class Character:
    def __init__(self, name, health, isAlive):
        self.name = name
        self.health = health
        self.isAlive = isAlive

    def receive_damage(self, attacker: Character, damagePoints: int):
        # TODO: implement this functionality
        pass


def play(characters, move):
    move.play(characters)
