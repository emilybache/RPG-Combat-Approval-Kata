using System;
using System.Collections.Generic;
using System.Linq;

namespace rpg;

public class PlayerCharacter
{
    public static int BugId = 0;

    public string Name { get; }
    public int Health { get; private set; }
    public bool IsAlive { get; private set; }
    public int Level { get; }
    public List<string> Factions { get; }

    public PlayerCharacter(string name, int health = 1000, bool isAlive = true, int level = 1, List<string> factions = null)
    {
        Name = name;
        Health = health;
        IsAlive = isAlive;
        Level = level;
        Factions = factions ?? new List<string>();
    }

    public override string ToString()
    {
        return $"Character(name='{Name}', health={Health}, level={Level}, alive={IsAlive})";
    }

    public Dictionary<string, object> ToDict()
    {
        var d = new Dictionary<string, object>
        {
            { "name", Name },
            { "health", Health },
            { "level", Level },
            { "alive", IsAlive },
            { "factions", Factions }
        };

        if (Health == 1000) d.Remove("health");
        if (Level == 1) d.Remove("level");
        if (IsAlive) d.Remove("alive");
        if (!Factions.Any()) d.Remove("factions");

        return d;
    }

    public void ReceiveDamage(PlayerCharacter attacker, int damagePoints)
    {
        if (this == attacker || (BugId != 1 && SameFaction(attacker)))
        {
            return;
        }

        if (Level >= attacker.Level + 5)
        {
            Health -= (int)(damagePoints / 2.0);
        }
        else if (Level <= attacker.Level - 5)
        {
            var multiplier = BugId == 6 ? 2.0 : 1.5;
            Health -= (int)(damagePoints * multiplier);
        }
        else
        {
            Health -= damagePoints;
        }

        if (Health <= 0)
        {
            Health = 0;
            IsAlive = false;
        }
    }

    public void Heal(PlayerCharacter healer, int damagePoints)
    {
        if (BugId != 2 && !IsAlive)
        {
            return;
        }

        if (Factions.Any() && !SameFaction(healer))
        {
            return;
        }

        Health += damagePoints;
        if (BugId != 5 && Health > 1000)
        {
            Health = 1000;
        }
    }

    public bool SameFaction(PlayerCharacter character)
    {
        if (BugId == 4) return true;
        return Factions.Any(f => character.Factions.Contains(f));
    }
}

public interface IMove
{
    void Play(Dictionary<string, PlayerCharacter> characters);
}

public class DealDamage : IMove
{
    private readonly string _attackerName;
    private readonly string _defenderName;
    private readonly int _damagePoints;

    public DealDamage(string attackerName, string defenderName, int amount)
    {
        _attackerName = attackerName;
        _defenderName = defenderName;
        _damagePoints = amount;
    }

    public void Play(Dictionary<string, PlayerCharacter> characters)
    {
        var attacker = characters[_attackerName];
        var defender = characters[_defenderName];
        defender.ReceiveDamage(attacker, _damagePoints);
    }

    public override string ToString()
    {
        return $"DealDamage: {_damagePoints} damage points from attacker {_attackerName} on defender {_defenderName}";
    }
}

public class Healing : IMove
{
    private readonly string _healer;
    private readonly string _patient;
    private readonly int _amount;

    public Healing(string healer, string patient, int amount)
    {
        _healer = healer;
        _patient = patient;
        _amount = amount;
    }

    public void Play(Dictionary<string, PlayerCharacter> characters)
    {
        var healerName = _healer;
        var patientName = _patient;
        if (PlayerCharacter.BugId == 3)
        {
            healerName = _patient;
            patientName = _healer;
        }
        var healer = characters[healerName];
        var patient = characters[patientName];
        patient.Heal(healer, _amount);
    }

    public override string ToString()
    {
        return $"Healing: {_amount} healing from {_healer} to {_patient}";
    }
}

public class DealDamageOnMultipleCharacters : IMove
{
    private readonly string _attackerName;
    private readonly List<string> _defenderNames;
    private readonly int _damagePoints;

    public DealDamageOnMultipleCharacters(string attackerName, List<string> defenderNames, int amount)
    {
        _attackerName = attackerName;
        _defenderNames = defenderNames;
        _damagePoints = amount;
    }

    public void Play(Dictionary<string, PlayerCharacter> characters)
    {
        foreach (var defenderName in _defenderNames)
        {
            var move = new DealDamage(_attackerName, defenderName, _damagePoints);
            move.Play(characters);
        }
    }

    public override string ToString()
    {
        return $"DealDamageOnMultipleCharacters: {_damagePoints} damage points from attacker {_attackerName} on defenders [{string.Join(", ", _defenderNames)}]";
    }
}

public class HealingMultipleCharacters : IMove
{
    private readonly string _healer;
    private readonly List<string> _characterNames;
    private readonly int _amount;

    public HealingMultipleCharacters(string healer, List<string> characterNames, int amount)
    {
        _healer = healer;
        _characterNames = characterNames;
        _amount = amount;
    }

    public void Play(Dictionary<string, PlayerCharacter> characters)
    {
        foreach (var patientName in _characterNames)
        {
            var move = new Healing(_healer, patientName, _amount);
            move.Play(characters);
        }
    }

    public override string ToString()
    {
        return $"HealingMultipleCharacters: {_amount} healing to [{string.Join(", ", _characterNames)}]";
    }
}

public static class Game
{
    public static void Play(Dictionary<string, PlayerCharacter> characters, IMove move)
    {
        move.Play(characters);
    }
}