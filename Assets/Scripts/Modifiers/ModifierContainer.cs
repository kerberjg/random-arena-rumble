using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    none,
    pistol,
    sniper,
    shotgun,
    enemyPistol,
    akimbo
};

public enum HatType {
    none,
    cowboy,
    party,
    wizard,
    top
}

[System.Serializable]
public struct ValueModifier {
    public static readonly float MIN_VALUE = 0.001f;

    public float speed;
    public float health;
    public float damage;
    public float scale; 

    public List<HatType> hats;
    public bool randomSounds;

    public WeaponType leftWeapon;
    public WeaponType rightWeapon;

    public static ValueModifier Default() {
        ValueModifier x = new ValueModifier();
        x.SetDefaults();
        return x;
    }

    public void MergeModifier(ValueModifier other) {
        if(other.speed > MIN_VALUE) this.speed += 1f - other.speed;
        if(other.speed > MIN_VALUE) this.health += 1f - other.health;
        if(other.speed > MIN_VALUE) this.damage += 1f - other.damage;
        if(other.speed > MIN_VALUE) this.scale += 1f - other.scale;

        if(other.hats != null) this.hats.AddRange(other.hats);
        this.randomSounds = other.randomSounds;

        this.leftWeapon = other.leftWeapon;
        this.rightWeapon = other.rightWeapon;
    }

    public void SetDefaults() {
        this.speed = 1f;
        this.health = 1f;
        this.damage = 1f;
        this.scale = 1f;

        this.hats = new List<HatType>();
        this.randomSounds = false;

        this.leftWeapon = WeaponType.none;
        this.rightWeapon = WeaponType.none;
    }
}

public class ModifierContainer : MonoBehaviour {
    public ValueModifier modifier = ValueModifier.Default();
}
