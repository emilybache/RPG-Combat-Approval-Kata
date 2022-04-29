
#include <string>
#include <ostream>
#include "PlayerCharacter.h"


void PlayerCharacter::receiveDamage(PlayerCharacter *enemy, int damagePoints) {

    if (_level >= (enemy->_level + 5)) {
        _health -= static_cast<int>(damagePoints/2.0);
    } else if (_level <= (enemy->_level -5)) {
        _health -= (static_cast<int>(damagePoints*1.5));
    } else {
        _health -= damagePoints;
    }
    if (_health <= 0) {
        _health = 0;
        _isAlive = false;
    }
}

string PlayerCharacter::getName() const {
    return _name;
}

std::ostream &operator<<(ostream &os, const PlayerCharacter &player) {
    os << "name: " << player._name << ", "
       << "health: " << player._health << ", "
       << "level: " << player._level << ", "
       << "alive: " << player._isAlive << ""
       ;
    return os;
}

PlayerCharacter::PlayerCharacter(string &name, int initialHealth, int level, bool isAlive) :
        _name(name), _health(initialHealth), _level(level), _isAlive(isAlive) {}

PlayerCharacter::PlayerCharacter(const string &name) :
        _name(name), _health(1000), _level(1), _isAlive(true) {}

