#include "ApprovalTests.hpp"
#include "catch2/catch.hpp"
#include "PlayerCharacter.h"


TEST_CASE ("PlayerCharacter") {
    auto hero = new PlayerCharacter("Hero");
    auto enemy = new PlayerCharacter("Orc");
    auto toVerify = stringstream();
    toVerify << *hero;

    SECTION("receiveDamage") {
        toVerify << "Orc deals 100 damage to Hero\n";
        hero->receiveDamage(enemy, 100);

        toVerify << *hero;
        ApprovalTests::Approvals::verify(toVerify.str());
    }
}


