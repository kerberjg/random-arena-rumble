using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    none,
    pistol,
    sniper,
    shotgun,
    enemyPistol,
};

public enum HatType {
    none,
    cowboy,
    party,
    wizard,
    top
}

public enum ModifierType {
    incremental,
    temporary
}

[System.Serializable]
public struct ValueModifier {
    public static readonly float MIN_VALUE = 0.001f;

    public ModifierType type;
    public Sprite icon;

    public float speed;
    public float health;
    public float damage;
    public float scale; 

    public List<HatType> hats;
    public bool randomSounds;

    public WeaponType leftWeapon;
    public WeaponType rightWeapon;


    public ArenaModifierType arena;

    public static ValueModifier Default() {
        ValueModifier x = new ValueModifier();
        x.SetDefaults();
        return x;
    }

    public void MergeModifier(ValueModifier other) {
        if(other.speed > MIN_VALUE) this.speed *= other.speed;
        if(other.health > MIN_VALUE) this.health *= other.health;
        if(other.damage > MIN_VALUE) this.damage *= other.damage;
        if(other.scale > MIN_VALUE) this.scale *= other.scale;

        if(other.hats != null) {
            if(this.hats != null)
                this.hats.AddRange(other.hats);
            else
                this.hats = other.hats;
        }

        this.randomSounds = other.randomSounds;

        if(type == ModifierType.incremental) {
            if(other.leftWeapon != WeaponType.none) this.leftWeapon = other.leftWeapon;
            if(other.rightWeapon != WeaponType.none) this.rightWeapon = other.rightWeapon;
        } else if(type == ModifierType.temporary) {
            this.leftWeapon = other.leftWeapon;
            this.rightWeapon = other.rightWeapon;
        }

        this.arena = other.arena;
    }

    public void SetDefaults() {
        this.type = ModifierType.incremental;
        this.icon = null;

        this.speed = 1f;
        this.health = 1f;
        this.damage = 1f;
        this.scale = 1f;

        this.hats = new List<HatType>();
        this.randomSounds = false;

        this.leftWeapon = WeaponType.none;
        this.rightWeapon = WeaponType.none;

        this.arena = ArenaModifierType.none;
    }

    public static ValueModifier TryGetModifier(Component obj) {
        try {
            ValueModifier modifier = obj.GetComponentInParent<ModifierContainer>().modifier;
            return modifier;
        } catch {
            return ValueModifier.Default();
        }
    }
}

public class ModifierContainer : MonoBehaviour {
    public ValueModifier modifier = ValueModifier.Default();
}
