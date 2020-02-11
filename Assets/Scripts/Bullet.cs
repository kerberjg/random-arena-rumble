using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
    
{
    public float bulletSpeed = 5f;
    public float spread;
    public bool piercing;
    public float damage;
    public float lifeTime_Bullet;
   

    public List<string> targetTag;


    void Update()
    {
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);

        Destroy(gameObject, lifeTime_Bullet);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (piercing && collision.gameObject.tag == "Wall") {
            Destroy(gameObject);
            print("Bullet Piercing");
        }
        else {
            foreach (string tag in targetTag) {
                if(collision.gameObject.tag == tag) {
                    ApplyDamage(collision.gameObject);
                    Destroy(gameObject);
                    return;
                }
            }
        }
       
    }

    void ApplyDamage(GameObject other) {
        Health h;
        
        if(other.TryGetComponent<Health>(out h)) {
            h.Hit(damage);
            Debug.Log("Hit " + other.tag + ": " + damage);
        } 
    }
}
