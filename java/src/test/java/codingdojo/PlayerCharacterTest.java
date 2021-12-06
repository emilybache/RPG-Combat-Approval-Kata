package codingdojo;

import com.spun.util.StringUtils;
import org.approvaltests.Approvals;
import org.junit.jupiter.api.Test;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class PlayerCharacterTest {

    // arrange
    private StringBuilder toVerify = new StringBuilder();

    private String printCharacterBasics(List<PlayerCharacter> heroes) {
        var heroesPrintedSimply = heroes.stream().map(PlayerCharacter::printBasics).toList();
        return StringUtils.toString("Characters", heroesPrintedSimply);
    }

    @Test
    void dealDamage() {
        // arrange
        List<PlayerCharacter> heroes = List.of(
                new PlayerCharacter("Hero", 900, 1, true),
                new PlayerCharacter("Hero with max health", 1000, 1, true),
                new PlayerCharacter("dead hero", 0, 1, false),
                new PlayerCharacter("dying hero", 50, 1, true),
                new PlayerCharacter("very high level hero who gets reduced damage", 51, 6, true)
        );
        var enemy = new PlayerCharacter("Orc");
        toVerify.append(printCharacterBasics(heroes));


        // act
        toVerify.append("Orc deals 100 damage to each Hero\n");
        heroes.forEach(c -> c.receiveDamage(enemy, 100));

        // assert
        toVerify.append(printCharacterBasics(heroes));
        Approvals.verify(toVerify);
    }

    @Test
    void dealDamageFromPowerfulEnemy() {
        // arrange
        List<PlayerCharacter> heroes = List.of(
                new PlayerCharacter("Hero", 900, 1, true),
                new PlayerCharacter("Hero with max health", 1000, 1, true),
                new PlayerCharacter("dead hero", 0, 1, false),
                new PlayerCharacter("dying hero", 50, 1, true),
                new PlayerCharacter("hero who is same level as orc", 101, 6, true),
                new PlayerCharacter("hero who is 5 levels above orc", 51, 11, true)
        );
        var enemy = new PlayerCharacter("Orc", 1000, 6, true);
        toVerify.append(printCharacterBasics(heroes));

        // act
        toVerify.append("Level 6 Orc deals 100 damage to each Hero\n");
        heroes.forEach(c -> c.receiveDamage(enemy, 100));

        // assert
        toVerify.append(printCharacterBasics(heroes));
        Approvals.verify(toVerify);
    }

    @Test
    void cannotDamageYourself() {
        var hero = new PlayerCharacter("Hero");
        hero.receiveDamage(hero, 100);
        toVerify.append(hero);
        Approvals.verify(toVerify);
    }

    @Test
    void healing() {
        List<PlayerCharacter> heroes = List.of(
                new PlayerCharacter("Hero", 900, 1, true),
                new PlayerCharacter("Hero with max health", 1000, 1, true),
                new PlayerCharacter("dead hero", 0, 1, false)
        );
        toVerify.append(printCharacterBasics(heroes));

        // act
        toVerify.append("All Heroes heal 100 damage\n");
        heroes.forEach(c -> c.heal(100));

        // assert
        toVerify.append(printCharacterBasics(heroes));
        Approvals.verify(toVerify);
    }


    @Test
    void joinFactions() {
        var hero = new PlayerCharacter("Hero");
        hero.joinFaction(new Faction("Blues"));
        hero.joinFaction(new Faction("Reds"));
        // duplicate addition has no effect
        hero.joinFaction(new Faction("Reds"));
        // join and leave a faction
        hero.joinFaction(new Faction("Yellows"));
        hero.leaveFaction(new Faction("Yellows"));
        toVerify.append(hero);
        Approvals.verify(toVerify);
    }

    @Test
    void receiveNoDamageFromSameFaction() {
        var hero1 = new PlayerCharacter("Blue Hero 1");
        hero1.joinFaction(new Faction("Blues"));
        var hero2 = new PlayerCharacter("Blue Hero 2");
        hero2.joinFaction(new Faction("Blues"));
        hero1.receiveDamage(hero2, 100);
        toVerify.append(hero1);
        Approvals.verify(toVerify);
    }

    @Test
    void sameFactionCanHealCharacter() {
        var healer = new PlayerCharacter("Blue Healer");
        healer.joinFaction(new Faction("Blues"));

        List<PlayerCharacter> heroes = new java.util.ArrayList<>(List.of(
                new PlayerCharacter("Blue Hero 1 with max health"),
                new PlayerCharacter("Blue slightly injured hero", 900, 1, true),
                new PlayerCharacter("Blue dead hero", 0, 1, false)
        ));
        heroes.forEach(h -> h.joinFaction(new Faction("Blues")));
        heroes.add(new PlayerCharacter("Neutral slightly injured hero", 900, 1, true));
        toVerify.append(printCharacterBasics(heroes));

        // act
        toVerify.append("All Heroes get healed 100 damage by Blue healer\n");
        heroes.forEach(c -> c.heal(healer, 100));

        // assert
        toVerify.append(printCharacterBasics(heroes));
        Approvals.verify(toVerify);
    }
}
