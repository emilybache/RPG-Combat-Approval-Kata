
#include <string>
#include <ostream>
#include "PlayerCharacter.h"


void PlayerCharacter::receiveDamage(PlayerCharacter *enemy, int damage) {
    // TODO: update health
}

string PlayerCharacter::getName() const {
    return _name;
}

std::ostream &operator<<(ostream &os, const PlayerCharacter &player) {
    // TODO: print the other characteristics of the character like health
    os << player.getName() << endl;
    return os;
}

