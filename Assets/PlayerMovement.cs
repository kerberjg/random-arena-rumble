using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    GameObject playerSprite;
    Rigidbody2D rigidBody;

    public float speed = 5f;
    Vector2 movement;

    //Aim
    int layerMask;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        layerMask = LayerMask.GetMask("AimLayer");
        playerSprite = GameObject.Find("Appearance");
    }
    void Update()

    {
        movement.x = Input.GetAxisRaw("Horizontal"); //Get Horizontal Input
        movement.y = Input.GetAxisRaw("Vertical"); //Get Vertical Input

        PlayerAiming();

    }

    private void PlayerAiming()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        playerSprite.transform.up = aimDirection;
    }

    void FixedUpdate()
    {
        transform.Translate(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0); //Move player based on input.
    }
}
