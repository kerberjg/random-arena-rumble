using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    public float speed = 1f;
    public float delta = 3f;

    private void Update()
    {
        float y = Mathf.PingPong(speed * Time.time, (transform.position.y + delta));
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
