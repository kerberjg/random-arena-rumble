using UnityEngine;

public abstract class ThrottledMonoBehaviour : MonoBehaviour {
    public int _throttle;

    public ThrottledMonoBehaviour(int throttleValue) {
        this._throttle = throttleValue;
    }

    void Update() {
        if(Time.frameCount % _throttle == 0)
            ThrottledUpdate();
    }

    protected abstract void ThrottledUpdate();
}