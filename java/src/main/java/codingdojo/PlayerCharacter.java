package codingdojo;

import java.util.HashSet;
import java.util.Set;

public class PlayerCharacter {
    private final String name;
    private int health;
    private int level;
    private boolean alive;
    Set<Faction> factions = new HashSet<>();

    public PlayerCharacter(String name) {
        this(name, 1000, 1, true);
    }

    public PlayerCharacter(String name, int health, int level, boolean alive) {
        this.name = name;
        this.health = health;
        this.level = level;
        this.alive = alive;
    }

    public void receiveDamage(PlayerCharacter enemy, long damagePoints) {
        if (this.equals(enemy) || sameFaction(enemy)) {
            return;
        }
        if (this.level >= (enemy.level + 5)) {
            this.health -= Math.round(damagePoints/2.0);
        } else if (this.level <= (enemy.level -5)) {
            this.health -= (Math.round(damagePoints*1.5));
        } else {
            this.health -= damagePoints;
        }
        if (this.health <= 0) {
            this.health = 0;
            this.alive = false;
        }
    }

    private boolean sameFaction(PlayerCharacter otherCharacter) {
        for (Faction f :
                factions) {
            if (otherCharacter.factions.contains(f)) {
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

    public void joinFaction(Faction faction) {
        this.factions.add(faction);
    }

    public void leaveFaction(Faction faction) {
        this.factions.remove(faction);
    }

}
