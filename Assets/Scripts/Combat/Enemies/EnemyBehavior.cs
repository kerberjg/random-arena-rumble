using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyBehavior : MonoBehaviour {
    public ValueModifier modifier { get; private set; }

    void LateUpdate() {
        // update modifier
        this.modifier = ValueModifier.TryGetModifier(this);
    } 
}