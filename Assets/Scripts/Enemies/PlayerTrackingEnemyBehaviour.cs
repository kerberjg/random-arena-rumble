using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlayerTrackingEnemyBehaviour : MonoBehaviour
{
    public Transform target;
    public float updateFrequency = 1f;
    public float lerpSpeed = 1f;

    protected Vector3 _targetPos { get; private set; }
    
    private Vector3 _currentPos;
    private Vector3 _prevPos;
    private float _lastUpdate;

    // TODO(James): add a minRadius and maxRadius properties for distance-keeping

    void Update() {
        float now = Time.time;
        float currentStep = _lastUpdate - now;
        float stepLength = 1f / updateFrequency;

        // update tracker position
        if(currentStep >= stepLength) {
            _prevPos = _currentPos;
            _currentPos = target.position;
            _lastUpdate = now;
        }

        // lerp position over time
        _currentPos = Vector3.Lerp(_prevPos, _currentPos, currentStep / stepLength);

        // call actual behavior update
        this.EnemyUpdate();
    }

    abstract public void EnemyUpdate();
}
