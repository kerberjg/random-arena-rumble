using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerGroupType {
    singular, incremental, sequential
}

public class TriggerController : MonoBehaviour
{
    public TriggerGroupType type;

    public GameObject[] targets;

    public int currentCount { get; private set; }
    private int targetCount = 0;
    private bool isComplete = false;

    void Start()
    {
        
    }

    void Update()
    {
        // don't update if trigger is complete
        if(isComplete)
            return;

        bool flag = false;
        switch(type) {
            case TriggerGroupType.singular:
                flag = currentCount > 0;
                break;

            case TriggerGroupType.incremental:
                flag = currentCount == targetCount;
                break;

            case TriggerGroupType.sequential:
                flag = currentCount == targetCount;
                break;
        }

        if(flag) {
            OnComplete();
        }
    }

    public void Register(Trigger t) {
        switch(type) {
            case TriggerGroupType.singular:
                targetCount += t.value;
                break;

            case TriggerGroupType.incremental:
                targetCount += t.value;
                break;

            case TriggerGroupType.sequential:
                targetCount = Mathf.Max(targetCount, t.value);
                break;
        }
    }

    public bool Trigger(Trigger t) {
        // don't update if trigger is complete
        if(isComplete)
            return false;

        switch(type) {
            case TriggerGroupType.singular:
                ++currentCount;
                return true;

            case TriggerGroupType.incremental:
                currentCount += t.value;
                return true;

            case TriggerGroupType.sequential:
                if(t.value == currentCount + 1) {
                    ++currentCount;
                    return true;
                } else {
                    Reset();
                    return false;
                }

            default:
                return false;
        }
    }

    void OnComplete() {
        Debug.Log("Trigger complete!");
        isComplete = true;
        foreach(GameObject obj in targets) {
            Destroy(obj);
        }
    }

    void Reset() {
        currentCount = 0;
    }
}
