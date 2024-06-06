package codingdojo;

import com.spun.util.StringUtils;
import org.approvaltests.Approvals;
import org.approvaltests.StoryBoard;
import org.junit.jupiter.api.Test;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;

class PlayerCharacterTest {
    @Test
    void dealDamage() {
        // arrange
        var hero = new PlayerCharacter("Hero");
        var orc = new PlayerCharacter("Orc");
        var story = new StoryBoard();
        story.addDescription("Hero battles orc");

        story.addFrame("Given Hero initial status", hero);

        // act
        hero.receiveDamage(orc, 100);
        story.addFrame("When Orc deals 100 damage to Hero", hero);

        // assert
        Approvals.verify(story);
    }

}
