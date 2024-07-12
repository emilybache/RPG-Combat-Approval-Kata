#include <string>
#include <utility>

#ifndef RPG_COMBAT_PLAYERCHARACTER_H
#define RPG_COMBAT_PLAYERCHARACTER_H


class PlayerCharacter {

public:
    explicit PlayerCharacter(std::string  name) : _name(std::move(name)) {};

    void receiveDamage(PlayerCharacter *enemy, int damage);

    friend std::ostream& operator<<(std::ostream& os, const PlayerCharacter& player);

private:
    std::string _name;

};


#endif //RPG_COMBAT_PLAYERCHARACTER_H
