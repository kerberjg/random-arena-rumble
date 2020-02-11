using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage;
    public bool destroyOnImpact;
    public List<string> targetTag;

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        foreach (string tag in targetTag) {
            if(collision.gameObject.tag == tag) {
                ApplyDamage(collision.gameObject);

                if(destroyOnImpact) {
                    Destroy(gameObject);
                }
                return;
            }
        }
    }

    protected void ApplyDamage(GameObject other) {
        Hurtbox h;
        
        if(other.TryGetComponent<Hurtbox>(out h)) {
            h.Hit(damage);
            Debug.Log("Hit " + other.tag + ": " + damage);
        } 
    }
}
