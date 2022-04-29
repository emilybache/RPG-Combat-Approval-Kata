#include "ApprovalTests.hpp"
#include "doctest/doctest.h"
#include "PlayerCharacter.h"

string doReceiveDamage(string &heroName, int health, int level, bool isAlive) {
    auto hero = new PlayerCharacter(heroName, health, level, isAlive);
    auto enemy = new PlayerCharacter("Orc");
    auto toVerify = stringstream();

    toVerify << "Orc deals 100 damage : ";
    hero->receiveDamage(enemy, 100);

    toVerify << *hero;
    string contents = toVerify.str();
    return contents;
}

TEST_CASE ("PlayerCharacter") {

    SUBCASE("receiveDamage") {

        vector<string> names {"Hero"};
        vector<int> healths {900, 1000, 0, 50, 51};
        vector<int> levels {1, 6};
        vector<bool> alive {true};
        ApprovalTests::CombinationApprovals::verifyAllCombinations(
                doReceiveDamage, names, healths, levels, alive);
    }
}


