using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DoorController : MonoBehaviour
{
    public bool isOpening = false;
    public bool isOpenned = false;

    public float speed = 5;
    public float targetAngle = 90.0f;
    public bool clockwise = true;

    public Transform pivot;

    public UnityEvent events;
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
            if (clockwise)
            {
                pivot.transform.Rotate(new Vector3(0, 1, 0), speed * Time.deltaTime);
            }
            else
            {
                pivot.transform.Rotate(new Vector3(0, -1, 0), speed * Time.deltaTime);
            }
            print(pivot.transform.localEulerAngles.y);
            if(Mathf.Abs(pivot.transform.localEulerAngles.y - targetAngle) <= 5f)
            {
                pivot.transform.localEulerAngles = new Vector3(
                    pivot.transform.localEulerAngles.z,
                    targetAngle,
                    pivot.transform.localEulerAngles.x);
                isOpenned = true;
                isOpening = false;

                if(events != null)
                {
                    events.Invoke();
                }
            }
        }
    }
}
