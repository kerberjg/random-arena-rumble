using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Hitbox {
    /* public float damage; */
    public float bulletSpeed = 5f;
    public float spread;
    public bool piercing;
    public float lifeTime_Bullet;
    /* public List<string> targetTag; */

    Bullet() {
        destroyOnImpact = true;
    }

    void Update()
    {
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);

        Destroy(gameObject, lifeTime_Bullet);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pierceable" && piercing) { //
            //Pierce object
        } else {
            SoundManager.i.PlayOnce("BulletHit" + collision.gameObject.tag);
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pierceable" && piercing) {
            //Pierce object
        } else {
            Destroy(gameObject);
        }
    }
    /*
    protected override void OnImpact(Collider2D collision)
    {
        SoundManager.i.PlayOnce("BulletHit" + collision.gameObject.tag);
        
        if (piercing && collision.gameObject.tag == "Wall") {
            Destroy(gameObject);
           // print("Bullet Piercing");
        }
        else {
            base.OnImpact(collision);
        }
    } */
}
