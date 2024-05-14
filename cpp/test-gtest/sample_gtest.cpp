#include <gtest/gtest.h>
#include "PlayerCharacter.h"
#include "Approvals.h"

using namespace std;


TEST(PlayerCharacterTest, HeroDefeatsOrc) {
    auto hero = new PlayerCharacter("Hero");
    auto orc = new PlayerCharacter("Orc");
    auto battleReport = stringstream();
    battleReport << *hero;

    battleReport << "Orc deals 100 damage to Hero.\n";
    hero->receiveDamage(orc, 100);

    battleReport << *hero;
    ApprovalTests::Approvals::verify(battleReport.str());
}