using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ComparisonResult {
    lesser, equals, greater
}
abstract public class BaseEnemyBehavior : MonoBehaviour
{
    [Header("Tracking options")]
    public bool enableTracking = true;
    public Transform target;
    public float updateFrequency = 1f;
    public Vector3 targetPos { get; private set; }
    
    [Header("Following options")]
    public bool enableFollow = true;
    public float minTargetDistance = 0f;
    public float maxTargetDistance = 0f;
    public float distanceTolerance = 0.1f;
    public float followSpeed = 0f;

    [Header("Movement debug")]
    [SerializeField]
    private Vector3 _currentPos = new Vector3();
    [SerializeField]
    private Vector3 _prevPos = new Vector3();
    [SerializeField]
    private float _lastUpdate;

    void UpdateTracking() {
        float now = Time.time;
        float currentStep = now - _lastUpdate;
        float stepLength = 1f / updateFrequency;

        // update tracker position
        if(currentStep >= stepLength) {
            _prevPos = _currentPos;
            _currentPos = target.position;
            _lastUpdate = now;
        }
        // lerp position over time
        else {
            targetPos = Vector3.Lerp(_prevPos, _currentPos, currentStep / stepLength);
        }
    }

    [SerializeField]
    private Vector3 _tmpDist = new Vector3();

    public void UpdateFollow(Vector3 targetPos) {
        // calculate distance to target
        _tmpDist = this.transform.position - targetPos;
        float distance = _tmpDist.magnitude;
        _tmpDist.Normalize();

        // if following is activated, move towards/away from the target...
        if(followSpeed > 0f) {
            // move towards the target
            if(
                (maxTargetDistance > 0 && CompareDistances(distance, maxTargetDistance, distanceTolerance) > 0)
                ||
                (minTargetDistance > 0 && CompareDistances(distance, minTargetDistance, distanceTolerance) > 0)
            ) {
                _tmpDist = this.transform.position - _tmpDist * followSpeed * Time.deltaTime;
                Debug.Log("too far, follow");
            }
            // move away from the target
            else if(minTargetDistance > 0 && CompareDistances(distance, minTargetDistance, distanceTolerance) < 0) {
                _tmpDist = this.transform.position + _tmpDist * followSpeed * Time.deltaTime;
                Debug.Log("too close, follow");
            } else {
                Debug.Log("within constraints, follow");
                return;
            }
        }
        // ...otherwise just stay within the distance range
        else {
            if(maxTargetDistance > 0 && CompareDistances(distance, maxTargetDistance, distanceTolerance) > 0) {
                _tmpDist = targetPos + _tmpDist * maxTargetDistance;
                Debug.Log("over max, no follow");
            } else if(minTargetDistance > 0 && CompareDistances(distance, minTargetDistance, distanceTolerance) < 0) {
                _tmpDist = targetPos + _tmpDist * minTargetDistance;
                Debug.Log("over max, no follow");
            } else {
                Debug.Log("within constraints, no follow");
                return;
            }
        }

        this.transform.position = _tmpDist;
    }

    void Update() {
        if(enableTracking) UpdateTracking();
        if(enableFollow) UpdateFollow(targetPos);

        EnemyUpdate();
    }
    abstract public void EnemyUpdate();

    private float CompareDistances(float d, float constraint, float tolerance) {
        float val = d - constraint;

        if(Mathf.Abs(val) <= tolerance)
            return 0;
        else if(val > 0)
            return +1;
        else
            return -1;
    }
}
