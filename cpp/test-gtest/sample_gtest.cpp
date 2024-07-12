#include <gtest/gtest.h>
#include "PlayerCharacter.h"
#include "Approvals.h"

using namespace std;


TEST(PlayerCharacterTest, HeroDefeatsOrc) {
    auto hero = PlayerCharacter("Hero");
    auto orc = PlayerCharacter("Orc");
    auto battleReport = stringstream();
    battleReport << hero;

    battleReport << "Orc deals 100 damage to Hero.\n";
    hero.receiveDamage(&orc, 100);

    battleReport << hero;
    ApprovalTests::Approvals::verify(battleReport.str());
}