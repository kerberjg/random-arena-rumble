using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ValueModifier {
    public float speed;
    public float health;
    public float damage;
    public float size; 
}

public sealed class ModifierContainer : MonoBehaviour {
    public ValueModifier modifier = new ValueModifier {
        speed = 1f, health = 1f, damage = 1f, size = 1f
    };
}