using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GunShooting : MonoBehaviour
{
    public SpriteRenderer gunSpriteRenderer;
    public Sprite normalGun;
    public Sprite shootSprite;
    float shootTimer;
    bool isShooting;
    public float muzzleFlashDelay;
    

    public GameObject bullet;
    public GameObject gunBarrel;
    GameObject playerAppearance;

    public float fireRate;
    public float timeBetweenBullets;
    public float bulletSpeed;
    public float damage;
    public float lifeTime_Bullet;
    public bool piercing;


    private void Start()
    {       
        playerAppearance = GameObject.Find("Appearance");
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenBullets += Time.deltaTime;

        gunBarrel.transform.rotation = playerAppearance.transform.rotation;

        Vector2 mousePos = PlayerSpriteBehaviour.GetMousePosition();
        Vector2 aimDirection = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        gunBarrel.transform.up = aimDirection;

        if (Input.GetKey(KeyCode.Mouse0) && timeBetweenBullets > fireRate) {
            Shoot();
        }

        if (isShooting) {
            shootTimer += Time.deltaTime;

            if(shootTimer >= muzzleFlashDelay) {
                gunSpriteRenderer.sprite = normalGun;
                shootTimer = 0f;
                isShooting = false;
            }
        }

    }

    private void Shoot()
    {
        isShooting = true;

        GameObject b = Instantiate(bullet, gunBarrel.transform.position, gunBarrel.transform.rotation);
        b.GetComponent<Bullet>().targetTag.Add("Enemy");
        b.GetComponent<Bullet>().piercing = piercing;
        b.GetComponent<Bullet>().damage = damage;
        b.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        b.GetComponent<Bullet>().lifeTime_Bullet = lifeTime_Bullet;

        timeBetweenBullets = 0;

        gunSpriteRenderer.sprite = shootSprite;
        
    }
}
