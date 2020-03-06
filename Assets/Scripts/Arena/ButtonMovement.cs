using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    public float delta = 1f;
    public float moveSpeed = 1f;

    bool moveUp = true;
    private float topValue;
    private float botValue;

    private void Start()
    {
        topValue = transform.position.y + delta;
        botValue = transform.position.y - delta;
    }
    private void Update()
    {
        if (transform.position.y > topValue) 
        {
            moveUp = false;
        }
        else if (transform.position.y < botValue)
        {
            moveUp = true;
        }
        if (moveUp)
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
    }

}
