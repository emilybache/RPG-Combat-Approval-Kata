#include "ApprovalTests.hpp"
#include "doctest/doctest.h"
#include "PlayerCharacter.h"

using namespace std;

TEST_CASE ("PlayerCharacter") {
    auto hero = PlayerCharacter("Hero");
    auto enemy = PlayerCharacter("Orc");
    auto battleReport = stringstream();
    battleReport << hero;

    SUBCASE("HeroDefeatsOrc") {

        battleReport << "Orc deals 100 damage to Hero\n";
        hero.receiveDamage(&enemy, 100);

        battleReport << hero;
        ApprovalTests::Approvals::verify(battleReport.str());
    }
}


