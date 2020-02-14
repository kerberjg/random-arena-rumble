using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ValueModifier {
    public float speed;
    public float health;
    public float damage;
    public float scale; 
}

public sealed class ModifierContainer : MonoBehaviour {
    public static readonly float MIN_VALUE = 0.001f;

    public ValueModifier modifier = new ValueModifier {
        speed = 1f, health = 1f, damage = 1f, scale = 1f
    };
}