using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GunShooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gunBarrel;
    GameObject playerAppearance;
    public float fireRate;
    float timeBetweenBullets;

    private void Start()
    {       
        playerAppearance = GameObject.Find("Appearance");
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenBullets += Time.deltaTime;

        gunBarrel.transform.rotation = playerAppearance.transform.rotation;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        gunBarrel.transform.up = aimDirection;

        if (Input.GetKey(KeyCode.Space) && timeBetweenBullets > fireRate) {
            
            //Shoot
            GameObject b = Instantiate(bullet, gunBarrel.transform.position, gunBarrel.transform.rotation);
            b.GetComponent<BulletMover>().targetTag.Add("Enemy");

            timeBetweenBullets = 0;
        }
    }
}
