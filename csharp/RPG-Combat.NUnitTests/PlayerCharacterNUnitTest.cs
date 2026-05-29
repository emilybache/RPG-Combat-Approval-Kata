using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using rpg;
using NUnit.Framework;
using VerifyNUnit;

namespace RPG_Combat.NUnitTests;

[TestFixture]
public sealed class PlayerCharacterNUnitTest
{
    private string PrintCharacters(IEnumerable<PlayerCharacter> characters)
    {
        var report = new StringBuilder("Characters:\n");
        foreach (var character in characters)
        {
            report.AppendLine($"    {character}");
        }
        return report.ToString();
    }

    [Test]
    public Task BasicBattle()
    {
        var report = new StringBuilder("\n");

        var hero = new PlayerCharacter("Hero", factions: new List<string> { "Elf" });
        var orc = new PlayerCharacter("Orc1", factions: new List<string> { "White Hand" });
        var characters = new List<PlayerCharacter> { hero, orc };
        
        report.Append(PrintCharacters(characters));

        int damage = 100;
        report.AppendLine($"\n{orc.Name} Receives {damage} Damage From {hero.Name}");
        orc.ReceiveDamage(hero, damage);

        report.Append(PrintCharacters(characters));

        return Verifier.Verify(report.ToString());
    }
}