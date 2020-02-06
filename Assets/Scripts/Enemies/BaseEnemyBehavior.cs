using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ComparisonResult {
    lesser, equals, greater
}
abstract public class BaseEnemyBehavior : MonoBehaviour
{
    [Header("Tracking options")]
    public Transform target;
    public float updateFrequency = 1f;
    public float lerpSpeed = 1f;
    protected Vector3 targetPos { get; private set; }
    
    [Header("Following options")]
    public float minTargetDistance = 0f;
    public float maxTargetDistance = 0f;
    public float distanceTolerance = 0.1f;
    public float followSpeed = 0f;

    private Vector3 _currentPos;
    private Vector3 _prevPos;
    private float _lastUpdate;

    void UpdateTracking() {
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
        if(lerpSpeed > 0) {
            targetPos = Vector3.Lerp(_prevPos, _currentPos, currentStep / stepLength);
        } else {
            targetPos = _currentPos;
        }
    }

    private Vector3 _tmpDist = new Vector3();
    void UpdateFollow() {
        // calculate distance to target
        _tmpDist = this.transform.position - targetPos;
        float distance = _tmpDist.magnitude;

        // if following is activated, move towards/away from the target...
        if(followSpeed > 0f) {
            // ignore if within tolerance
            if(Mathf.Abs(distance) >= distanceTolerance) {
                return;
            }

            // move towards the target
            if(
                (maxTargetDistance > 0 && distance > maxTargetDistance)
                ||
                (minTargetDistance > 0 && distance > minTargetDistance)
            ) {
                this.transform.position += _tmpDist.Normalize() * followSpeed * Time.deltaTime;
            }

            // move away from the target

        }
        // ...otherwise just stay within the distance range
        else {
            if(maxTargetDistance > 0 && distance > maxTargetDistance) {
                this.transform.position = targetPos + _tmpDist.Normalize() * maxTargetDistance;
            } else if(minTargetDistance > 0 && distance < minTargetDistance) {
                this.transform.position = targetPos + _tmpDist.Normalize() * minTargetDistance;
            }
        }
    }

    void Update() {
        UpdateTracking();
        UpdateFollow();

        EnemyUpdate();
    }
    abstract public void EnemyUpdate();
}
