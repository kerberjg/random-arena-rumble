using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyDebugger : MonoBehaviour
{
    public BaseEnemyBehavior enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = enemy.targetPos;
        this.GetComponent<CircleCollider2D>().radius = enemy.minTargetDistance;
    }
}
