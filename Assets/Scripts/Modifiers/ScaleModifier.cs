using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleModifier : MonoBehaviour {
    [SerializeField]
    private float scale = 1f;

    private float _previousScale;

    void LateUpdate() {
        // apply scale modifier
        ValueModifier modifier = GetComponentInParent<ModifierContainer>().modifier;
        float newScale = Mathf.Max(modifier.scale, ValueModifier.MIN_VALUE);

        if(scale != newScale) {
            _previousScale = scale;
            Vector3 currentScale = transform.localScale / scale;

            scale = newScale;
            transform.localScale = currentScale * scale;
        }
    }
}