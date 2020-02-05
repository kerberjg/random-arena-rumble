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

        if(Input.GetKey(KeyCode.Space) && timeBetweenBullets > fireRate) {
            
            //Shoot
            Instantiate(bullet, gunBarrel.transform.position, playerAppearance.transform.rotation);

            timeBetweenBullets = 0;
        }
    }
}
