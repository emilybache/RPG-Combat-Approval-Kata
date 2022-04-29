#ifndef RPB_COMBAT_PLAYERCHARACTER_H
#define RPB_COMBAT_PLAYERCHARACTER_H

using namespace std;

class PlayerCharacter {

public:
    explicit PlayerCharacter(const string& name);
    PlayerCharacter(string& name, int initialHealth, int level, bool isAlive);

    void receiveDamage(PlayerCharacter *enemy, int damage);

    string getName() const;

    friend std::ostream& operator<<(std::ostream& os, const PlayerCharacter& player);

private:
    string _name;
    int _health;
    int _level;
    bool _isAlive;

};


#endif //RPB_COMBAT_PLAYERCHARACTER_H
