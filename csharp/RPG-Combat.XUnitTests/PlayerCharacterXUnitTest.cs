using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace rpg
{
    public class PlayerCharacterXUnitTest
    {
        [Fact]
        public Task DealDamage()
        {
            // arrange
            var hero = new PlayerCharacter("Hero");
            var enemy = new PlayerCharacter("Orc");
            var toVerify = hero.ToString() + "\n";

            // act
            toVerify += "Orc deals 100 damage to Hero\n";
            hero.ReceiveDamage(enemy, 100);

            // assert
            toVerify += hero.ToString();
            return Verifier.Verify(toVerify);
        }
    }
}
