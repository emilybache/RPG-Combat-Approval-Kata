package codingdojo;

import com.spun.util.StringUtils;
import org.approvaltests.Approvals;
import org.junit.jupiter.api.Test;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;

class PlayerCharacterTest {
    @Test
    void dealDamage() {
        // arrange
        var hero = new PlayerCharacter("Hero");
        var enemy = new PlayerCharacter("Orc");
        var toVerify = new StringBuilder();
        toVerify.append(hero.toString() + "\n");

        // act
        toVerify.append("Orc deals 100 damage to Hero\n");
        hero.receiveDamage(enemy, 100);

        // assert
        toVerify.append(hero.toString() + "\n");
        Approvals.verify(toVerify);
    }

}
