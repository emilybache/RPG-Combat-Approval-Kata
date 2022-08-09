#!/usr/bin/env python

import rpg_combat
import json


def print_character(character):
    "This is the Printer you need to implement so that you can see what's happened to the Character during combat"
    return f"Character(name='{character.name}'" \
           f", health={character.health}" \
           f", level={character.level}" \
           f", alive={character.isAlive}" \
           ")"


def print_move(move):
    "This is the Printer you need to implement so that you can see what moves happen during combat"
    return str(move)


def init_characters(characters_as_json):
    characters = json.loads(characters_as_json, object_hook=lambda c: rpg_combat.Character(**c))
    return dict((character.name, character) for character in characters)


def init_moves(moves_as_json):
    moves = []
    raw_moves = json.loads(moves_as_json)
    for move in raw_moves:
        moveType = move["moveType"]
        if moveType == "DealDamage":
            moves.append(rpg_combat.DealDamage(move["character1"], move["character2"], move["amount"]))
        elif moveType == "DealDamageOnMultipleCharacters":
            moves.append(rpg_combat.DealDamageOnMultipleCharacters(move["attacker"], move["defenders"], move["amount"]))
        elif moveType == "HealingMultipleCharacters":
            moves.append(rpg_combat.HealingMultipleCharacters(move["patients"], move["amount"]))
    return moves


def main():
    # Arrange
    with open("characters.json") as f:
        characters_as_json = f.read()

    with open("combat_moves.json") as f:
        combat_moves_as_json = f.read()

    characters = init_characters(characters_as_json)
    print_characters(characters)

    moves = init_moves(combat_moves_as_json)

    # Act
    print("")
    for move in moves:
        print(f"playing move {print_move(move)}")
        rpg_combat.play(characters, move)

    # Assert
    print_characters(characters)


def print_characters(characters):
    print("")
    print("Characters:")
    for name, character in list(sorted(characters.items())):
        print(f"    {print_character(character)}")


if __name__ == "__main__":
    main()
