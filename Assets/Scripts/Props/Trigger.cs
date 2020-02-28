using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerController controller;
    public int value = 1;

    public Material materialUnactive;
    public Material materialActive;
    bool active = false;
    
    void OnEnable() {
        controller.Register(this);
    }

    void OnTriggerCollision2D(Collider2D collider) {
        if(!active && collider.gameObject.tag == "Player") {
            Activate();
        }
    }

    void LateUpdate() {
        if(this.active && controller.currentCount == 0) {
            Deactivate();
        }
    }

    public void Activate() {
        active = true;

        Renderer r;
        if(TryGetComponent<Renderer>(out r)) {
            r.material = materialActive;
        }

        SoundManager.i.PlayOnce("ButtonActivate");
    }

    public void Deactivate() {
        active = false;

        Renderer r;
        if(TryGetComponent<Renderer>(out r)) {
            r.material = materialActive;
        }

        SoundManager.i.PlayOnce("ButtonDeactivate", true);
    }
}
