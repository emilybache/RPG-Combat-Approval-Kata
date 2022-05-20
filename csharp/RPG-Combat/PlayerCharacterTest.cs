using System.Collections.Generic;
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
        [Fact]
        public void dealDamage()
        {
            // arrange
            var hero = new PlayerCharacter("Hero");
            var enemy = new PlayerCharacter("Orc");
            var toVerify = "";

            // act
            toVerify += "Orc deals 100 damage to Hero\n";
            hero.receiveDamage(enemy, 100);

            // assert
            toVerify += StringUtils.ToReadableString(new List<PlayerCharacter>{hero, enemy});
            Approvals.Verify(toVerify);
        }
    }
}
