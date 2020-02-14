using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public GameObject shouldersPivot;
    BoxCollider2D playerCollider;
    
    Rigidbody2D rigidBody;

    public float speed = 5f;
    Vector2 movement;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        //playerSprite = GameObject.Find("Appearance");
    }
    void Update()

    {
        movement.x = Input.GetAxisRaw("Horizontal"); //Get Horizontal Input
        movement.y = Input.GetAxisRaw("Vertical"); //Get Vertical Input

        PlayerAiming();

    }

    private void PlayerAiming()
    {
        shouldersPivot.transform.up = MouseUtils.GetAimDirection(transform.position);
    }

    void FixedUpdate()
    {
        transform.Translate(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0); //Move player based on input.
    }

}
