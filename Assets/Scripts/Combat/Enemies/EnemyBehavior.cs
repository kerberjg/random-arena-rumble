using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour {
    public ValueModifier modifier { get; private set; }

    void LateUpdate() {
        // update modifier
        this.modifier = GetComponentInParent<ModifierContainer>().modifier;
    }
}