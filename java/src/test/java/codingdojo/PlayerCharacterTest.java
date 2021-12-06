package codingdojo;

import com.spun.util.StringUtils;
import org.approvaltests.Approvals;
import org.junit.jupiter.api.Test;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class PlayerCharacterTest {
    @Test
    void dealDamage() {
        // arrange
        var hero = new PlayerCharacter("Hero");
        var enemy = new PlayerCharacter("Orc");
        var toVerify = new StringBuilder();

        // act
        toVerify.append("Orc deals 100 damage to Hero \n");
        hero.receiveDamage(enemy, 100);

        // assert
        toVerify.append(StringUtils.toString("Characters", List.of(hero, enemy)));
        Approvals.verify(toVerify);
    }

}
