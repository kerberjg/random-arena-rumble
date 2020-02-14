using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform target;
    public float cameraSmoothing;
    Vector3 cameraOffset;

    public float xLeft, xRight;
    public float yBot, yTop;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCameraPosition = target.position + cameraOffset;
        targetCameraPosition.x = Mathf.Clamp(targetCameraPosition.x, xLeft, xRight);
        targetCameraPosition.y = Mathf.Clamp(targetCameraPosition.y, yBot, yTop);

        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, cameraSmoothing * Time.deltaTime);
    }
}
