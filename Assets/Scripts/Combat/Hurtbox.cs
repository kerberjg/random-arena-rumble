using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    /// Whether an entity can be healed back to life after death
    public bool allowResuscitation = false;
    public bool destroyOnDeath = false;
    /// Maximum health value. The health is initialized to this value
    public float maxHealth = 0;
    public string hurtSoundName = "PlayerTakeDamage";
    
    public float health;

    void Start()
    {
        // apply health modifier
        maxHealth *= Mathf.Max(ValueModifier.TryGetModifier(this).health, ValueModifier.MIN_VALUE);

        // initialize values
        health = maxHealth;
    }

    /// Decrements the health by the specified amount
    /// Returns true if the entity dies on hit
    public bool Hit(float damage) {
        float newHealth = this.health - damage;

        if(newHealth > 0) {
            // play sound effect
            if(ValueModifier.TryGetModifier(this).randomSounds) {
                SoundManager.i.SetPitch("Quack", Random.Range(0.75f, 0.9f));
                SoundManager.i.PlayOnce("Quack");
                SoundManager.i.SetPitch("Quack", 1);
            } else {
                SoundManager.i.PlayOnce(this.hurtSoundName);
            }
            
            // set health values
            this.health = newHealth;
            return false;
        } else {
            health = 0;
            if(destroyOnDeath) {
                Destroy(gameObject);
            }

            return true;
        }
    }

    public bool isAlive {
        get { return this.health > 0; }
    }

    /// Increments the health by the specified amount
    /// Returns true if health is maxed out as a result
    public bool Heal(float healing) {
        float newHealth = this.health + healing;

        if(this.allowResuscitation || this.isAlive) {
            this.health = newHealth;
        }

        return this.health == this.maxHealth;
    }

    /// Heals the entity to max health
    /// Always returns true
    public bool Heal() {
        return this.Heal(float.PositiveInfinity);
    }
}
