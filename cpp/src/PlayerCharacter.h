#ifndef RPB_COMBAT_PLAYERCHARACTER_H
#define RPB_COMBAT_PLAYERCHARACTER_H

using namespace std;

class PlayerCharacter {

public:
    explicit PlayerCharacter(const string& name) : _name(name) {};

    void receiveDamage(PlayerCharacter *enemy, int damage);

    string getName() const;

    friend std::ostream& operator<<(std::ostream& os, const PlayerCharacter& player);

private:
    string _name;

};


#endif //RPB_COMBAT_PLAYERCHARACTER_H
