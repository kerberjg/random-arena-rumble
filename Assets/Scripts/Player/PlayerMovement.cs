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

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal"); //Get Horizontal Input
        movement.y = Input.GetAxisRaw("Vertical"); //Get Vertical Input

        PlayerAiming();
    }

    private void PlayerAiming() {
        shouldersPivot.transform.up = MouseUtils.GetAimDirection(transform.position);
    }

    void FixedUpdate() {
        float speed = this.speed * ValueModifier.TryGetModifier(this).speed;
        transform.Translate(movement * speed * Time.fixedDeltaTime); //Move player based on input.
    }

}
