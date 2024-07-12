
#include <string>
#include <ostream>
#include "PlayerCharacter.h"

using namespace std;

void PlayerCharacter::receiveDamage(PlayerCharacter *enemy, int damage) {
    // TODO: update health
}

std::ostream &operator<<(ostream &os, const PlayerCharacter &player) {
    // TODO: print the other characteristics of the character like health
    os << player._name << endl;
    return os;
}

