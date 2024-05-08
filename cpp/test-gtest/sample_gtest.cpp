#include <gtest/gtest.h>
#include "PlayerCharacter.h"
#include "Approvals.h"

using namespace std;


TEST(PlayerCharacterTest, dealDamage) {
    auto hero = new PlayerCharacter("Hero");
    auto orc = new PlayerCharacter("Orc");
    auto toVerify = stringstream();
    toVerify << *hero;

    toVerify << "Orc deals 100 damage to Hero\n";
    hero->receiveDamage(orc, 100);

    toVerify << *hero;
    ApprovalTests::Approvals::verify(toVerify.str());
}