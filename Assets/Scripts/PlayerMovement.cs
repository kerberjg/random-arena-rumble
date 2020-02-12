﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public GameObject leftHandPivot;
    public GameObject rightHandPivot;
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
        Vector3 mousePos = PlayerSpriteBehaviour.GetMousePosition();
        Vector2 aimDirection_Left = new Vector2(mousePos.x - leftHandPivot.transform.position.x, mousePos.y - leftHandPivot.transform.position.y);
        Vector2 aimDirection_Right = new Vector2(mousePos.x - rightHandPivot.transform.position.x, mousePos.y - rightHandPivot.transform.position.y);

        leftHandPivot.transform.up = aimDirection_Left;
        rightHandPivot.transform.up = aimDirection_Right;

    }

    void FixedUpdate()
    {
        transform.Translate(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0); //Move player based on input.
    }

}
