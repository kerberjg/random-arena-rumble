using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateGO : MonoBehaviour
{
    float speed = 50000.0f;

    public bool startRotate = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startRotate = true;
        }
        if (startRotate)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
