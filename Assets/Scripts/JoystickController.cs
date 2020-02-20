using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    private bool isMovingDown = false;
    private bool pressed = false;

    public float DownSpeed = 10f;
    public float UpSpeed = 5f;

    public void Press() {
        pressed = true;
        isMovingDown = true;
    }

    void Update() {
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
