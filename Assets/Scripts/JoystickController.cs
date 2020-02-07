using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public bool isMovingDown = false;
    public bool pressed = false;

    public float DownSpeed = 10f;
    public float UpSpeed = 5f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && pressed == false)
        {
            pressed = true;
            isMovingDown = true;
        }
        if(pressed == true)
        {
            if(isMovingDown == true)
            {
                transform.eulerAngles -= new Vector3(DownSpeed, 0, 0) * Time.deltaTime;
            }
            else if(isMovingDown == false)
            {
                transform.eulerAngles += new Vector3(UpSpeed, 0, 0) * Time.deltaTime;
            }
            if (transform.eulerAngles.x - 360 < -85)
            {
                isMovingDown = false;
            }
            if(transform.eulerAngles.x - 360 < -355 && pressed == true && isMovingDown == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                pressed = false;
            }
        }
    }
}
