using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
    
{
    public float bulletSpeed = 5f;
    public float lifeTime;
    public float spread;
    public bool piercing;


    public List<string> targetTag;

    void Update()
    {
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (piercing && collision.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
        else {
            foreach (string tag in targetTag) {

                if(collision.gameObject.tag == tag) {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
}
