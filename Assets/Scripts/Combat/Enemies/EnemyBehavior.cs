using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyBehavior : MonoBehaviour {
    public ValueModifier modifier { get; private set; }

    void LateUpdate() {
        // update modifier
        try {

        this.modifier = GetComponentInParent<ModifierContainer>().modifier;
        } catch(Exception e) {
            this.modifier = new ValueModifier() { speed = 1f };
            print("Enemy is Sad");
        }
    } 
}