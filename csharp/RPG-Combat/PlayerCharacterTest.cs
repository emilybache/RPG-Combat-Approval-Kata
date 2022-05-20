using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Xunit;

namespace rpg
{
    [UseReporter(typeof(QuietReporter))]
    public class PlayerCharacterTest
    {
        private StringBuilder toVerify = new StringBuilder();

        [Fact]
        public void dealDamage()
        {
            // arrange
            var hero = new PlayerCharacter("Hero");
            var enemy = new PlayerCharacter("Orc");
            var toVerify = printCharacterBasics(new List<PlayerCharacter> { hero, enemy });;

            // act
            toVerify += "\n\nOrc deals 100 damage to Hero\n\n";
            hero.receiveDamage(enemy, 100);

            // assert
            toVerify += printCharacterBasics(new List<PlayerCharacter> { hero, enemy });
            Approvals.Verify(toVerify);
        }

        private string printCharacterBasics(List<PlayerCharacter> heroes)
        {
            var heroesPrintedSimply = new List<string>();
            foreach (var hero in heroes)
            {
                heroesPrintedSimply.Add(hero.PrintSimply());
            }

            return string.Join("\n", heroesPrintedSimply);
        }


        [Fact]
        void dealDamageFromPowerfulEnemy()
        {
            // arrange
            List<PlayerCharacter> heroes = new List<PlayerCharacter>()
            {
                new PlayerCharacter("Hero", 900, 1, true),
                new PlayerCharacter("Hero with max health", 1000, 1, true),
                new PlayerCharacter("dead hero", 0, 1, false),
                new PlayerCharacter("dying hero", 50, 1, true),
                new PlayerCharacter("hero who is same level as orc", 101, 6, true),
                new PlayerCharacter("hero who is 5 levels above orc", 51, 11, true)
            };
            var enemy = new PlayerCharacter("Orc", 1000, 6, true);
            toVerify.Append(printCharacterBasics(heroes));

            // act
            toVerify.Append("\n\nLevel 6 Orc deals 100 damage to each Hero\n\n");
            heroes.ForEach(c => c.receiveDamage(enemy, 100));

            // assert
            toVerify.Append(printCharacterBasics(heroes));
            Approvals.Verify(toVerify);
        }

        [Fact]
        void cannotDamageYourself()
        {
            var hero = new PlayerCharacter("Hero");
            hero.receiveDamage(hero, 100);
            toVerify.Append(hero.PrintSimply());
            Approvals.Verify(toVerify);
        }

        [Fact]
        void healing()
        {
            List<PlayerCharacter> heroes = new List<PlayerCharacter>()
            {
                new PlayerCharacter("Hero", 900, 1, true),
                new PlayerCharacter("Hero with max health", 1000, 1, true),
                new PlayerCharacter("dead hero", 0, 1, false)
            };
            toVerify.Append(printCharacterBasics(heroes));

            // act
            toVerify.Append("\n\nAll Heroes heal 100 damage\n\n");
            heroes.ForEach(c => c.heal(100));

            // assert
            toVerify.Append(printCharacterBasics(heroes));
            Approvals.Verify(toVerify);
        }


        [Fact]
        void joinFactions()
        {
            var hero = new PlayerCharacter("Hero");
            hero.joinFaction(new Faction("Blues"));
            hero.joinFaction(new Faction("Reds"));
            // duplicate addition has no effect
            hero.joinFaction(new Faction("Reds"));
            // join and leave a faction
            hero.joinFaction(new Faction("Yellows"));
            hero.leaveFaction(new Faction("Yellows"));
            toVerify.Append(hero.PrintDetails());
            Approvals.Verify(toVerify);
        }

        [Fact]
        void receiveNoDamageFromSameFaction()
        {
            var hero1 = new PlayerCharacter("Blue Hero 1");
            hero1.joinFaction(new Faction("Blues"));
            var hero2 = new PlayerCharacter("Blue Hero 2");
            hero2.joinFaction(new Faction("Blues"));
            hero1.receiveDamage(hero2, 100);
            toVerify.Append(hero1.PrintDetails());
            Approvals.Verify(toVerify);
        }

        [Fact]
        void sameFactionCanHealCharacter()
        {
            var healer = new PlayerCharacter("Blue Healer");
            healer.joinFaction(new Faction("Blues"));

            List<PlayerCharacter> heroes = new List<PlayerCharacter>()
            {
                new PlayerCharacter("Blue Hero 1 with max health"),
                new PlayerCharacter("Blue slightly injured hero", 900, 1, true),
                new PlayerCharacter("Blue dead hero", 0, 1, false)
            };
            heroes.ForEach(h => h.joinFaction(new Faction("Blues")));
            heroes.Add(new PlayerCharacter("Neutral slightly injured hero", 900, 1, true));
            toVerify.Append(printCharacterBasics(heroes));

            // act
            toVerify.Append("\n\nAll Heroes get healed 100 damage by Blue healer\n\n");
            heroes.ForEach(c => c.heal(healer, 100));

            // assert
            toVerify.Append(printCharacterBasics(heroes));
            Approvals.Verify(toVerify);
        }
    }
}