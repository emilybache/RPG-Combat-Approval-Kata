using System;
using System.Collections.Generic;

namespace rpg;

public class PlayerCharacter
{
    private string name;
    private int health;
    private int level;
    private bool alive;
    HashSet<Faction> factions = new HashSet<Faction>();

    public PlayerCharacter(string name) : this(name, 1000, 1, true) {
    }
    
    public PlayerCharacter(string name, int health, int level, bool alive) {
        this.name = name;
        this.health = health;
        this.level = level;
        this.alive = alive;
    }

    public void receiveDamage(PlayerCharacter enemy, int damagePoints)
    {
        if (this == enemy || sameFaction(enemy)) {
            return;
        }
        if (this.level >= (enemy.level + 5)) {
            this.health -= (int)Math.Round(damagePoints/2.0);
        } else if (this.level <= (enemy.level -5)) {
            this.health -= (int)(Math.Round(damagePoints*1.5));
        } else {
            this.health -= damagePoints;
        }
        if (this.health <= 0) {
            this.health = 0;
            this.alive = false;
        }
    }
    
    private bool sameFaction(PlayerCharacter otherCharacter) {
        foreach (var faction in factions)
        {
            if (otherCharacter.factions.Contains(faction)) {
                return true;
            }
        }

        return false;
    }

    public void heal(int damagePoints) {
        if (!this.alive) {
            return;
        }
        this.health += damagePoints;
        if (this.health > 1000) {
            this.health = 1000;
        }
    }

    public void heal(PlayerCharacter healer, int points) {
        if (sameFaction(healer)) {
            this.heal(points);
        }
    }

    public void joinFaction(Faction f) {
        this.factions.Add(f);
    }

    public void leaveFaction(Faction f) {
        this.factions.Remove(f);
    }

    public string PrintSimply()
    {
        return "PlayerCharacter{" +
               "name='" + name + '\'' +
               ", health=" + health +
               ", level=" + level +
               ", alive=" + alive +
               '}';
    }

    public string PrintDetails()
    {
        return "PlayerCharacter{" +
               "name='" + name + '\'' +
               ", health=" + health +
               ", level=" + level +
               ", alive=" + alive +
               ", factions=" + string.Join(", ", factions) +
               '}';
    }
}


public record Faction
{
    private readonly string name;

    public Faction(string name)
    {
        this.name = name;
    }

    public override string ToString()
    {
        return name;
    }
}