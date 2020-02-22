using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSource : MonoBehaviour {
    [Header("Colors")]
    public Gradient gradient;
    public GradientColorKey[] colorKey;
    
    [Header("Timing")]
    public bool activateTimeflow = false;
    [Range(-1, +1)] public float time = 0f;

    

    void Start() {

    }
}