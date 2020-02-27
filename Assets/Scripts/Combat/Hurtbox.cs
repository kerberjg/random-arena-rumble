using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    /// Whether an entity can be healed back to life after death
    public bool allowResuscitation = false;
    public bool destroyOnDeath = false;
    public string hurtSoundName = "PlayerTakeDamage";

    [Header("Health")]
    /// Maximum health value. The health is initialized to this value
    public float maxHealth = 0;
        public float health;

    [Header("Invulnerability")]
    public float invulnerabilityTime = 1f;
    public float invulnerabilityBlink = 0.0666f;
    private float invTimer = 0f;


    void Start()
    {
        // apply health modifier
        maxHealth *= Mathf.Max(ValueModifier.TryGetModifier(this).health, ValueModifier.MIN_VALUE);

        // initialize values
        health = maxHealth;
    }

    void LateUpdate() {
        // update invulnerability state
        if(invTimer > 0) {
            invTimer = Mathf.Clamp(invTimer - Time.deltaTime, 0, invulnerabilityTime);

            // update invulnerability blink
            bool blink = Mathf.RoundToInt( invTimer / invulnerabilityBlink ) % 2 == 0;
            SetVisibility(blink);    
        }
    }

    protected virtual void SetVisibility(bool blink) {
        Renderer[] sprites = GetComponentsInChildren<Renderer>();
        foreach (Renderer a in sprites) {
            if(a.gameObject.activeSelf)
                a.enabled = blink;
        }
    }

    /// Decrements the health by the specified amount
    /// Returns true if the entity dies on hit
    public bool Hit(float damage) {
        // no damage taken if currently invulnerable
        if(invTimer > 0) {
            return false;
        }

        float newHealth = this.health - damage;

        if(newHealth > 0) {
            // play sound effect
            if(ValueModifier.TryGetModifier(this).randomSounds) {
                SoundManager.i.PlayOnce("Quack");
            } else {
                SoundManager.i.PlayOnce(this.hurtSoundName);
            }
            
            // set health values
            this.health = newHealth;

            // start invulnerability
            invTimer = invulnerabilityTime;

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
