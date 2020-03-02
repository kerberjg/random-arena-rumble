using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpening = false;
    public bool isOpenned = false;

    public float speed = 5;

    public Transform pivot;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && isOpening == false && isOpenned == false)
        {
            isOpening = true;
        }
    }

    private void Update()
    {
        if(isOpening == true && isOpenned == false)
        {
            pivot.transform.localEulerAngles += new Vector3(0, 1, 0) * speed * Time.deltaTime;
            if(pivot.transform.localEulerAngles.y >= 90.0f)
            {
                pivot.transform.localEulerAngles = new Vector3(
                    pivot.transform.localEulerAngles.x,
                    90.0f,
                    pivot.transform.localEulerAngles.z);
                isOpenned = true;
                isOpening = false;
            }
        }
    }
}
